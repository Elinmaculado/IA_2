using Unity.VisualScripting;
using UnityEngine;

namespace BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence(string name) : base(name) {}
        public override Status Process()
        {
            if (currentChild < children.Count) // Si aun no procesa todos sus hijos
            {
                switch (children[currentChild].Process()) // Procesa el hijo actual cada que se llama
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Failure:
                        Reset();
                        return Status.Failure;
                    case Status.Succes:
                        currentChild++;
                        return currentChild == children.Count ? Status.Succes :  Status.Running;
                    
                }
            }
            Reset();
            return Status.Succes;
        }
    }

    public class Selector : Node
    {
        public Selector(string name) : base(name) {}

        public override Status Process()
        {
            if(currentChild < children.Count)
                switch (children[currentChild].Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Succes:
                        Reset();
                        return Status.Succes;
                    default:
                        currentChild++;
                        return currentChild == children.Count ? Status.Failure : Status.Running;
                }
            Reset();
            return Status.Succes;
        }
    }
}