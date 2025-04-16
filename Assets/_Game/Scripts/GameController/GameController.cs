using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Game Data")]
    [SerializeField] private float _tapLimitDuration = 2.5f;

    [Header("Dependencies")]
    [SerializeField] private Unit _playerUnitPrefab;
    [SerializeField] private Transform _playerUnitSpawnLocation;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private InputBroadcaster _input;
    [SerializeField] private bool _resetPositionOnRestart = true;

    // Public properties
    public float TapLimitDuration => _tapLimitDuration;
    public Unit PlayerUnitPrefab => _playerUnitPrefab;
    public Transform PlayerUnitSpawnLocation => _playerUnitSpawnLocation;
    public UnitSpawner UnitSpawner => _unitSpawner;
    public InputBroadcaster Input => _input;
    public bool IsGameWon { get; set; }

    public void ResetGame()
    {
        if (_resetPositionOnRestart && PlayerUnitSpawnLocation != null)
        {
            Camera.main.transform.position = PlayerUnitSpawnLocation.position;
        }
    }
}