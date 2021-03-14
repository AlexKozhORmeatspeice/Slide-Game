using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    private Vector3 origin;
    [SerializeField] private float _speed = 3.0f;
    public float Speed => _speed;

    [SerializeField] private float damage = 1.0f;

    public float Damage => damage;
    
    private Animator _animator;
    
    private List<GameObject> _movePoints = new List<GameObject>();
    
    private NavMeshAgent _agent;

    private bool underPressure = false; //yeah, it's reference to the queen

    public bool UnderPressure => underPressure;
    
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
        active = false;
        ChoseCharAnimation(Animations.idle);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            active = true;
        }
        if(!active) { return; }
        
        underPressure = CheckFinishStatusOfPoint(_nowPoint);
        if (_nowPoint == _movePoints.Count - 1 && !underPressure)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        
        ChoseCharAnimation(!underPressure ? Animations.run : Animations.idle);

        if (!underPressure && _nowPoint != _movePoints.Count)
        {
            Run(_movePoints[_nowPoint + 1].transform.position);
        }
        else
        {
            GameObject nowPointObj = _movePoints[_nowPoint];
            if (nowPointObj.transform.childCount != 0)
            {
                gameObject.transform.LookAt(nowPointObj.transform.GetChild(nowPointObj.transform.childCount - 1).position); // get middle enemy and look at him
            }
        }
    }

    private void Run(Vector3 pos) 
    {
        _agent.SetDestination(pos);
        pos.y = transform.position.y;
        if (Vector3.Distance(transform.position, pos) < 2.0f) //checks the passage of the section
        {
            _agent.ResetPath();
            _nowPoint++;
        }
    }
    
    private bool CheckFinishStatusOfPoint(int indexOfPoint)
    {
        if(indexOfPoint >= _movePoints.Count || indexOfPoint < 0) return false;
        
        return _movePoints[indexOfPoint].transform.childCount != 0;
    }

    private void ChoseCharAnimation(Animations animations)
    { 
        _animator.SetInteger("state", (int)animations);
    }


}
