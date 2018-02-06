using System;
using TruphoneTicketMachine.PrefixSearchAlgorithms;

namespace TruphoneTicketMachine.SearchEngines
{
    public class StationsSearchEngine : IStationsSearchEngine
    {
        private readonly IPrefixSearchAlgorithm _prefixSearchAlgorithm;

        public StationsSearchEngine(IPrefixSearchAlgorithm prefixSearchAlgorithm, IStationsRepository stationsRepository)
        {
            _prefixSearchAlgorithm = prefixSearchAlgorithm;
            _prefixSearchAlgorithm.LoadStrings(stationsRepository.GetStations());
        }

        public StationsSearchResult GetCharactersAndStationsSuggestions(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) 
            {
                throw new NullReferenceException("The searchTerm can't be null");
            }
            var searchAlgResult = _prefixSearchAlgorithm.SearchByPrefix(searchTerm);

            return new StationsSearchResult
                (
                string.Join(", ", searchAlgResult.Item2),
                string.Join(", ", searchAlgResult.Item1)
                );
        }
    }
}
