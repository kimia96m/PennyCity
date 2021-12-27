using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Comments
{
    public interface ICommentRepository
    {
        Task Add(Comment comment);
        Task Save();
        Task Delete(int id);
        Task<Comment> Find(int id);
        Task<IEnumerable<Comment>> Searchbypid(int pid);

    }
}
