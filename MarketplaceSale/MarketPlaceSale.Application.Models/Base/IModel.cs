using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceSale.Application.Models.Base
{
    public interface IModel<out TId> where TId : struct, IEquatable<TId>
    {
        public TId Id { get; }
    }
}
