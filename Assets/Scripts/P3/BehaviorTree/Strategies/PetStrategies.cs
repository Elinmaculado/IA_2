using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTree
{
    public class IsPlayerCloseStrategy : IStrategies
    {
        private Transform player;
        private Transform entity;
        private float chaseDistance;

        public IsPlayerCloseStrategy(Transform entity, Transform player, float chaseDistance)
        {
            this.entity = entity;
            this.player = player;
            this.chaseDistance = chaseDistance;
        }

        public Node.Status Process()
        {
            float distance = Vector3.Distance(entity.position, player.position);
            // Si el jugador está a la distancia que lo perseguimos, succes, si no failure
            return distance <= chaseDistance ? Node.Status.Succes : Node.Status.Failure;
        }

    }

    public class ChasePlayerStrategy : IStrategies
    {
        private Transform player;
        private Transform entity;
        private NavMeshAgent agent;
        private float chaseDistance;

        public ChasePlayerStrategy(Transform entity, Transform player, NavMeshAgent agent, float chaseDistance)
        {
            this.entity = entity;
            this.player = player;
            this.agent = agent;
            this.chaseDistance = chaseDistance;
        }

        public Node.Status Process()
        {
            // Seguimos checando la distancia por si sale del rango
            float distance = Vector3.Distance(entity.position, player.position);
            if (distance > chaseDistance)
            {
                return Node.Status.Failure;
            }

            agent.SetDestination(player.position);
            return Node.Status.Running;
        }
    }

    public class WaitStrategy : IStrategies
    {
        private float waitTime;
        private float timer;

        public WaitStrategy(float waitTime)
        {
            this.waitTime = waitTime;
            timer = 0f;
        }

        public Node.Status Process()
        {
            timer += Time.deltaTime;
            return timer >= waitTime ? Node.Status.Succes : Node.Status.Running;
        }

        public void Reset()
        {
            timer = 0f;
        }
    }

    public class GoHomeStrategy : IStrategies
    {
        private Transform home;
        private NavMeshAgent agent;

        public GoHomeStrategy(Transform home, NavMeshAgent agent)
        {
            this.home = home;
            this.agent = agent;
        }

        public Node.Status Process()
        {
            agent.SetDestination(home.position);
            return agent.remainingDistance < 0.1f ? Node.Status.Succes : Node.Status.Running;
        }
    }
}