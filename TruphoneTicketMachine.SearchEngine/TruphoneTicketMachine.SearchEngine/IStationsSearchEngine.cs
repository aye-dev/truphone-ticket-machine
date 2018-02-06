namespace TruphoneTicketMachine.SearchEngines
{
    public interface IStationsSearchEngine
    {
        StationsSearchResult GetCharactersAndStationsSuggestions(string searchTerm);
    }
}
