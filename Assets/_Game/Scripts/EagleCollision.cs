using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public UrbanEagleController _urbanEagleControllerScript;

    public GameMenuController _gameMenuControllerScript;

    private AudioSource _audioSource;


    public void Start()
    {
        _urbanEagleControllerScript = GameObject.Find("UrbanEagleController").GetComponent<UrbanEagleController>();

        _gameMenuControllerScript = GameObject.Find("GameMenu").GetComponent<GameMenuController>();

        _audioSource = GetComponent<AudioSource>();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "environment")
        {
            _gameMenuControllerScript.Die();

            _audioSource.Play();

        }
    }
}
