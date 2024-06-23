from django.urls import path
from . import views

urlpatterns = [
    path('post/inventory/', views.PostDonationRequest.as_view(), name='post_inventory'),
]