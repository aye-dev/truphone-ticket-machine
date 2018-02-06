using System;
using System.Collections.Generic;

namespace TruphoneTicketMachine.PrefixSearchAlgorithms.Trie
{
    public class TrieSearchAlgorithm : TrieSearchNode, IPrefixSearchAlgorithm
    {
        public void LoadStrings(IEnumerable<string> strings)
        {
            foreach (var str in strings)
            {
                if (string.IsNullOrEmpty(str))
                {
                    throw new ArgumentOutOfRangeException("Loaded string can't be null or empty");
                }
                Insert(str, 0);
            }

            DetermineLeavesAndCompleteNodesValues();
        }



        public Tuple<IEnumerable<string>, IEnumerable<char>> SearchByPrefix(string prefix)
        {
            var node = string.IsNullOrEmpty(prefix) ? this : Find(prefix, 0);
            if (node == null)
            {
                return new Tuple<IEnumerable<string>, IEnumerable<char>>(new string[0], new char[0]);
            }
            return new Tuple<IEnumerable<string>, IEnumerable<char>>(node.GetLeavesAndCompleteNodesValues(), node.GetChildrenKeys());
        }
    }
}
