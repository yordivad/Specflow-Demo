Feature: Chain
	The Chain has the idea to approve some 
	invoice depending of the rule

@chain
Scenario: Adding Rules to  Chain
	Given  Group of rules
	When I Add
	Then Group of the rules are saved

Scenario: Approving Rules
	Given a list of rules 
	| Name   | Step                   | Role               | Apply                                   |
	| Rule 1 | Less 200               | Automatic Approver | (c) => c.Amount < 200                   |
	| Rule 2 | More 200 And Less 400  | Automatic Approve  | (c) => c.Amount > 400 && c.Amount < 600 |
	| Rule 3 | More 600 And Less 800  | Automatic Approve  | (c) => c.Amount > 600 && c.Amount < 8000 |
	| Rule 4 | More 800 And Less 1000 | Automatic Approve  | (c) => c.Amount > 800 && c.Amount < 1000 |
	| Rule 5 | More 200 And Less 400  | John Smith         | (c) => c.Amount > 1000 |
	And  a group of invoices
	| Name      | Date        | Amount  |
	| Invoice 1 | 12/12/2012  | 123.56  |
	| Invoice 2 | 12/12/2013  | 1020.56 |
	| Invoice 3 | 12/12/2014  | 600.56  |
	| Invoice 4 | 12/12/20115 | 789.56  |

	When I Execute 'Invoice 1'
	Then I Get the 'Rule 1'
	When I Execute 'Invoice 2'
	Then I Get the 'Rule 5'
	When I Execute 'Invoice 3'
	Then I Get the 'Rule 3'
	When I Execute 'Invoice 4'
	Then I Get the 'Rule 3'

	When I Execute Invoice
	| Field  | Value      |
	| Name   | John Galt  |
	| Date   | 12/12/2015 |
	| Amount | 685.45     |
	Then I Get the Rule 
	| Field | Value                 |
	| Name  | Rule 3                |
	| Step  | More 600 And Less 800 |
	| Role  | Automatic Approve     |

	
	