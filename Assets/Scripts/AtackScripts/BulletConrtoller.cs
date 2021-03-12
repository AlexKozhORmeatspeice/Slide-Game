using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletConrtoller : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _damage = 1.0f;
    private Vector3 _target;
    public Vector3 Target
    {
        set => _target = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_target);
        transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * _speed);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.DamageEnemy(_damage);
        }
        Destroy(gameObject);
    }
    
}
