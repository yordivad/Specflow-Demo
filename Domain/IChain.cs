using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IChain
    {

        Rule this[string name] { get; set; }

        void Add(Rule rule);

        Rule GetRule(Invoice invoice);
    }
}
