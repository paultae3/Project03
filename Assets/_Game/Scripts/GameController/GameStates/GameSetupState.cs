using UnityEngine;

public class GameSetupState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GameSetupState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        _controller.ResetGame();

        Debug.Log("STATE: Game Setup");
        _controller.UnitSpawner.Spawn(
            _controller.PlayerUnitPrefab,
            _controller.PlayerUnitSpawnLocation
        );
    }

    public override void Exit() => base.Exit();
    public override void Tick() => base.Tick();
    public override void FixedTick() => base.FixedTick();
}