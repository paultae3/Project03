using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class UrbanEagleController : MonoBehaviour
{
    private InputHandler _input;

    public GameObject _eagle;

    public float _gravity = 30;

    public float _jump = 10;

    private float _verticalSpeed;


    void Start()
    {
        
    }

   
    void Update()
    {

        _verticalSpeed += -_gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space ))
        {
            _verticalSpeed = 0;
            _verticalSpeed += _jump;
        }

        _eagle.transform.position += Vector3.up * _verticalSpeed * Time.deltaTime;

    }
}
