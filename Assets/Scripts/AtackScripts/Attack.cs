using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePref;
    private Character _character;

    void Start()
    {
        _character = FindObjectOfType<Character>();
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && _character.UnderPressure)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject proj = Pooler.Instance.SpawnPoolObject("Bullet", Camera.main.transform.position, Quaternion.identity);
                proj.GetComponent<BulletConrtoller>().Target = hit.point;
            }
        }
    }
}


