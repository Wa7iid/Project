from django.urls import path
from .views import auth_view, logout_view, profile, edit_profile

urlpatterns = [
    path('', auth_view, name='auth'),
    path('logout/', logout_view, name='logout'),
    path('profile/', profile, name='profile'),
    path('profile/edit/', edit_profile, name='edit_profile'),

]