using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class UrbanEagleController : MonoBehaviour
{

    public GameObject _eagle;
    public GameObject _obstaclePrefab;
    public GameObject _pickupPrefab;
    public GameObject WingLeft;
    public GameObject WingRight;
    public Text scoreCount;
    [SerializeField] TextMeshProUGUI highScoreText;

    public float _gravity = 30;

    public float _jump = 10;

    private float _verticalSpeed;

    private float _obstacleSpawnCountdown;
    public float _obstacleSpawnInterval = 20;
    public float _obstacleSpeed = 5;
    private GameObject obstacleHolder;
    private int obstacleCount;
    public int score;

    private float _pickupSpawnCountdown;
    public float _pickupSpawnInterval = 2;
    public float _pickupSpeed = 5;
    private GameObject pickupHolder;
    private int pickupCount;

    [SerializeField] private AudioClip _song02;

    [SerializeField] private ParticleSystem _pickupParticle;


    void Start()
    {
        score = 0;
        scoreCount.text = score.ToString();

        obstacleCount = 0;
        Destroy(obstacleHolder);
        obstacleHolder = new GameObject("ObstacleHolder");
        obstacleHolder.transform.parent = this.transform;

        pickupCount = 0;
        Destroy(pickupHolder);
        pickupHolder = new GameObject("PickupHolder");
        pickupHolder.transform.parent = this.transform;

        _verticalSpeed = 0;
        _eagle.transform.position = Vector3.up * 5;

        _obstacleSpawnCountdown = 0;

        _pickupSpawnCountdown = 0;

        MusicManager.Instance.Play(_song02, .1f);
        UpdateHighScoreText();

    }

   
    void Update()
    {

        _verticalSpeed += -_gravity * Time.deltaTime;

        

        if (Input.touchCount>0)
        {
            _verticalSpeed = 0;
            _verticalSpeed += _jump;
        }

        _eagle.transform.position += Vector3.up * _verticalSpeed * Time.deltaTime;

        _obstacleSpawnCountdown -= Time.deltaTime;

        _pickupSpawnCountdown -= Time.deltaTime;

        if (_obstacleSpawnCountdown <= 0)
        {
            _obstacleSpawnCountdown = _obstacleSpawnInterval;
            GameObject building = Instantiate(_obstaclePrefab);
            building.transform.parent = obstacleHolder.transform;
            building.transform.name = (++obstacleCount).ToString();

            building.transform.position += Vector3.right * 40;
            building.transform.position += Vector3.up * Mathf.Lerp(4, 9, Random.value);
        }

        obstacleHolder.transform.position += Vector3.left * _obstacleSpeed * Time.deltaTime;

        if (_pickupSpawnCountdown <= 0)
        {
            _pickupSpawnCountdown = _pickupSpawnInterval;
            GameObject pickup = Instantiate(_pickupPrefab);
            pickup.transform.parent = pickupHolder.transform;
            pickup.transform.name = (++pickupCount).ToString();

            pickup.transform.position += Vector3.right * 70;
            pickup.transform.position += Vector3.up * Mathf.Lerp(4, 9, Random.value);
        }

        pickupHolder.transform.position += Vector3.left * _pickupSpeed * Time.deltaTime;

        float speedToRange = Mathf.InverseLerp(-5, 5, _verticalSpeed);
        float noseAngle = Mathf.Lerp(-10, 10, speedToRange);
        _eagle.transform.rotation = Quaternion.Euler(Vector3.forward * noseAngle) * Quaternion.Euler(Vector3.up *20);

        float wingSpeed = (_verticalSpeed > 0) ? 30 : 5;
        float angle = Mathf.Sin(Time.time * wingSpeed) * 45;
        WingLeft.transform.localRotation = Quaternion.Euler(Vector3.left * angle);
        WingRight.transform.localRotation = Quaternion.Euler(Vector3.right * angle);

        foreach (Transform building in obstacleHolder.transform)
        {
            if (building.position.x < 0)
            {
                int buildingID = int.Parse(building.name);
                if (buildingID > score)
                {
                    score = buildingID;
                    scoreCount.text = score.ToString();
                    CheckHighScore();
                }
            }

            if (building.position.x < -50)
            {
                Destroy(building.gameObject);
            }
        }

        foreach (Transform pickup in pickupHolder.transform)
        {

            if (pickup.position.x < -50)
            {
                Destroy(pickup.gameObject);
            }
        }
    }

    public void CheckHighScore()
    {
        if(score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void UpdateHighScoreText()
    {
        highScoreText.text = $"HighScore: {PlayerPrefs.GetInt("HighScore", 0)}";
    }    

   public void restart()
    {
        Start();
       Debug.Log("In");

       MusicManager.Instance.Play(_song02, .1f);
    }

    public void stopMusic()
    {
        MusicManager.Instance.Stop(2);
    }

    public void pickup()
    {
        foreach (Transform pickup in pickupHolder.transform)
        {
            Time.timeScale = .5f;
            StartCoroutine(ReturnToNormalSpeed());
            Destroy(pickup.gameObject);
            Debug.Log("Grabbed");
            _pickupParticle.Play();
        }

    }

    private IEnumerator ReturnToNormalSpeed()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 1f;
    }

}
