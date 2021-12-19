using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Products.Comments;

namespace WebApplication2.Repository.EF
{
    public class CommentRepository : ICommentRepository
    {
        private ApplicationDbContext context;
        public CommentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task Add(Comment comment)
        {
            await context.comments.AddAsync(comment);
        }

        public async Task Delete(int id)
        {
            var q = await context.comments.FindAsync(id);
            context.Remove(q);
        }

        public async Task<Comment> Find(int id)
        {
            return await context.comments.FindAsync(id);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
