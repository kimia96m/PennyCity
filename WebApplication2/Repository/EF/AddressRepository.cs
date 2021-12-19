using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Profiles;

namespace WebApplication2.Repository.EF
{
    public class AddressRepository : IAddressRepository
    {
        private ApplicationDbContext context;
        public AddressRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task Add(Address address)
        {
            await context.addresses.AddAsync(address);
        }

        public async Task<IEnumerable<Address>> Find(string customerid)
        {
            var address = await context.addresses.Where(c => c.customer.Id == customerid).ToAsyncEnumerable().ToList();
            return address;
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
