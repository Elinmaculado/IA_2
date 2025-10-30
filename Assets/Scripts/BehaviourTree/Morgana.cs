using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTree
{
    public class Morgana : MonoBehaviour
    {
        public BehaviourTree tree;
        public GameObject prize;
        public List<Transform> patrolPoints =  new List<Transform>();

        public NavMeshAgent agent;
        private void Awake()
        {
            tree = new BehaviourTree("Morgana");

            agent = GetComponent<NavMeshAgent>();

            var foo = new Condition(() => prize.activeSelf);
            
            Leaf isPrizePresent = new Leaf("IsPrizePresent", foo);
            Leaf moveToPrize = new Leaf("MoveToPrize", new ActionStrategy(() => agent.SetDestination(prize.transform.position)));
            
            Sequence FindPrize = new Sequence("FindPrize");
            
            FindPrize.AddChild(isPrizePresent);
            FindPrize.AddChild(moveToPrize);
            
            Selector baseSelector = new Selector("Base Selector");
            baseSelector.AddChild(new Leaf("Patrol", new PatrolStrategy(transform, agent, patrolPoints, 3)));
            
            tree.AddChild(baseSelector);
        }

        private void Update()
        {
            tree.Process();
        }
    }
}
