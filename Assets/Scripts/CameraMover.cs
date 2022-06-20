using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraMover : MonoBehaviour
{
    public event Action OnBossFightStart;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _finish;

    private void FixedUpdate()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        if(transform.position.z < _finish.transform.position.z)
        {
            _rigidbody.AddForce(Vector3.forward * _speed);
        }
        else
        {
            _rigidbody.isKinematic = true;
            transform.DOLocalMoveY(40, 1);
            transform.DOLocalRotate(new Vector3(20, 0, 0), 1);
            OnBossFightStart?.Invoke();
        }
    }
}
