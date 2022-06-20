using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private Rigidbody _rigidbody;
    private NavMeshAgent agent;
    private GameC _gameC;
    
    private void Awake()
    {
        _gameC = FindObjectOfType<GameC>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_gameC.balls.Count > 0 && _gameC.hold)
        {
            agent.SetDestination(_gameC.balls[0].transform.position);
        }
    }
}
