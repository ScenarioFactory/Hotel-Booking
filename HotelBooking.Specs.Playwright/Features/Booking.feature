Feature: Room Booking
	Guests can book and manage their hotel rooms.

@Web
Scenario: Booking a room
	Given there are no bookings for guest 'Jane Jones'

	When the following booking is made for guest 'Jane Jones'
	| Price  | Deposit Paid | Check-in   | Check-out  |
	| 159.99 | True         | 2022-03-01 | 2022-03-10 |

	Then the booking should appear in the list of bookings

@Web
Scenario: Cancelling a room booking
	Given the following booking for guest 'Jane Jones'
	| Price  | Deposit Paid | Check-in   | Check-out  |
	| 245.00 | False        | 2022-04-03 | 2022-04-07 |

	When the booking is cancelled

	Then the booking should be removed from the list of bookings
