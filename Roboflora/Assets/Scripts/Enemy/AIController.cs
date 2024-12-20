using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class AIController : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; } // Not needed since we're not using animations
    public Transform[] Waypoints;
    public Transform Player;
    public float SightRange = 10f;
    public float maxAngle = 45.0f;
    public float AttackRange = 15f; // New attack range variable
    public LayerMask PlayerLayer;
    public LayerMask obstacleMask; // Assign this in the Inspector to include walls, terrain, etc.
    public StateType currentState;

    public int throwChance; 
    public bool canThrow;
    public bool isThrowDone;
    public float chasingSpeed;

    public bool canEnemyThrow;
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>(); // Commented out since we're not using animations

        StateMachine = new StateMachine();
        StateMachine.AddState(new IdleState(this));
        StateMachine.AddState(new PatrolState(this));
        StateMachine.AddState(new ChaseState(this));
        StateMachine.AddState(new AttackState(this));
        StateMachine.AddState(new ThrowState(this)); // Add the new AttackState
        StateMachine.TransitionToState(StateType.Idle);
    }

    void Update()
    {
        StateMachine.Update();

        throwChance = Random.Range(1, 100);
    }
    

    public bool CanSeePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
     
     
        if (distanceToPlayer <= SightRange)
        {
            if(distanceToPlayer > 15 && distanceToPlayer < 35 && throwChance == 1 && canEnemyThrow){
                canThrow = true;
                Debug.Log("THROWING");
                return true;
            }
            canThrow = false;
            // // Direction from NPC to player
            // Vector3 directionToPlayer = (Player.position - transform.position).normalized;
            // float angle = Mathf.Acos(Vector3.Dot(transform.forward, directionToPlayer));
            // if (angle < maxAngle)
            // {
            //     // Perform Raycast to check if there's a clear line of sight
            //     if (!Physics.Raycast(raycastOrigin.position, directionToPlayer, SightRange))
            //     {
                    
            //     }
            // }
            // No obstacles in the way
            return true;
        }
        return false;
    }

    // New method to check if the AI is within attack range
    public bool IsPlayerInAttackRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        return distanceToPlayer <= AttackRange;
    }
    public void HitPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeThrowing(){
        isThrowDone = true;
    }
}
