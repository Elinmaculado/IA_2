using UnityEngine;
using System.Collections.Generic;

namespace BehaviourTree
{
    public class Node
    {
        public enum Status
        {
            Succes,
            Failure,
            Running
        }

        public readonly string name;
        public readonly Status status;
        
        public readonly List<Node> children = new List<Node>();
        protected int currentChild = 0;

        public Node(string name)
        {
            this.name = name;
        }

        public void AddChild(Node child)
        {
            children.Add(child);
        }
        
        public virtual Status Process() => children[currentChild].Process();

        public virtual void Reset()
        {
            currentChild = 0;
            foreach (Node child in children)
            {
                child.Reset();
            }
        }
    }

    public class Leaf : Node
    {
        readonly IStrategies strategy;

        public Leaf(string name, IStrategies strategy) : base(name)
        {
            this.strategy = strategy;
        }

        public override Status Process() => strategy.Process();
        public override void Reset() => strategy.Reset();
    }

    public class BehaviourTree : Node
    {
        public BehaviourTree(string name) : base(name) { }

        public override Status Process()
        {
            while (currentChild < children.Count)
            {
                var status = children[currentChild].Process();
                if (status != Status.Succes)
                {
                    return status;
                }
                currentChild++;
            }
            return Status.Succes;
        }
    }
}
    
    
