from rest_framework import serializers
from hospital.models import BloodType, Hospital, Inventory, DonationRequest
from authentication.models import BaseUser

class BaseUserSerializer(serializers.ModelSerializer):
    class Meta:
        model = BaseUser
        fields = '__all__'

class HospitalSerializer(serializers.ModelSerializer):
    class Meta:
        model = Hospital
        fields = '__all__'

class InventorySerializer(serializers.ModelSerializer):
    hospital = HospitalSerializer(read_only=True, many=False)
    hosbital_id = serializers.PrimaryKeyRelatedField(write_only=True, queryset=Hospital.objects.all(), source='hospital')
    class Meta:
        model = Inventory
        fields = ['blood_type', 'number_of_bags', 'is_required', 'hospital', 'hosbital_id']        

class DonationRequestSerializer(serializers.ModelSerializer):
    hospital = HospitalSerializer(read_only=True, many=False)
    hosbital_id = serializers.PrimaryKeyRelatedField(write_only=True, queryset=Hospital.objects.all(), source='hospital')
    user = BaseUserSerializer(read_only=True, many=False)
    user_id = serializers.PrimaryKeyRelatedField(write_only=True, queryset=BaseUser.objects.all(), source='user')
    class Meta:
        model = DonationRequest
        fields = ['id', 'blood_type', 'user', 'hospital', 'hosbital_id', 'user_id']

