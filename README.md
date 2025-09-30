# README

El código del examen se encuentra dentro de la carpeta de scripts y tiene le nombre "*IAE1*" y la escena donde se encuentra el NPC es en SampleScene

## Explicación

en este caso se me hizo más sencillo Hacer un script desde cero e implementarle algunos elementos de lo que hicimos en clase, principalmente el FOW

## Comportamiento
El comportamiento que yo agregué fue que la IA tomara diferentes acciones dependiendo del color del material del jugador. En este caso
- Blanceo: Ignora al jugador, continúa patruyando
- Negro: persigue al jugador
- Amarillo: vigila al jugador, se le queda viendo.

### Explicación del códgio
``` C#
    [Header("Info jugador")]
    private GameObject player;
    private Transform playerTransform;
    private MeshRenderer playerMeshRenderer;

    private NavMeshAgent agent;

    [Header("Vision")]
    public float viewDistance = 10f;
    public float viewAngle = 90f;
    private float distanceToPlayer;

    [Header("Patrol info")]
    public Transform[] patrolPoints = new Transform[4];
    private int patrolIndex = 0;
    public float distanceCheck = 1;
    
    
    private enum State {Patrol, Chase, Watch }
    private State currentState = State.Patrol;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerMeshRenderer = player.GetComponent<MeshRenderer>();
        }
        TryGetComponent(out agent);
    }

    void Update()
    {
        Sense();
        Plan();
        Act();
    }
```

Empezamos por la declaración de variables que vamos a utilizar y la asignación de estas, principalmente de los elementos que pertenecen al player cómo lo son su ubicación y su meshRenderer para poder ver el color del jugador.
Finalmente en Update llamamos a las funciones de Sense, Plan y Act.

#### Sense
Tomamos la distancia la jugador, esto nos servirá para el FOV. Además hacemos un cambio del indice de patrullaje para que vaya a sus distintos puntos de patrullaje.
```C#
void Sense()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        if (Vector3.Distance(patrolPoints[patrolIndex].position, transform.position) < distanceCheck && currentState == State.Patrol)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }
```
#### Plan
Aqui es donde se determina cual va a el State osea el comportamiento qeu tiene en base a lo que percibe. Si no puede ni ver al jugador va a patrullar, lo demás cambia en base al color de jugador.
```C#
    void Plan()
    {

        if (!PlayerInFOV())
        {
            currentState = State.Patrol;
            return;
        }

        if (playerMeshRenderer.material.color == Color.white)
            currentState = State.Patrol;
        else if (playerMeshRenderer.material.color == Color.black)
            currentState = State.Chase;
        else if (playerMeshRenderer.material.color == Color.yellow)
            currentState = State.Watch;
    }
``` 
#### Act
En base al estado que tenga hará algo diferente. Cada comportamiento tiene su propio método.
```C#
    void Act()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Watch:
                Watch();
                break;
        }
    }
    
    private void Patrol()
    {
        agent.SetDestination(patrolPoints[patrolIndex].position);
    }

    private void Chase()
    {
        agent.SetDestination(playerTransform.position);
    }

    private void Watch()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);
    }
```

### Funciones extra
PlayerInFOV nos ayuda a determinar si el NPC es capaz de ver al jugador dentro de su campo de visión sin ningún obstaculo.

Y OnDrawGizmosSelected es una función de unity que nos ayuda a visualizar en el NPC parámetros cómo su campo de visión, el ángulo de su visión y la linea que existe actualmente entre el NPC y el jugador, es únicamente para visualizar con más claridad en el inspector.

```C#
    private bool PlayerInFOV()
    {
        Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;

        if (distanceToPlayer > viewDistance)
            return false;

        float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
        if (angleToPlayer > viewAngle / 2f)
            return false;

        if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hit, distanceToPlayer))
        {
            if (hit.collider.gameObject == player)
                return true;

            return false;
        }

        return false;
    }

     void OnDrawGizmosSelected()
    {
        // Radio de visión
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        // Límites del ángulo de visión
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewDistance);

        // Línea hacia el jugador
        if (playerTransform != null)
        {
            Gizmos.color = PlayerInFOV() ? Color.red : Color.gray;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }
    }
```