Feature: Service
	The Service process all 
	Invoice for clients and actualize 
	the Invoice with the approval

	@process 
	Scenario: Process Invoice for Client
	Given a group of the clients
	| Id | Name     |
	| 1  | Client 1 |
	| 2  | Client 2 |
	| 3  | Client 3 |
	And a Group of Invoice for  client (1)
	| Id | Name      | Value  |
	| 1  | Invoice 1 | 128    |
	| 2  | Invoice 2 | 568.45 |
	And  a Group of Invoice for  client (2)
	| Id | Name      | Value  |
	| 3  | Invoice 3 | 684    |
	And  a Group of Invoice for  client (3)
	| Id | Name      | Value  |
	| 4  | Invoice 4 | 995    |
	And a Rule for Invoice (1)
	| Name   | Step                   | Role               |
	| Rule 1 | Less 500               | Automatic Approver |
	And a Rule for Invoice (2)
	| Name   | Step                   | Role               |
	| Rule 1 | Less 500               | Automatic Approver |
	And a Rule for Invoice (3)
	| Name   | Step                   | Role               |
	| Rule 2 | More 501 And Less 1000 | Automatic Approve  |
	And a Rule for Invoice (4)
	| Name   | Step                   | Role               |
	| Rule 2 | More 501 And Less 1000 | Automatic Approve  |
	When Invoice Process Run
	Then Verified Client Get Method is Called
	And Verified the Invoice Method is Called For Client (1)
	And Verified the Invoice Method is Called For Client (2)
	And Verified the Invoice Method is Called For Client (3)
	And for every invoice the Chain Method is Called four times
	And The Method Save is Called four times
	

