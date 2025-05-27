using MarketPlaceSale.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Models.Product
{
     public record class CreateProductModel(
         Guid SellerId,
         Guid Id,
         string ProductName,
         string Description,
         decimal Price,
         int StockQuantity
         
    ): ICreateModel;
}
