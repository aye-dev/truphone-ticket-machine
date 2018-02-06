using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruphoneTicketMachine.PrefixSearchAlgorithms
{
    public interface IPrefixSearchAlgorithm
    {
        void LoadStrings(IEnumerable<string> strings);
        Tuple<IEnumerable<string>, IEnumerable<char>> SearchForPrefix(string prefix);
    }
}
