using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Data
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients();
    }
}