using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Data
{
    public interface IInvoiceRepository
    {
        IEnumerable<Invoice> Get(int clientId);

        void Save(Invoice invoice, Rule rule);
    }
}