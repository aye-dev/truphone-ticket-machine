using System.Collections.Generic;

namespace TruphoneTicketMachine.SearchEngines
{
    public interface IStationsRepository
    {
        IEnumerable<string> GetStations();
    }
}
