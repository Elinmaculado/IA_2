using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTree
{
    public class DekuScrub : MonoBehaviour
    {
        public BehaviourTree dekuTree;
        public NavMeshAgent agent;
        public List<Transform> waypoints = new List<Transform>();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            dekuTree = new BehaviourTree("El deku tree");
            IStrategies patrolStrategy = new PatrolStrategy(transform, agent, waypoints, 3);
            dekuTree.AddChild(new Leaf("Patrullando", patrolStrategy));
        }

        private void Update()
        {
            dekuTree.Process();
        }
    }

}
