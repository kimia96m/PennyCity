using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Profiles
{
    public interface IAddressRepository
    {
        Task Add(Address address);
        Task<IEnumerable<Address>> Find(string customerid);
        Task Save();
    }
}
