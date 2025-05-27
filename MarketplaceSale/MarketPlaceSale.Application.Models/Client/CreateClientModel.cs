using MarketPlaceSale.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Models.Client
{
    public record class CreateClientModel(
        Guid Id,
        string Username
    ) : ICreateModel;
}
