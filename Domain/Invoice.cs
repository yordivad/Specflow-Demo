using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Invoice: ICondition
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
    }
}
