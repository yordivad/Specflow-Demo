using Domain.Data;

namespace Domain.Business
{
    public class Service : IService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IChain _chain;

        public Service(IInvoiceRepository invoiceRepository, IClientRepository clientRepository, IChain chain)
        {
            _invoiceRepository = invoiceRepository;
            _clientRepository = clientRepository;
            _chain = chain;
        }

        public void ProcessAuthorization()
        {
            var clients = _clientRepository.GetClients();
            foreach (var client in clients)
            {
                var invoices = _invoiceRepository.Get(client.Id);
                foreach (var invoice in invoices)
                {
                    var rule = _chain.GetRule(invoice);
                    _invoiceRepository.Save(invoice, rule);
                }
            }
        }
    }
}