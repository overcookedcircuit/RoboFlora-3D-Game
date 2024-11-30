using UnityEngine;

public class PatrolState : IState
{
    private AIController aiController;
    private int currentWaypointIndex = 0;

    public StateType Type => StateType.Patrol;

    public PatrolState(AIController aiController)
    {
        this.aiController = aiController;
    }

    public void Enter()
    {
        Debug.Log("Boss Patrol");
        aiController.Animator.SetBool("isMoving", true);
        aiController.Animator.SetBool("isChasing", false);
        MoveToNextWaypoint();
    }

    public void Execute()
    {
        if (aiController.CanSeePlayer())
        {
            aiController.StateMachine.TransitionToState(StateType.Chase);
            return;
        }

        if (!aiController.Agent.pathPending && aiController.Agent.remainingDistance <= aiController.Agent.stoppingDistance)
        {
            MoveToNextWaypoint();
        }
    }

    public void Exit()
    {
        // Cleanup if necessary
    }

    private void MoveToNextWaypoint()
    {
        if (aiController.Waypoints.Length == 0)
            return;

        aiController.Agent.destination = aiController.Waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % aiController.Waypoints.Length;
    }
}