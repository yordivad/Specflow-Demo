using Domain.Business;
using Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Testing.Steps
{
    [Binding]
    public class ChainSteps
    {
        private IChain chain;

        [BeforeScenario]
        public void Initialize()
        {
            chain = new Chain();
        }

        [Given(@"Group of rules")]
        public void GivenGroupOfRules()
        {
            ScenarioContext.Current.Add("Rule 1",
                (new Rule
                {
                    Name = "Rule 1",
                    Step = "Less 200",
                    Role = "Automatic Approver",
                    Apply = (c) => c.Amount < 200
                }));
            ScenarioContext.Current.Add("Rule 2",
                (new Rule
                {
                    Name = "Rule 2",
                    Step = "More 200 And Less 400",
                    Role = "Automatic Approver",
                    Apply = (c) => c.Amount > 200 && c.Amount < 400
                }));
            ScenarioContext.Current.Add("Rule 3",
                (new Rule
                {
                    Name = "Rule 3",
                    Step = "More 400 and Less 600",
                    Role = "Automatic Approver",
                    Apply = (c) => c.Amount > 400 && c.Amount < 600
                }));
            ScenarioContext.Current.Add("Rule 4",
                (new Rule
                {
                    Name = "Rule 4",
                    Step = "More 600 and Less 1000",
                    Role = "Automatic Approver",
                    Apply = (c) => c.Amount > 600 && c.Amount < 1000
                }));
            ScenarioContext.Current.Add("Rule 5",
                (new Rule
                {
                    Name = "Rule 5",
                    Step = "More Thant 1000",
                    Role = "John Smith",
                    Apply = (c) => c.Amount > 1000
                }));
        }

        [When(@"I Add")]
        public void WhenIAdd()
        {
            chain.Add(ScenarioContext.Current.Get<Rule>("Rule 1"));
            chain.Add(ScenarioContext.Current.Get<Rule>("Rule 2"));
            chain.Add(ScenarioContext.Current.Get<Rule>("Rule 3"));
            chain.Add(ScenarioContext.Current.Get<Rule>("Rule 4"));
            chain.Add(ScenarioContext.Current.Get<Rule>("Rule 5"));
        }

        [Then(@"Group of the rules are saved")]
        public void ThenGroupOfTheRulesAreSaved()
        {
            Assert.AreEqual(chain["Rule 1"].Name, "Rule 1");
            Assert.AreEqual(chain["Rule 2"].Name, "Rule 2");
            Assert.AreEqual(chain["Rule 3"].Name, "Rule 3");
            Assert.AreEqual(chain["Rule 4"].Name, "Rule 4");
            Assert.AreEqual(chain["Rule 5"].Name, "Rule 5");
        }

        [Given(@"a list of rules")]
        public void GivenAListOfRules(Table table)
        {
            var rules = table.CreateSet<Rule>();
            chain = new Chain();
            rules.ToList().ForEach(r => { chain.Add(r); });

            chain["Rule 1"].Apply = (c) => c.Amount < 200;
            chain["Rule 2"].Apply = (c) => c.Amount > 400 && c.Amount < 600;
            chain["Rule 3"].Apply = (c) => c.Amount > 600 && c.Amount < 800;
            chain["Rule 4"].Apply = (c) => c.Amount > 800 && c.Amount < 1000;
            chain["Rule 5"].Apply = (c) => c.Amount > 1000;
        }

        [Given(@"a group of invoices")]
        public void GivenAGroupOfInvoices(Table table)
        {
            ScenarioContext.Current.Add("invoices", table.CreateSet<Invoice>());
        }

        [When(@"I Execute '(.*)'")]
        public void WhenIExecute(string invoiceName)
        {
            var invoices = ScenarioContext.Current.Get<IEnumerable<Invoice>>("invoices");
            var invoice = invoices.SingleOrDefault(c => c.Name == invoiceName);
            var rule = chain.GetRule(invoice);
            ScenarioContext.Current.Add("execute response", rule);
        }

        [Then(@"I Get the '(.*)'")]
        public void ThenIGetThe(string ruleName)
        {
            var rule = ScenarioContext.Current.Get<Rule>("execute response");
            Assert.AreEqual(ruleName, rule.Name);
            ScenarioContext.Current.Remove("execute response");
        }

        [When(@"I Execute Invoice")]
        public void WhenIExecuteInvoice(Table table)
        {
            var invoice = table.CreateInstance<Invoice>();
            ScenarioContext.Current.Add("response invoice from John Galt", chain.GetRule(invoice));
        }

        [Then(@"I Get the Rule")]
        public void ThenIGetTheRule(Table table)
        {
            var rule = ScenarioContext.Current.Get<Rule>("response invoice from John Galt");
            table.CompareToInstance(rule);
        }
    }
}