using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UrbanEagleController : MonoBehaviour
{

    public GameObject _eagle;
    public GameObject _obstaclePrefab;
    public GameObject WingLeft;
    public GameObject WingRight;
    public Text scoreCount;

    public float _gravity = 30;

    public float _jump = 10;

    private float _verticalSpeed;

    private float _obstacleSpawnCountdown;
    public float _obstacleSpawnInterval = 2;
    public float _obstacleSpeed = 5;
    private GameObject obstacleHolder;
    private int obstacleCount;
    private int score;


    void Start()
    {
        score = 0;
        scoreCount.text = score.ToString();

        obstacleCount = 0;
        Destroy(obstacleHolder);
        obstacleHolder = new GameObject("ObstacleHolder");
        obstacleHolder.transform.parent = this.transform;

        _verticalSpeed = 0;
        _eagle.transform.position = Vector3.up * 5;

        _obstacleSpawnCountdown = 0;
    }

   
    void Update()
    {

        _verticalSpeed += -_gravity * Time.deltaTime;

        

        if (Input.touchCount>0)
        {
            _verticalSpeed = 0;
            _verticalSpeed += _jump;
            Debug.Log("Touch");
        }

        _eagle.transform.position += Vector3.up * _verticalSpeed * Time.deltaTime;

        _obstacleSpawnCountdown -= Time.deltaTime;

        if (_obstacleSpawnCountdown <= 0)
        {
            _obstacleSpawnCountdown = _obstacleSpawnInterval;
            GameObject building = Instantiate(_obstaclePrefab);
            building.transform.parent = obstacleHolder.transform;
            building.transform.name = (++obstacleCount).ToString();

            building.transform.position += Vector3.right * 30;
            building.transform.position += Vector3.up * Mathf.Lerp(4, 9, Random.value);
        }

        obstacleHolder.transform.position += Vector3.left * _obstacleSpeed * Time.deltaTime;

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
                }
            }

            if (building.position.x < -30)
            {
                Destroy(building.gameObject);
            }
        }
    }

   private void OnTriggerEnter(Collider collider)
    {
        Start();
       Debug.Log("In");
    }
}
