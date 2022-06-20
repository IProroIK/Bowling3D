using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PathCreation;
using System;
public class BallStacking : MonoBehaviour
{
    public event Action OnGameEnd;

    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private float _shotPower;
    [SerializeField] private float _timeForStack;
    [SerializeField] private GameObject _stackSphere;
    [SerializeField] private Rigidbody _stackSphereRb;
    [SerializeField] private GameC _gameC;
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private Collider _aimWall;

    private bool _isGameFinishing = false;
    private Vector3 _shootDirection;
    private int ballsCount = 0;
    private Camera _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main;
        _cameraMover.OnBossFightStart += CanShoot;
    }

    private void OnDisable()
    {
        _cameraMover.OnBossFightStart -= CanShoot;
    }
    private void Update()
    {
        if (_isGameFinishing)
        {
            Shoot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StackBall(other);
    }
    
    private void StackBall(Collider ball)
    {
        ballsCount++;
        ball.transform.DOMove(_stackSphere.transform.position, _timeForStack);
        _stackSphere.transform.DOScale(ballsCount * 0.05f, _timeForStack);
        Destroy(ball.gameObject, _timeForStack);

    }
    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = new Ray();
            ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 worldPosition = hit.point;
                Vector3 nearestWorldPositionOnPath = _pathCreator.path.GetPointAtDistance(_pathCreator.path.GetClosestDistanceAlongPath(worldPosition));
                _arrow.transform.position = nearestWorldPositionOnPath;
                _shootDirection = _arrow.transform.position - _stackSphere.transform.position;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _stackSphereRb.isKinematic = false;
            _stackSphereRb.AddForce(_shootDirection * _shotPower, ForceMode.Impulse);
            _aimWall.enabled = false;
            StartCoroutine(EndOfGameDelay());

        }

    }

    private IEnumerator EndOfGameDelay()
    {
        yield return new WaitForSeconds(2);
        OnGameEnd?.Invoke();
    }

    private void CanShoot()
    {
        _isGameFinishing = true;
    }
}
