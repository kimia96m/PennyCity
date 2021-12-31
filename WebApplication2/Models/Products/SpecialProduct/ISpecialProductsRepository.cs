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
        Task<IEnumerable<SpecialProducts>> SearchAsync(string primaryTitle, int? id, int? brand, States states);
        Task<SpecialProducts> Find(int id);
        Task<SpecialProducts> FindbyTitle(string title);
        Task Save();
        Task Delete(SpecialProducts specials);

    }
}
