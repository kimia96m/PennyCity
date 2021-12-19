using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products.Tags;

namespace WebApplication2.Models.Products.Tags
{
    public interface ITagRepository
    {
        Task Add(Tag tags);
        Task Update(Tag tags);
        Task Delete(int id);
        Task Save();
        Task<Tag> Find(int id);
        Task<IEnumerable<Tag>> Search(int? id, string title);
    }
}
