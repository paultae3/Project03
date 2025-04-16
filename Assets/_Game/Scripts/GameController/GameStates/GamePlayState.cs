using UnityEngine;

public class GamePlayState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GamePlayState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("STATE: Game Play");
    }

    public override void Tick()
    {
        base.Tick();

        if (_controller.Input.IsTapPressed)
        {
            Debug.Log("You Win!");
            _controller.IsGameWon = true;
            _stateMachine.ChangeState(_stateMachine.EndedState);
        }
        else if (StateDuration >= _controller.TapLimitDuration)
        {
            Debug.Log("You Lose!");
            _controller.IsGameWon = false;
            _stateMachine.ChangeState(_stateMachine.EndedState);
        }
    }

    public override void Exit() => base.Exit();
    public override void FixedTick() => base.FixedTick();
}