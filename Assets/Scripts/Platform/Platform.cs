using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private bool _isMove = true;
    [SerializeField]
    private float _speed;

    public float Speed { get { return _speed; } set { _speed = value < 0 ? 0 : value; } }
    public bool IsMove { get { return _isMove; } set { _isMove = value; } }

    void FixedUpdate()
    {
        if (_isMove) transform.Translate(-Vector3.forward * Time.deltaTime * _speed);
    }
}
