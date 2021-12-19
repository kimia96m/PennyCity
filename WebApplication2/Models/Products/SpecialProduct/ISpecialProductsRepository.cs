using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.SpecialProduct
{
    public interface ISpecialProductsRepository
    {
        Task Add(SpecialProducts specials);
        void Update(SpecialProducts specials);
        Task<IEnumerable<SpecialProducts>> Search(int? id);
        Task<SpecialProducts> Find(int id);
        Task Save();
        Task Delete(SpecialProducts specials);

    }
}
