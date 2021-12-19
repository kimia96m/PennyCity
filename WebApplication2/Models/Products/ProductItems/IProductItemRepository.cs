using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products.ProductItems;

namespace WebApplication2.Models.Products.ProductItems
{
    public interface IProductItemRepository
    {
        Task AddAsync(ProductItem productitem);
        Task AddItemTagValuesAsync(List<ItemTagValue> itemtagvalue);
        Task UpdateAsync(ProductItem productitem);
        Task DeleteAsync(int id);
        Task SaveAsync();
        Task<List<ProductItem>> SearchAsync(int? pid);

        Task itemtagvaluemerge(List<ItemTagValue> itemtagvalue);
        Task<ProductItem> FindAsync(int id);
        Task Update(List<ProductItem> productitems);
        Task<List<ProductItem>> SearchAsync();

    }
}
