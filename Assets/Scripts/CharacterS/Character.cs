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
    
    private List<Vector3> _movePoints = new List<Vector3>();
    
    private NavMeshAgent _agent;

    private bool finishedPlot = true;

    private int _nowPoint = 0;
    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
        foreach (GameObject mp in GameObject.FindGameObjectsWithTag("MovePoint"))
        {
            _movePoints.Add(mp.transform.position);
        }
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        Debug.Log(_speed);
        _agent.speed = _speed;
        StartCoroutine(PlayStartScene());
    }

    // Update is called once per frame
    void Update()
    {
        
        _animator.SetInteger("state", finishedPlot ? 1 : 0); 

        
        if (finishedPlot && _nowPoint != _movePoints.Count)
        {
            Run(_movePoints[_nowPoint]);
        }
    }

    private void Run(Vector3 pos) 
    {
        pos.y = 0;
        
        _agent.SetDestination(pos);

        if (Vector3.Distance(transform.position, pos) < 3.0f) //checks the passage of the section
        {
            _agent.Stop();
            _nowPoint++;
            finishedPlot = false;
        }
    }

    private IEnumerator PlayStartScene()
    {
        finishedPlot = false;
        yield return new WaitForSeconds(3.0f);
        finishedPlot = true;
    }
    
}
