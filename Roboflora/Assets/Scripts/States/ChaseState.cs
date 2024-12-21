
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
        Debug.Log("Chasing player");
        aiController.Animator.SetBool("isChasing", true);
        aiController.Animator.SetBool("isAttacking", false);
        aiController.Animator.SetBool("isThrowing", false);
        aiController.Animator.SetBool("isMoving", false);
        aiController.Agent.speed = aiController.chasingSpeed;
        aiController.isThrowDone = false;
        // No animations, so no need to set any animator parameters
    }

    public void Execute()
    {
        if (!aiController.CanSeePlayer())
        {
            aiController.Agent.speed = 2;
            aiController.StateMachine.TransitionToState(StateType.Patrol);
            return;
        }

        if (aiController.IsPlayerInAttackRange())
        {
            aiController.StateMachine.TransitionToState(StateType.Attack);
            return;
        }

        if(aiController.canThrow && aiController.canEnemyThrow){
            aiController.StateMachine.TransitionToState(StateType.Throw);
            return;
        }

        

        aiController.Agent.destination = aiController.Player.position;
       
    }

    public void Exit()
    {
        // No cleanup necessary
    }
}
