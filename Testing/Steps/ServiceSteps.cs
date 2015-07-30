using Domain.Business;
using Domain.Data;
using Domain.Entities;
using Moq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Testing.Steps
{
    [Binding]
    public class ServiceSteps
    {
        private Mock<IClientRepository> _clientRepositoryMock;
        private Mock<IChain> _chainMock;
        private Mock<IInvoiceRepository> _invoiceRepositoryMock;
        private IService _service;

        [BeforeScenario("process")]
        public void Initialize()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _chainMock = new Mock<IChain>();
            _invoiceRepositoryMock = new Mock<IInvoiceRepository>();
            _service = new Service(_invoiceRepositoryMock.Object, _clientRepositoryMock.Object, _chainMock.Object);
        }


        [Given(@"a group of the clients")]
        public void GivenAGroupOfTheClients(Table table)
        {
            var clients = table.CreateSet<Client>();
            _clientRepositoryMock.Setup(c => c.GetClients()).Returns(clients);
        }

        [Given(@"a Group of Invoice for  client \((.*)\)")]
        public void GivenAGroupOfInvoiceForClient(int clientId, Table table)
        {
            _invoiceRepositoryMock.Setup(c => c.Get(It.Is<int>(ic => ic == clientId)))
                .Returns(table.CreateSet<Invoice>());
        }

        [Given(@"a Rule for Invoice \((.*)\)")]
        public void GivenARuleForInvoice(int invoiceId, Table table)
        {
            _chainMock.Setup(c => c.GetRule(It.Is<Invoice>(i => i.Id == invoiceId)))
                .Returns(table.CreateInstance<Rule>());
        }

        [When(@"Invoice Process Run")]
        public void WhenInvoiceProcessRun()
        {
            _service.ProcessAuthorization();
        }

        [Then(@"Verified Client Get Method is Called")]
       public void ThenVerifiedClientGetMethodIsCalled()
        {
            _clientRepositoryMock.Verify(c => c.GetClients());
        }

        [Then(@"Verified the Invoice Method is Called For Client \((.*)\)")]
        public void ThenVerifiedTheInvoiceMethodIsCalledForClient(int clientId)
        {
            _invoiceRepositoryMock.Verify(c => c.Get(It.Is<int>(i => i == clientId)), Times.Once);
        }

        [Then(@"for every invoice the Chain Method is Called four times")]
        public void ThenForEveryInvoiceTheChainMethodIsCalledFourTimes()
        {
            _chainMock.Verify(c => c.GetRule(It.IsAny<Invoice>()), Times.Exactly(4));
        }

        [Then(@"The Method Save is Called four times")]
        public void ThenTheMethodSaveIsCalledFourTimes()
        {
            _invoiceRepositoryMock.Verify(c => c.Save(It.IsAny<Invoice>(), It.IsAny<Rule>()));
        }

    }
}