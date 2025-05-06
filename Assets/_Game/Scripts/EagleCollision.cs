using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public UrbanEagleController _urbanEagleControllerScript;

    public void Start()
    {
        _urbanEagleControllerScript = GameObject.Find("UrbanEagleController").GetComponent<UrbanEagleController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "environment")
        {
            _urbanEagleControllerScript.restart();
        }
    }
}
