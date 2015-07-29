using System;

namespace Domain.Entities
{
    public class Rule
    {
        public String Name { get; set; }

        public string Step { get; set; }

        public Func<ICondition, bool> Apply { get; set; }

        public string Role { get; set; }
    }
}