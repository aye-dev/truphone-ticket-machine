using System;
using System.Linq;
using NUnit.Framework;
using TruphoneTicketMachine.PrefixSearchAlgorithms.Trie;

namespace TruphoneTicketMachine.Tests
{
    // brainstorming
    // https://en.wikipedia.org/wiki/Trie - looks like a good choice for the problem at hand,
    // Both Insert and Find run in O(n) time, where n is the length of the key and NOT THE NUMBER OF ENTRIES - 
    // should work well even with thousands of stations that should be the case for UK
    // when the desired node is found, the children keys are the possible characters and the leaves of
    // the subtree are the stations - seems to have the right datastructure for the problem at hand,
    // however i will slowdown the search.
    // ALTERNATIVES? - HASHTABLES? - WE COULD EVEN USE SIMPLY DICTIONARY<STRING, LIST<STRING>> that are "hastables" and create
    // all possible prefix substrings for each possible station name and insert all the stations starting with it,
    // but it would use much more memory 
    [TestFixture]
    public class TrieSearchAlgorithmTests
    {
        [TestCase(new[] { "DARTFORD", "DARTMOUTH", "TOWER HILL", "DERBY" }, "DART", new[] { 'F', 'M' }, new[] { "DARTFORD", "DARTMOUTH" })]
        [TestCase(new[] { "DARTFORD", "DARTMOUTH", "TOWER HILL", "DERBY" }, "D", new[] { 'A', 'E' }, new[] { "DARTFORD", "DARTMOUTH", "DERBY" })]
        [TestCase(new[] { "LIVERPOOL", "LIVERPOOL LIME STREET", "PADDINGTON" }, "LIVERPOOL", new[] { ' ' }, new[] { "LIVERPOOL", "LIVERPOOL LIME STREET" })]
        [TestCase(new[] { "EUSTON", "LONDON BRIDGE", "VICTORIA"  }, "KINGS CROSS", new char[] { }, new string[] { })]
        [TestCase(new[] { "EUSTON", "LONDON BRIDGE", "VICTORIA" }, "LONDON BRIDGE", new char[] { }, new string[] { "LONDON BRIDGE" })]
        [TestCase(new[] { "EUSTON", "LONDON BRIDGE", "VICTORIA" }, "", new char[] { 'E', 'L', 'V' }, new string[] { "EUSTON", "LONDON BRIDGE", "VICTORIA" })]
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

            var searchResult = trieSearchAlgorithm.SearchByPrefix(prefix);
            Assert.IsTrue(matchedLeaves.All(l => searchResult.Item1.Contains(l)));
            Assert.IsTrue(nextChars.All(c => searchResult.Item2.Contains(c)));
        }

        [Test]
        public void Should_Throw_When_Null_Or_Empty_Strings_Loaded()
        {
            var trieSearchAlgorithm = new TrieSearchAlgorithm();

            Assert.Throws<ArgumentOutOfRangeException>(() => trieSearchAlgorithm.LoadStrings(new[] { string.Empty }));
            Assert.Throws<ArgumentOutOfRangeException>(() => trieSearchAlgorithm.LoadStrings(new string[] { null }));
        }

        [Test]
        public void Should_Throw_When_Null_Strings_Array_Loaded()
        {
            var trieSearchAlgorithm = new TrieSearchAlgorithm();

            Assert.Throws<NullReferenceException>(() => trieSearchAlgorithm.LoadStrings(null));
        }
    }

}
