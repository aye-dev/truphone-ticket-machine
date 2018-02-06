using System;
using System.Collections.Generic; 

namespace TruphoneTicketMachine.PrefixSearchAlgorithms
{
    public interface IPrefixSearchAlgorithm
    {
        void LoadStrings(IEnumerable<string> strings);
        Tuple<IEnumerable<string>, IEnumerable<char>> SearchByPrefix(string prefix);
    }
}
