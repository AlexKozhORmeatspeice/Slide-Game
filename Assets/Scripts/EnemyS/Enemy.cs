using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp = 20.0f;
    private Animator _animator;
    public float Hp => hp;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Die();
        }
    }

    public void DamageEnemy(float dam)
    {
        hp -= dam;
    }

    public void Die()
    {
        StartCoroutine(DeathPlay());
    }

    private IEnumerator DeathPlay()
    {
        _animator.SetBool("die", true);
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
   
}
