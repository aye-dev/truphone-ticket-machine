using Moq;
using NUnit.Framework;
using System;

namespace TruphoneTicketMachine.Tests.SearchEngine
{
    // Brainstorming
    // -> Given a search term, search engine should retrieve the possible destinations 
    // and next letters matching the search term
    // -> It should rely on some kind of string search algorithm - search effience is a key
    // -> It should rely on some kind of data source and be able to load the data
    // -> It should use the data source to load the data, search the "searchTerm" using the algorithm and
    // retrieve the corresponding destinations and next character suggestions to be presented to the user

    [TestFixture]
    public class SearchEngineTests
    {

    }
}
