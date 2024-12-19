
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
        aiController.Animator.SetBool("isThrowing", false);
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

        if(aiController.canThrow){
            aiController.StateMachine.TransitionToState(StateType.Throw);
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
