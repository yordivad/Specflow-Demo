using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Business
{
    public class Chain : IChain
    {
        private readonly IDictionary<string, Rule> rules;

        public Chain()
        {
            rules = new Dictionary<string, Rule>();
        }

        public void Add(Rule rule)
        {
            rules.Add(rule.Name, rule);
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