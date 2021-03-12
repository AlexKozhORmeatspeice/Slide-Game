using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject _target;
    [SerializeField][Range(1.0f,5.0f)]  private float _speedOfMove = 1.0f;

    private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _target = FindObjectOfType<Character>().gameObject;
        _offset = _target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float targAngle = _target.transform.eulerAngles.y;
        
        Quaternion rotation = Quaternion.Euler(0, targAngle, 0);

        Vector3 pos = _target.transform.position - (rotation * _offset);
        transform.position = Vector3.Lerp(transform.position,pos,Time.deltaTime * _speedOfMove);
        
        transform.LookAt(_target.transform);
    }
}
