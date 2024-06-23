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
    

