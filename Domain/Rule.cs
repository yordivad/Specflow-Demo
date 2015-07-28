using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Rule
    {
        public String Name { get; set; }

        public string Step { get; set; }

        public Func<ICondition, bool> Apply { get; set; }

        public string Role { get; set; }
    }
}
