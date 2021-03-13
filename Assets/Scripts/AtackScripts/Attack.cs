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
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _character.active)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject proj = Instantiate(_projectilePref, Camera.main.transform.position, Quaternion.identity);
                proj.GetComponent<BulletConrtoller>().Target = hit.point;
            }
        }
    }
}


