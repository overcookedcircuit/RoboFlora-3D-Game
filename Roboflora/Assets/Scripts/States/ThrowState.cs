using UnityEngine;
using UnityEngine.SceneManagement;

public class ThrowState : IState
{
    private AIController aiController;

    public StateType Type => StateType.Throw;

    public ThrowState(AIController aiController)
    {
        this.aiController = aiController;
    }

    public void Enter()
    {
        aiController.Animator.SetBool("isThrowing", true);
        aiController.Agent.isStopped = true; // Stop the AI agent movement
    }

    public void Execute()
    {
        // Check if the player is Done with Throwing animation
        if (aiController.isThrowDone)
        {
            aiController.StateMachine.TransitionToState(StateType.Patrol);
            return;
        }
     
    }

    public void Exit()
    {
        aiController.Agent.isStopped = false;
        aiController.isThrowDone = false; // Resume the AI agent movement  
    }
}
