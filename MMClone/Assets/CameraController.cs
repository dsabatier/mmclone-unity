using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _playerTransform;
    private float _xPosition;
    private float _zPosition;

    void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _xPosition = transform.position.x;
        _zPosition = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_xPosition, _playerTransform.position.y, _zPosition);
    }
}
