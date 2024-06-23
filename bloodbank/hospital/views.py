from django.shortcuts import render, redirect
from django.urls import reverse
from .forms import RequestCreateForm
from django.views import View
from .models import BloodType, Hospital, Inventory, DonationRequest
from django.shortcuts import get_object_or_404
from django.contrib.auth.decorators import login_required
from django.contrib.auth.mixins import LoginRequiredMixin


@login_required
def index(request):
    return render(request, 'hospital/index.html')

class SearchBlood(View, LoginRequiredMixin):

    def get(self, request):
        form = RequestCreateForm()
        context = {
            'form': form,
            }
        return render(request, 'hospital/search.html', context)
    
    def post(self, request):
        blood_type = request.POST.get('blood_type')
        blood = get_object_or_404(BloodType, type=blood_type)
        hospitals = Inventory.objects.filter(blood_type=blood, is_required=False)
        context = {
            'hospitals' : hospitals
        }
        print(hospitals)
        return render(request, 'hospital/search.html', context)
    
class DonateBlood(View, LoginRequiredMixin):
    def get(self, request):
        form = RequestCreateForm()
        context = {
            'form': form,
            }
        return render(request, 'hospital/donate.html', context)
    
    def post(self, request):
        blood_type = request.POST.get('blood_type')
        blood = get_object_or_404(BloodType, type=blood_type)
        hospitals = Inventory.objects.filter(blood_type=blood, is_required=True)
        context = {
            'hospitals' : hospitals
        }
        print(hospitals)
        return render(request, 'hospital/donate.html', context)
    
@login_required
def about(request):
    hospital = Hospital.objects.all().count()
    donation = DonationRequest.objects.all().count()
    return render(request, 'about-us.html', {'hospital': hospital, 'donation': donation})

@login_required
def contact(request):
    return render(request, 'contact-us.html')

@login_required
def reaserches(request):
    return render(request, 'reaserches.html')


class PostDonationRequest(View, LoginRequiredMixin):
    def post(self, request):
        inventory = request.POST.get('inventory_id')
        inventory = get_object_or_404(Inventory, id=inventory)
        
        DonationRequest.objects.create(
            hospital=inventory.hospital,
            blood_type=inventory.blood_type,
            user=request.user
        )

        return redirect(reverse('profile'))
        