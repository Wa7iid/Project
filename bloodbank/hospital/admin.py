from django.contrib import admin
from . import models


admin.site.register(models.Hospital)
admin.site.register(models.BloodType)
# admin.site.register(models.Inventory)
admin.site.register(models.DonationRequest)
admin.site.register(models.Contact)

from django.contrib import admin
from .models import Inventory

@admin.register(Inventory)
class InventoryAdmin(admin.ModelAdmin):
    list_display = ('hospital_name', 'blood_type', 'number_of_bags', 'is_required')
    list_filter = ('hospital__name', 'blood_type')
    search_fields = ['hospital__name']

    def hospital_name(self, obj):
        return obj.hospital.name

    hospital_name.admin_order_field = 'hospital__name'

    # Optionally, you can define other methods or properties as needed
