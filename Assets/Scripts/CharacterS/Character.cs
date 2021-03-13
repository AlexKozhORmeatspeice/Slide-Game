using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    public float Speed => _speed;

    [SerializeField] private float damage = 1.0f;

    public float Damage => damage;
    
    private Animator _animator;
    
    private List<GameObject> _movePoints = new List<GameObject>();
    
    private NavMeshAgent _agent;

    private bool underPressure = false; //yeah, it's reference to the queen
    private bool active = false;

    private int _nowPoint = 0;
    
    private enum Animations { idle, run }
    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
        foreach (GameObject mp in GameObject.FindGameObjectsWithTag("MovePoint"))
        {
            _movePoints.Add(mp);
        }
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _agent.speed = _speed;
        StartCoroutine(PlayPauseBetweenPoints());
    }

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            ChoseCharAnimation(Animations.idle);
            return;
        }
        underPressure = CheckFinishStatusOfPoint(_nowPoint);
        
        ChoseCharAnimation(!underPressure ? Animations.run : Animations.idle);

        if (!underPressure && _nowPoint != _movePoints.Count)
        {
            Run(_movePoints[_nowPoint + 1].transform.position);
        }
    }

    private void Run(Vector3 pos) 
    {
        _agent.SetDestination(pos);
        if (Vector3.Distance(transform.position, pos) < 3.0f) //checks the passage of the section
        {
            _nowPoint++;
        }
    }


    private bool CheckFinishStatusOfPoint(int indexOfPoint)
    {
        if(indexOfPoint >= _movePoints.Count || indexOfPoint < 0) return false;
        
        return _movePoints[indexOfPoint].transform.childCount != 0;
    }
    private IEnumerator PlayPauseBetweenPoints()
    {
        active = false;
        yield return new WaitForSeconds(3.0f);
        active = true;
    }

    private void ChoseCharAnimation(Animations animations)
    { 
        _animator.SetInteger("state", (int)animations);
    }
    
}
