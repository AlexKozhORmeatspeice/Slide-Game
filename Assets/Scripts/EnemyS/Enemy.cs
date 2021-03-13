using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp = 20.0f;

    public float Hp => hp;
    
    // Start is called before the first frame update
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
        Destroy(gameObject);
    }

   
}
