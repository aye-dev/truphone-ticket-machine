# truphone-ticket-machine exercise

### Techs/Process used:
 - .NET Framework
 - NUnitLite
 - Moq
 - TDD

### Algorithms used - a variation of Trie:
- Find complexity is O(n) - where n is the length of the key and NOT THE NUMBER OF ENTRIES;
- The leaves (stations) for each intermediate node are determined during the trie loading, there is no extra-cost of traversing the subtree to determine the leaves when searching.

### Installation and Usage:
- git clone https://github.com/aye-dev/truphone-ticket-machine.git
- Build the solution and run TruphoneTicketMachine.Tests
