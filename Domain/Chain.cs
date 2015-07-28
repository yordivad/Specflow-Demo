using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Chain : IChain
    {
        private IDictionary<String, Rule> rules;

        public Chain()
        {
            rules = new Dictionary<string, Rule>();
        }

        public void Add(Rule rule)
        {
            rules.Add(rule.Name,rule);
        }

        public Rule this[string name]
        {
            get { return rules[name]; }
            set { throw new NotImplementedException(); }
        }


        public Rule GetRule(Invoice invoice)
        {
            var list = rules.Values.ToList();
            return list.FirstOrDefault(c => c.Apply(invoice));
        }
    }
}
