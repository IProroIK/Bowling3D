using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _sideMoveSpeed = 5;

    private void Update()
    {
        _rigidbody.AddForce(Vector3.forward * _speed);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * _sideMoveSpeed, transform.position.y, transform.position.z);
            }
        }
    }

}
