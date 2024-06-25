from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework import status
from hospital.models import BloodType, Hospital, Inventory, DonationRequest
from rest_framework.permissions import AllowAny
from .serializers import HospitalSerializer, InventorySerializer

class SearchInventory(APIView):
    permission_classes = [AllowAny, ]
    def get(self, request, blood_type):
        if blood_type not in ['A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-']:
            return Response({'error': 'Invalid blood type'}, status=status.HTTP_400_BAD_REQUEST)
        blood_type = Inventory.objects.filter(type=blood_type, is_required=False) 
        serializer = InventorySerializer(blood_type, many=True) 
        return Response(serializer.data, status=status.HTTP_200_OK)
    

class SearchHospital(APIView):
    permission_classes = [AllowAny, ]
    def get(self, request, blood_type):
        if blood_type not in BloodType.objects.values_list('type', flat=True):
            return Response({'error': 'Invalid blood type'}, status=status.HTTP_400_BAD_REQUEST)
        blood_type = Inventory.objects.filter(type=blood_type, is_required=True) 
        serializer = HospitalSerializer(blood_type, many=True) 
        return Response(serializer.data, status=status.HTTP_200_OK)


class DonateRequest(APIView):
    permission_classes = [AllowAny, ]
    def post(self, request):
        serializer = HospitalSerializer(data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
    


from rest_framework.decorators import api_view
from rest_framework.response import Response
from django.core.mail import send_mail
from django.conf import settings
from .serializers import ContactSerializer

@api_view(['POST'])
def contact_api_view(request):
    if request.method == 'POST':
        serializer = ContactSerializer(data=request.data)
        if serializer.is_valid():
            contact = serializer.save()

            # Send confirmation email to the user
            send_mail(
                'Thank you for contacting us',
                f"Dear {contact.name},\n\n"
                f"Thank you for reaching out to us. We have received your message and will get back to you shortly.\n\n"
                f"Best regards,\nBlood Bank Team",
                settings.DEFAULT_FROM_EMAIL,
                [contact.email],
                fail_silently=False,
            )

            return Response({'success': 'Message sent successfully'}, status=status.HTTP_200_OK)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)