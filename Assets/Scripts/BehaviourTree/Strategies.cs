using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTree
{
    public interface IStrategies
    {
        Node.Status Process();

        void Reset()
        {
            // Ño
        }
    }

    public class Condition : IStrategies
    {
        private readonly Func<bool> predicate;

        public Condition(Func<bool> predicate)
        {
            this.predicate = predicate;
        }
        public Node.Status Process() => predicate() ? Node.Status.Succes : Node.Status.Failure;
    }

    public class ActionStrategy : IStrategies
    {
        private readonly Action doSometing;

        public ActionStrategy(Action doSometing)
        {
            this.doSometing = doSometing;
        }

        public Node.Status Process()
        {
            doSometing();
            return Node.Status.Succes;
        }
    }

    public class PatrolStrategy : IStrategies
    {
        public Transform entity;
        public NavMeshAgent agent;
        public List<Transform> patrolPoints;
        public float patrolSpeed;
        public int patrolIndex = 0;

        private bool isPathCalculated;

        public PatrolStrategy(Transform entity, NavMeshAgent agent, List<Transform> patrolPoints, float patrolSpeed)
        {
            this.entity = entity;
            this.agent = agent;
            this.patrolPoints = patrolPoints;
            this.patrolSpeed = patrolSpeed;
        }
        public Node.Status Process()
        {
            if (patrolIndex == patrolPoints.Count)
            {
                return Node.Status.Succes;
            }
            
            var target = patrolPoints[patrolIndex];
            agent.SetDestination(target.position);
            entity.LookAt(new Vector3(target.position.x, entity.position.y, target.position.z));

            // WTF porque solo jala si es falso
            if (!isPathCalculated && agent.remainingDistance < 0.1f)
            {
                
                isPathCalculated = false;
                patrolIndex++;
            }

            if (agent.pathPending)
            {
                isPathCalculated = true;
            }
            
            return Node.Status.Running;
        }

        public void Reset() => patrolIndex = 0;
    }
}
