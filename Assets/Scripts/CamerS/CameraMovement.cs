using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject _target;
    [SerializeField][Range(1.0f,5.0f)]  private float _speedOfMove = 1.0f;

    private Vector3 _startOffset;
    private Vector3 _offset;

    private float _timeChangedOffset = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        _target = FindObjectOfType<Character>().gameObject;
        _offset = _target.transform.position - transform.position;
        _startOffset = _offset;
        
        InvokeRepeating("CheckObstaclesOnWayOfCam", 0.0f, 0.5f);
    }

    // Update is called once per frame

    void Update()
    {
        if (Time.time - _timeChangedOffset > 5.0f) //return base offset if no changes after x seconds
        {
            Debug.Log(1);
            _offset = Vector3.Lerp(_offset, _startOffset, Time.deltaTime * 0.5f);
        }
        
        float targAngle = _target.transform.eulerAngles.y;
        
        Quaternion rotation = Quaternion.Euler(0, targAngle, 0);
        
        Vector3 pos = _target.transform.position - (rotation * _offset);

        transform.position = Vector3.Lerp(transform.position,pos,Time.deltaTime * _speedOfMove);
        
        transform.LookAt(_target.transform);
    }

    private void CheckObstaclesOnWayOfCam()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit)) //check obstacles in down
        {
            float distBetweenGroundAndCam = Vector3.Distance(transform.position, hit.point);
            
            if (distBetweenGroundAndCam < 2.0f)
            {
                _offset.y -= distBetweenGroundAndCam;
                _timeChangedOffset = Time.time;
            }
        }

        if (Physics.Raycast(transform.position, Vector3.forward, out hit)) // check obstacles in forward
        {
            float distBetweenForwObjAndCam = Vector3.Distance(transform.position, hit.point);
            
            if (distBetweenForwObjAndCam < 4.0f)
            {
                _offset.z += distBetweenForwObjAndCam / 1.9f;
                
                Vector3 direction = hit.transform.position - transform.position;
                float angle = Vector3.Angle(direction, Vector3.forward);

                if (angle > 45)
                {
                    _offset.x += distBetweenForwObjAndCam / 1.9f;
                }
                else
                {
                    _offset.x -= distBetweenForwObjAndCam / 1.9f;
                }

                _timeChangedOffset = Time.time;
            }

        }

    }
}
