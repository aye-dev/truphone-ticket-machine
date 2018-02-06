namespace TruphoneTicketMachine.SearchEngines
{
    //Could be somewhere in TruphoneTicketMachine.DAL library, to KISS left here
    public interface IStationsSearchEngine 
    {
        StationsSearchResult GetCharactersAndStationsSuggestions(string searchTerm);
    }
}
