from django import forms
from .models import Hospital, Inventory, DonationRequest
from django.contrib.auth.models import User
from django.contrib.auth.forms import UserCreationForm


class RequestCreateForm(forms.ModelForm):
    class Meta:
        model = DonationRequest
        fields = ['hospital', 'blood_type', 'user']        