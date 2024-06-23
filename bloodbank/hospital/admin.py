from django.contrib import admin
from . import models


admin.site.register(models.Hospital)
admin.site.register(models.BloodType)
admin.site.register(models.Inventory)
admin.site.register(models.DonationRequest)