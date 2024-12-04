
using UnityEngine;
public class ChaseState : IState
{
    private AIController aiController;

    public StateType Type => StateType.Chase;

    public ChaseState(AIController aiController)
    {
        this.aiController = aiController;
    }

    public void Enter()
    {
	   aiController.Animator.SetBool("isChasing", true);
       aiController.Animator.SetBool("isAttacking", false);
       aiController.Agent.speed = 7;
       Debug.Log("Boss chasing");
        // No animations, so no need to set any animator parameters
    }

    public void Execute()
    {
        if (!aiController.CanSeePlayer())
        {
            Debug.Log("Chasing Stop");
            aiController.Agent.speed = 2;
            aiController.StateMachine.TransitionToState(StateType.Patrol);
            return;
        }

        if (aiController.IsPlayerInAttackRange())
        {
            aiController.StateMachine.TransitionToState(StateType.Attack);
            return;
        }

        aiController.Agent.destination = aiController.Player.position;
       
    }

    public void Exit()
    {
        // No cleanup necessary
    }
}
