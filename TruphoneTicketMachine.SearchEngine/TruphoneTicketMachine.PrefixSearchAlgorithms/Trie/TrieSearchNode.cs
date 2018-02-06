using System.Collections.Generic;

namespace TruphoneTicketMachine.PrefixSearchAlgorithms.Trie
{
    public class TrieSearchNode
    {
        private bool _isComplete = false;
        public TrieSearchNode(string value = "")
        {
            Value = value;
            Children = new Dictionary<char, TrieSearchNode>();
        }

        public IEnumerable<string> GetLeavesAndCompleteNodesValues()
        {
            return LeavesAndCompleteNodesValues;
        }

        public IEnumerable<char> GetChildrenKeys()
        {
            return Children.Keys;
        }

        public void DetermineLeavesAndCompleteNodesValues()
        {
            LeavesAndCompleteNodesValues = new List<string>();
            if (_isComplete)
            {
                LeavesAndCompleteNodesValues.Add(Value);
            }
            AddLeaves(LeavesAndCompleteNodesValues);

            foreach(var childKeyValue in Children)
            {
                childKeyValue.Value.DetermineLeavesAndCompleteNodesValues();
            }
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

        protected void AddLeaves(List<string> leaves)
        {
            if (Children.Count == 0)
            {
                leaves.Add(Value);
                return;
            }
            foreach (var child in Children)
            {
                child.Value.AddLeaves(leaves);
            }
        }

        protected string Value { get; private set; }
        protected Dictionary<char, TrieSearchNode> Children { get; }
        protected List<string> LeavesAndCompleteNodesValues { get; private set; }
    }

}
