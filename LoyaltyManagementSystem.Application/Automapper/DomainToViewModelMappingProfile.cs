using AutoMapper;
using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.Application.ViewModels.Promotion;
using LoyaltyManagementSystem.Application.ViewModels.System;
using LoyaltyManagementSystem.Data.Entities;

namespace LoyaltyManagementSystem.Application.Automapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Inventory, InventoryViewModel>();
            CreateMap<Collection, CollectionViewModel>();
            CreateMap<CollectionDetail, CollectionDetailViewModel>();
            CreateMap<Campaign, CampaignViewModel>();
            CreateMap<CampaignDetail, CampaignDetailViewModel>();
            CreateMap<Store, StoreViewModel>();
            CreateMap<Customer, CustomerViewModel>();
        }
    }
}