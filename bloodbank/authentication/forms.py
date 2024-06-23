# authentication/forms.py

from django import forms
from django.contrib.auth.forms import AuthenticationForm, UserCreationForm
from authentication.models import BaseUser
from django.contrib.auth.forms import PasswordChangeForm

class CustomAuthenticationForm(AuthenticationForm):
    username = forms.CharField(widget=forms.TextInput(attrs={'placeholder': 'Enter your email'}))
    password = forms.CharField(widget=forms.PasswordInput(attrs={'placeholder': 'Enter your password'}))

    class Meta:
        model = BaseUser
        fields = ['username', 'password']

class CustomUserCreationForm(UserCreationForm):
    name = forms.CharField(widget=forms.TextInput(attrs={'placeholder': 'Enter your name'}))
    email = forms.EmailField(widget=forms.EmailInput(attrs={'placeholder': 'Enter your email'}))
    password1 = forms.CharField(widget=forms.PasswordInput(attrs={'placeholder': 'Enter your password'}))
    password2 = forms.CharField(widget=forms.PasswordInput(attrs={'placeholder': 'Confirm your password'}))

    class Meta:
        model = BaseUser
        fields = ['name', 'email', 'password1', 'password2']


from django import forms
from .models import BaseUser

class EditProfile(forms.ModelForm):
    class Meta:
        model = BaseUser
        fields = ['image', 'name', 'email', 'blood_type', 'phone']
        widgets = {
            'image': forms.FileInput(attrs={'class': 'form-control'}),
            'name': forms.TextInput(attrs={'class': 'form-control'}),
            'phone': forms.TextInput(attrs={'class': 'form-control'}),
            'email': forms.EmailInput(attrs={'class': 'form-control'}),
        }


class CustomPasswordChangeForm(PasswordChangeForm):
    class Meta:
        model = BaseUser
        fields = ['old_password', 'new_password1', 'new_password2']

    old_password = forms.CharField(
    label="Old Password",
    widget=forms.PasswordInput(),
    help_text=""
    )
    new_password1 = forms.CharField(
        label="New Password",
        widget=forms.PasswordInput(),
        help_text=""
    )
    new_password12 = forms.CharField(
        label="New Password Confirmation",
        widget=forms.PasswordInput(),
        help_text=""
    )        