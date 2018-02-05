using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TruphoneTicketMachine.Tests.SearchEngine
{
    // Brainstorming
    // -> Given a search term, search engine should retrieve the possible destinations 
    // and next letters matching the search term
    // -> It should rely on some kind of string search algorithm - search efficency is a key
    // -> It should rely on some kind of data source and be able to load the data
    // -> It should use the data source to load the data, search the "searchTerm" using the algorithm and
    // retrieve the corresponding destinations and next character suggestions to be presented to the user

    [TestFixture]
    public class SearchEngineTests
    {
        
        [TestCase(new [] {"DARTFORD", "DARTMOUTH", "TOWER HILL", "DERBY"}, "DART", "F,M", "DARTFORD, DARTMOUTH")]
        public void Should_Suggest(IEnumerable<string> stations, string searchTerm, string suggestedCharacters, string suggestedStations)
        {
            var prefixSearchAlgorithm = new Mock<IPrefixSearchAlgorithm>();
            var stationsRepository = new Mock<IStationsRepository>();
            var searchEngine = new SearchEngine(prefixSearchAlgorithm.Object, stationsRepository.Object);
            var searchResult = searchEngine.GetCharactersAndStationsSuggestions(searchTerm);
            Assert.IsNotNull(searchResult);
        }
    }

    public class SearchEngine
    {
        private readonly IPrefixSearchAlgorithm _prefixSearchAlgorithm;
        private readonly IStationsRepository _stationsRepository;

        public SearchEngine(IPrefixSearchAlgorithm prefixSearchAlgorithm, IStationsRepository stationsRepository)
        {
            _prefixSearchAlgorithm = prefixSearchAlgorithm;
            _stationsRepository = stationsRepository;
        }

        public Tuple<string, string> GetCharactersAndStationsSuggestions(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }

    public interface IStationsRepository
    {

    }

    public interface IPrefixSearchAlgorithm
    {
    }
}
