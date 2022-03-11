### Repository structure

**HotelBooking.Specs** - sample tests covering the Hotel Booking application. The approach used is component-level page objects. I have favoured pre-test teardown rather than post-test, as this produces more robust tests and allows post-test analysis.

**HotelBooking.Specs.Playwright** - the same sample tests covering the Hotel Booking application but using the Playwright library.

**HotelBooking.Specs.Screenplay** - the same sample tests covering the Hotel Booking application but using the Screenplay pattern.

**ExploratoryTestingNotes.txt** - manual, exploratory testing notes.

### SpecFlow test suite principles

- Provide database and other infrastructure access via stateless components.
- Retain SpecFlow types and parsing of SpecFlow parameters within step files.
- Avoid coupling steps to infrastructure methods. Pass DTOs or primitive types between them.
- Only perform assertions in steps.
- Perform polling for eventually consistent values in steps, not infrastructure methods.
- Perform waiting for UI spinners and page loads in the steps that triggered them, not the steps that follow.
- Use hooks very sparingly and with explicit method names and file locations.
- Avoid sharing state between step files.
- When state is shared between steps in the same file, use local member variables where possible over injected types.
- Where state is shared between steps, assert the state with an explicit guard in dependent steps.

### Stateless v Stateful step file philosophy

When developing scenarios, the preference is for highly readable scenarios without repetition. e.g.

```
@Web
Scenario: Create new customer
	Given the following customer details
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  |

	When I create the customer in the system

	Then the customer is added to the system with the details provided
	And the customer is marked as manually invoiced
```

To aid readability some state has to be maintained in step files. As complexity increases it may be necessary to scope step files to specific feature files to maintain readable scenarios, moving common parts of user journeys into separate, stateless components. Infrastructure access should already be stateless and highly reusable.

As an alternative where maximum step re-usability is needed, perhaps where scenarios are to be written by non-programming QAs, the Gherkin can be rewritten to pass much of the state to the steps. This is more verbose and less readable, but allows QAs to re-use steps more easily. e.g.

```
@Web
Scenario: Create new customer
	Given there are no customers named 'Jane Jones'

	When I create a new customer in the system with the following details
	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  |

	Then a customer is present in the system with the following details
 	| Title | Name       | Address Line 1   | Address Line 2 | Address Line 3 | Postcode |
	| Mrs   | Jane Jones | 72 Acacia Avenue | Shepherds Bush | London         | W12 8QT  |
  
	And customer 'Jane Jones' is marked as manually invoiced
```
