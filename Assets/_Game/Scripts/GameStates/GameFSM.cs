using UnityEngine;

[RequireComponent(typeof(GameController))]
public class GameFSM : StateMachineMB
{
    private GameController _controller;
    public GameSetupState SetupState { get; private set; }
    public GamePlayState PlayState { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<GameController>();
        SetupState = new GameSetupState(this, _controller);
        PlayState = new GamePlayState(this, _controller);
    }
}