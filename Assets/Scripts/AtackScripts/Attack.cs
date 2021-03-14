using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Attack : MonoBehaviour, IPointerDownHandler
{
    private Vector3 origin;
    
    [SerializeField] private GameObject _projectilePref;
    private Character _character;

    void Start()
    {
        origin = Vector3.zero;
        _character = FindObjectOfType<Character>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (origin != Vector3.zero && _character.UnderPressure)
        {
            Ray ray = Camera.main.ScreenPointToRay(origin);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject proj = Pooler.Instance.SpawnPoolObject("Bullet", Camera.main.transform.position, Quaternion.identity);
                proj.GetComponent<BulletConrtoller>().Target = hit.point;
            }

            origin = Vector3.zero;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        origin = eventData.position;
    }
}


