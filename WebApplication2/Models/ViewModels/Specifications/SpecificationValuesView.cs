using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebApplication2.Models.Products;
using WebApplication2.Models.ViewModels.Products;

namespace WebApplication2.Models.ViewModels.Specifications
{
    public class SpecificationValuesView: IAsyncQueryProvider
    {
        public int id { get; set; }
        public string title { get; set; }
        public States state { get; set; }
        public ProductView product
        {
            get; set;
        }
        public SpecificationView specification { get; set; }
        public string creator { get; set; }
        public string createdate { get; set; }
        public string lastmodifier { get; set; }
        public string lastmodifydate { get; set; }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
