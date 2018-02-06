namespace TruphoneTicketMachine.SearchEngines
{
    public class StationsSearchResult
    {
        public const string NoCharactersFound = "no next characters";
        public const string NoStationsFound = "no stations";

        public StationsSearchResult(string characters, string stations)
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


}
