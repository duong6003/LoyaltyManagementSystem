using AutoMapper;
using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.Application.ViewModels.Promotion;
using LoyaltyManagementSystem.Application.ViewModels.System;
using LoyaltyManagementSystem.Data.Entities;

namespace LoyaltyManagementSystem.Application.Automapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(c => new Product(c.Name, c.ImageList, c.Description, c.Content, c.SKU, c.Status, c.DateCreated, c.DateModified));
            CreateMap<AppRoleViewModel, AppRole>()
                .ConstructUsing(c => new AppRole(c.Name, c.Description));
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(c => new Product(c.Name,c.ImageList,c.Description,c.Content,c.SKU,c.Status,c.DateCreated,c.DateModified));
            CreateMap<InventoryViewModel, Inventory>()
                .ConstructUsing(c => new Inventory(c.WareHouseId,c.ProductId,c.Quantity,c.Status,c.DateCreated,c.DateModified,c.ProductState));
            CreateMap<CollectionViewModel, Collection>()
                .ConstructUsing(c => new Collection(c.Name, c.Description, c.Status, c.DateCreated, c.DateModified, c.StartDate, c.EndDate));
            CreateMap<CollectionDetailViewModel, CollectionDetail>()
               .ConstructUsing(c => new CollectionDetail(c.CollectionId, c.ProductId, c.StoreId));
            CreateMap<CampaignViewModel, Campaign>()
               .ConstructUsing(c => new Campaign(c.Name, c.Description, c.Status, c.DateCreated, c.DateModified, c.StartDate, c.EndDate));
            CreateMap<CampaignDetailViewModel, CampaignDetail>()
               .ConstructUsing(c => new CampaignDetail(c.CampaignId,c.CollectionId,c.ProductId,c.StoreId));
            CreateMap<StoreViewModel, Store>()
                .ConstructUsing(c => new Store(c.Name, c.Address, c.Country, c.Status));
            CreateMap<CustomerViewModel, Customer>()
               .ConstructUsing(c => new Customer(c.StoreId,c.CustomerName,c.CustomerAddress, c.CustomerPhone,c.RedemptionCode, c.Gift,c.DayOfRedemption, c.DateCreated,c.DateModified));
        }
    }
}