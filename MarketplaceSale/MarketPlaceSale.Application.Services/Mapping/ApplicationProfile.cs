using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MarketplaceSale.Domain.ValueObjects;
using MarketplaceSale.Domain.Entities;
using MarketPlaceSale.Application.Models.Product;
using MarketPlaceSale.Application.Models.Order;
using MarketPlaceSale.Application.Models.Seller;
using MarketPlaceSale.Application.Models.Client;
using MarketPlaceSale.Application.Models.OrderLine;



namespace MarketPlaceSale.Application.Services.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Money, decimal>().ConvertUsing(x => x.Value);
            CreateMap<Quantity, int>().ConvertUsing(x => x.Value);
            CreateMap<Username, string>().ConvertUsing(x => x.Value);
            CreateMap<Description, string>().ConvertUsing(x => x.Value);
            CreateMap<ProductName, string>().ConvertUsing(x => x.Value);

            CreateMap<Client, ClientModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
                .ForMember(dest => dest.AccountBalance, opt => opt.MapFrom(src => src.AccountBalance))
                .ForMember(dest => dest.PurchaseHistory, opt => opt.MapFrom(src => src.PurchaseHistory.Select(o => o.Id)))
                .ForMember(dest => dest.ReturnHistory, opt => opt.MapFrom(src => src.ReturnHistory.Select(o => o.Id)))
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Cart));

            CreateMap<Seller, SellerModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
                .ForMember(dest => dest.BusinessBalance, opt => opt.MapFrom(src => src.BusinessBalance))
                .ForMember(dest => dest.AvailableProducts, opt => opt.MapFrom(src => src.AvailableProducts))
                .ForMember(dest => dest.SalesHistory, opt => opt.MapFrom(src => src.SalesHistory));

            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName.Value))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.Seller.Id));

            CreateMap<OrderLine, OrderLineModel>();  
            //CreateMap<Product, ProductModel>();      
            //CreateMap<Seller, SellerModel>();        

            CreateMap<Order, OrderModel>()
                .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines))
                .ForMember(dest => dest.ReturnedProducts, opt => opt.MapFrom(src => src.ReturnedProducts))
                .ForMember(dest => dest.ReturnStatuses, opt => opt.MapFrom(src => src.ReturnStatuses));
        }
    }
}
