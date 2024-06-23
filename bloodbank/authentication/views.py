from django.shortcuts import render, redirect
from django.contrib.auth import authenticate, login, logout
from .forms import CustomAuthenticationForm, CustomUserCreationForm, EditProfile, CustomPasswordChangeForm
from django.contrib.auth import update_session_auth_hash
from hospital.models import DonationRequest
from django.contrib.auth.decorators import login_required
from django.shortcuts import get_object_or_404



def auth_view(request):
    login_form = CustomAuthenticationForm()
    register_form = CustomUserCreationForm()

    if request.method == 'POST':
        action = request.POST.get('action')

        if action == 'Login':
            email = request.POST.get('username')
            password = request.POST.get('password')
            user = authenticate(request, username=email, password=password)
            print("user", user)

            if user is not None:
                login(request, user)
                return redirect('home')  # Replace with your desired redirect URL
            else:
                return render(request, 'authentication/auth.html', {'login_form': login_form, 'register_form': register_form, 'login_error': True, 'login_error_msg': 'Invalid email or password'})

        elif action == 'Signup':
            register_form = CustomUserCreationForm(request.POST)
            if register_form.is_valid():
                user = register_form.save(commit=False)
                user.email = user.email.lower()  # Ensure email is lowercase
                user.save()
                login(request, user)  # Automatically login the user after registration
                return redirect('home')  # Replace with your desired redirect URL
            else:
                # Print form errors for debugging
                print(register_form.errors)
                return render(request, 'authentication/auth.html', {'login_form': login_form, 'register_form': register_form, 'register_error': True})

    return render(request, 'authentication/auth.html', {'login_form': login_form, 'register_form': register_form})


def logout_view(request):
    logout(request)
    return redirect('auth')

@login_required
def profile(request):
    user_donations = DonationRequest.objects.filter(user=request.user).order_by('-id')
    last_donate  = user_donations.last()
    if last_donate:
        last_donate = last_donate.created_at
    else:
        last_donate = "You Dont have Donations"
    if request.method == 'POST':
        complete_id = request.POST.get('complete_id')
        confirmed = get_object_or_404(DonationRequest, id=complete_id)
        confirmed.is_done = True
        confirmed.save()
        return render(request, 'authentication/profile.html', {'user_donations': user_donations, 'last_donate': last_donate})
    return render(request, 'authentication/profile.html', {'user_donations': user_donations, 'last_donate': last_donate})


def edit_profile(request):
    if request.method == "POST":
        if request.FILES :
            form = EditProfile(request.POST, request.FILES, instance=request.user)
        else:
            form = EditProfile(request.POST, instance=request.user)
        if form.is_valid():  
            form.save()
            return redirect('profile') 
    else:
        form = EditProfile(instance=request.user)
    return render(request, "authentication/edit_profile.html", {'form': form})


# def change_password(request):
#     if request.method == 'POST':
#         form = CustomPasswordChangeForm(request.user, request.POST)
#         if form.is_valid():
#             user = form.save()
#             update_session_auth_hash(request, user)
#             return redirect('profile')
#     else:
#         form = CustomPasswordChangeForm(request.user)
#     return render(request, 'registration/edit_password.html', {'form': form})