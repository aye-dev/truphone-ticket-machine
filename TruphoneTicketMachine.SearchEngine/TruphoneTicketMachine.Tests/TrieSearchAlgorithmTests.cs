using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using TruphoneTicketMachine.PrefixSearchAlgorithms;

namespace TruphoneTicketMachine.Tests
{
    // brainstorming
    // https://en.wikipedia.org/wiki/Trie - looks like a good choice for the problem at hand,
    // Both Insert and Find run in O(n) time, where n is the length of the key and NOT THE NUMBER OF ENTRIES - 
    // should work well even with thousands of stations that should be the case for UK
    // when the desired node is found, the children keys are the possible characters and the leaves of
    // the subtree are the stations - seems to have the right datastructure for the problem at hand,
    // however i will slowdown the search.
    // ALTERNATIVES? - HASHTABLES? - WE COULD EVEN USE SIMPLY DICTIONARY<STRING, LIST<STRING>> and create
    // all possible prefix substrings for each possible station name and insert all the stations starting with it,
    // but it would use much more memory
    [TestFixture]
    public class TrieSearchAlgorithmTests
    {
        [TestCase(new[] { "DARTFORD", "DARTMOUTH", "TOWER HILL", "DERBY" }, "DART", new[] { 'F', 'M' }, new[] { "DARTFORD", "DARTMOUTH" })]
        [TestCase(new[] { "LIVERPOOL", "LIVERPOOL LIME STREET", "PADDINGTON" }, "LIVERPOOL", new[] { ' ' }, new[] { "LIVERPOOL", "LIVERPOOL LIME STREET" })]
        [TestCase(new[] { "EUSTON", "LONDON BRIDGE", "VICTORIA"  }, "KINGS CROSS", new char[] { }, new string[] { })]
        [TestCase(new[] { "EUSTON", "LONDON BRIDGE", "VICTORIA" }, "LONDON BRIDGE", new char[] { }, new string[] { "LONDON BRIDGE" })]
        public void Should_Retrieve_Expected_Search_Results
            (
            string[] availableStrings, 
            string prefix, 
            char[] nextChars, 
            string[] matchedLeaves
            )
        {
            var trieSearchAlgorithm = new TrieSearchAlgorithm();
            trieSearchAlgorithm.LoadStrings(availableStrings);

            var searchResult = trieSearchAlgorithm.SearchForPrefix(prefix);
            Assert.IsTrue(searchResult.Item1.All(l => matchedLeaves.Contains(l)));
            Assert.IsTrue(searchResult.Item2.All(c => nextChars.Contains(c)));
        }
    }

    public class TrieSearchAlgorithm : IPrefixSearchAlgorithm
    {
        public void LoadStrings(IEnumerable<string> strings)
        {
            throw new NotImplementedException();
        }

        public Tuple<IEnumerable<string>, IEnumerable<char>> SearchForPrefix(string prefix)
        {
            throw new NotImplementedException();
        }
    }
}
