using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePref;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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


