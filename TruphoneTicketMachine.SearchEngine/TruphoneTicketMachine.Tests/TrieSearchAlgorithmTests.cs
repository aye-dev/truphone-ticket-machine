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
            
            Assert.Throws<NullReferenceException>(() => trieSearchAlgorithm.LoadStrings(null));
            Assert.Throws<ArgumentOutOfRangeException>(() => trieSearchAlgorithm.LoadStrings(new[] { string.Empty }));
            Assert.Throws<ArgumentOutOfRangeException>(() => trieSearchAlgorithm.LoadStrings(new string[] { null }));
        }
    }

    public class TrieSearchAlgorithm : TrieSearchNode, IPrefixSearchAlgorithm
    {
        public void LoadStrings(IEnumerable<string> strings)
        {
            foreach(var str in strings)
            {
                if (string.IsNullOrEmpty(str))
                {
                    throw new ArgumentOutOfRangeException();
                }
                Insert(str, 0);
            }
        }

        public Tuple<IEnumerable<string>, IEnumerable<char>> SearchByPrefix(string prefix)
        {
            var node  = string.IsNullOrEmpty(prefix) ? this : Find(prefix, 0);
            if(node == null)
            {
                return new Tuple<IEnumerable<string>, IEnumerable<char>>(new string[0], new char[0]);
            }
            return new Tuple<IEnumerable<string>, IEnumerable<char>>(node.GetLeaves(), node.GetChildrenKeys());
        }
    }

    public class TrieSearchNode
    {
        private bool _isComplete = false;
        public TrieSearchNode(string value = "")
        {
            Value = value;
            Children = new Dictionary<char, TrieSearchNode>();
        }

        protected void Insert(string newValue, int index)
        {
            if (index == newValue.Length)
            {
                Value = newValue;
                _isComplete = true;
                return;
            }

            TrieSearchNode child;
            if (!Children.TryGetValue(newValue[index], out child))
            {
                Children[newValue[index]] = child = new TrieSearchNode(newValue.Substring(0, index + 1));
            }
            child.Insert(newValue, ++index);
        }

        protected TrieSearchNode Find(string value, int index)
        {
            if (Value == value)
            {
                return this;
            }

            if (index == value.Length)
            {
                return null;
            }

            TrieSearchNode child;
            if (Children.TryGetValue(value[index], out child))
            {
                return child.Find(value, ++index);
            }
            return null;
        }

        public IEnumerable<char> GetChildrenKeys()
        {
            return Children.Keys;
        }

        public IEnumerable<string> GetLeaves()
        {
            var leavesList = new List<string>();
            if (_isComplete)
            {
                leavesList.Add(Value);
            }
            AddLeaves(leavesList);
            return leavesList;
        }

        protected void AddLeaves(List<string> leaves)
        {
            if(Children.Count == 0)
            {
                leaves.Add(Value);
                return;
            }
            foreach(var child in Children)
            {
                child.Value.AddLeaves(leaves);
            }
        }

        protected string Value { get; private set; }
        protected Dictionary<char, TrieSearchNode> Children { get; }
    }
}
