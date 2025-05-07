using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public UrbanEagleController _urbanEagleControllerScript;

    public GameMenuController _gameMenuControllerScript;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip _hurtSound;

    [SerializeField] private AudioClip _pickupSound;


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

            _audioSource.PlayOneShot(_hurtSound);

        }

        if (other.gameObject.tag == "pickup")
        {
            _urbanEagleControllerScript.pickup();

            _audioSource.PlayOneShot(_pickupSound);

        }
    }
}
