using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTree
{
    public class PetBT : MonoBehaviour
    {
        public Transform player;
        public Transform home;
        public float chaseDistance = 5f;
        public float waitTime = 3f;
        public NavMeshAgent agent;

        private BehaviourTree tree;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            tree = new BehaviourTree("PetAI");

            var isPlayerClose = new Leaf("IsPlayerClose", new IsPlayerCloseStrategy(transform, player, chaseDistance));
            var chasePlayer = new Leaf("ChasePlayer", new ChasePlayerStrategy(transform ,player, agent, chaseDistance));
            var wait = new Leaf("Wait", new WaitStrategy(waitTime));
            var goHome = new Leaf("GoHome", new GoHomeStrategy(home, agent));

            // Hacemos el nodo sequence para saber si va a perseguir al jugador o no
            var chaseSequence = new Sequence("ChaseSequence");
            chaseSequence.AddChild(isPlayerClose);
            chaseSequence.AddChild(chasePlayer);

            // Hacemos el nodo en el que se va a su casita
            var fallbackSequence = new Sequence("FallbackSequence");
            fallbackSequence.AddChild(wait);
            fallbackSequence.AddChild(goHome);

            // La raíz del árbol
            var rootSelector = new Selector("RootSelector");
            rootSelector.AddChild(chaseSequence);
            rootSelector.AddChild(fallbackSequence);

            tree.AddChild(rootSelector);
        }

        void Update()
        {
            tree.Process();
        }

    }

}