using UnityEngine;

public class GameEndedState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GameEndedState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(_controller.IsGameWon ? "VICTORY! Tap to restart." : "GAME OVER. Tap to retry.");
    }

    public override void Tick()
    {
        base.Tick();

        if (_controller.Input.IsTapPressed)
        {
            // Restart the game cycle
            _stateMachine.ChangeState(_stateMachine.SetupState);

            // Alternative: Return to main menu
            // SceneManager.LoadScene("MainMenu"); 
        }
    }

    public override void Exit() => base.Exit();
}