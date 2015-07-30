using Domain.Entities;

namespace Domain.Business
{
    public interface IChain
    {
        Rule this[string name] { get; set; }

        void Add(Rule rule);

        Rule GetRule(Invoice invoice);
    }
}