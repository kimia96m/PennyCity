using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Tags
{
    public interface ITagValuesRepository
    {
        Task Add(TagValeus tags);
        Task Update(TagValeus tags);
        Task Delete(int id);
        Task Save();
        Task<TagValeus> Find(int id);
        Task<IEnumerable<TagValeus>> Search(int? id, string title, int tagid);
        Task<IEnumerable<TagValeus>> SearchAdvance(States state, string title);
        Task<IEnumerable<TagValeus>> Search(string title);
    }
}
