using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Pool;
using UnityEngine;

public class BulletConrtoller : MonoBehaviour, IPooledObj
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _damage = 1.0f;
    private Vector3 _target;
    public Vector3 Target
    {
        set
        {
            _target = value;
            direction = _target - transform.position;
        }
    }

    private Vector3 direction;
    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        direction = _target - transform.position;
        Invoke("Destroy", 10.0f);
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(_target);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * _speed);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.DamageEnemy(_damage);
        }
        this.Destroy();
    }
    private void Destroy()
    {
        gameObject.SetActive(false);
    }

}
