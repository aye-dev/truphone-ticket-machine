using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TruphoneTicketMachine.Tests.SearchEngine
{
    // Brainstorming
    // -> Given a search term, search engine should retrieve the possible Stations 
    // and next letters matching the search term
    // -> It should rely on some kind of string search algorithm - search efficency is a key
    // -> It should rely on some kind of data source and be able to load the data
    // -> It should use the data source to load the data, search the "searchTerm" using the algorithm and
    // retrieve the corresponding Stations and next character suggestions to be presented to the user

    [TestFixture]
    public class SearchEngineTests
    {
        
        [TestCase("DART", new[] { 'F', 'M' }, new[] { "DARTFORD", "DARTMOUTH" }, "F, M", "DARTFORD, DARTMOUTH")]
        [TestCase("LIVERPOOL", new[] { ' ' }, new[] { "LIVERPOOL", "LIVERPOOL LIME STREET" }, " ", "LIVERPOOL, LIVERPOOL LIME STREET")]
        [TestCase("KINGS CROSS’", new char[] { }, new string[] { }, SearchResult.NoCharactersFound, SearchResult.NoStationsFound)]
        public void Should_Suggest_Characters_And_Stations
            (
            string searchTerm,
            char[] characters,
            string[] stations, 
            string suggestedCharacters, 
            string suggestedStations
            )
        {

            var stationsRepository = new Mock<IStationsRepository>();
            stationsRepository.Setup(r => r.GetStations()).Returns(new string[] { });

            // Assign
            var prefixSearchAlgorithm = new Mock<IPrefixSearchAlgorithm>();
            prefixSearchAlgorithm.Setup(a => a.SearchForPrefix(searchTerm)).Returns
                (
                new Tuple<IEnumerable<string>, IEnumerable<char>>(stations, characters)
                );

            // Act
            var searchEngine = new SearchEngine(prefixSearchAlgorithm.Object, stationsRepository.Object);
            var searchResult = searchEngine.GetCharactersAndStationsSuggestions(searchTerm);

            // Assert/Verify
            stationsRepository.Verify(r => r.GetStations(), Times.Once);
            prefixSearchAlgorithm.Verify(a => a.LoadStrings(It.IsAny<IEnumerable<string>>()), Times.Once);

            Assert.IsNotNull(searchResult);
            Assert.AreEqual(searchResult.Charracters, suggestedCharacters);
            Assert.AreEqual(searchResult.Stations, suggestedStations);
        }

        [Test]
        public void Throws_When_StationsRepository_Is_Null()
        {
            var prefixSearchAlgorithm = new Mock<IPrefixSearchAlgorithm>();
            Assert.Throws<NullReferenceException>(() => new SearchEngine(prefixSearchAlgorithm.Object, null));
        }

        [Test]
        public void Throws_When_PrefixSearchAlgorithm_Is_Null()
        {
            var stationsRepository = new Mock<IStationsRepository>();
            Assert.Throws<NullReferenceException>(() => new SearchEngine(null, stationsRepository.Object));
        }

        [Test]
        public void Throws_When_SearchTerm_Is_Null()
        {
            var stationsRepository = new Mock<IStationsRepository>();
            stationsRepository.Setup(r => r.GetStations()).Returns(new string[] { });

            var prefixSearchAlgorithm = new Mock<IPrefixSearchAlgorithm>();

            var searchEngine = new SearchEngine(prefixSearchAlgorithm.Object, stationsRepository.Object);
            var exception = Assert.Throws<NullReferenceException>(() => searchEngine.GetCharactersAndStationsSuggestions(null));
            Assert.AreEqual(exception.Message, "The searchTerm can't be null");
        }
    }

    public class SearchEngine
    {
        private readonly IPrefixSearchAlgorithm _prefixSearchAlgorithm;


        public SearchEngine(IPrefixSearchAlgorithm prefixSearchAlgorithm, IStationsRepository stationsRepository)
        {
            _prefixSearchAlgorithm = prefixSearchAlgorithm;
            _prefixSearchAlgorithm.LoadStrings(stationsRepository.GetStations());
        }

        public SearchResult GetCharactersAndStationsSuggestions(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                throw new NullReferenceException("The searchTerm can't be null");
            }
            var searchAlgResult = _prefixSearchAlgorithm.SearchForPrefix(searchTerm);

            return new SearchResult
                (
                string.Join(", ", searchAlgResult.Item2),
                string.Join(", ", searchAlgResult.Item1)
                );
        }
    }

    public class SearchResult
    {
        public const string NoCharactersFound = "no next characters";
        public const string NoStationsFound = "no stations";

        public SearchResult(string characters, string stations)
        {
            if (string.IsNullOrEmpty(characters))
            {
                Charracters = NoCharactersFound;
            }
            else
            {
                Charracters = characters;
            }

            if (string.IsNullOrEmpty(stations))
            {
                Stations = NoStationsFound;
            }
            else
            {
                Stations = stations;
            }
        }

        public string Charracters { get; }
        public string Stations { get; }
    }

    public interface IStationsRepository
    {
        IEnumerable<string> GetStations();
    }

    public interface IPrefixSearchAlgorithm
    {
        void LoadStrings(IEnumerable<string> strings);
        Tuple<IEnumerable<string>, IEnumerable<char>> SearchForPrefix(string prefix);
    }
}
