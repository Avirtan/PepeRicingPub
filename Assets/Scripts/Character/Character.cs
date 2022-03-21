using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    private LayerMask _layerMaskGround;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _forceJump = 35;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private bool _isAlive;
    [SerializeField]
    private float _distOnGround = 0.5f;
    [SerializeField]
    private bool _onGround;
    [SerializeField]
    public bool _isDebug;
    ///
    public bool IsAlive { get { return _isAlive; } }
    public bool OnGround { get { return _onGround; } }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.sleepThreshold = 0.0f;
        _animator = GetComponentInChildren<Animator>();
        _isAlive = true;
    }

    void Update()
    {
        isGround();
        if (transform.position.y < -3) _isAlive = false;
        if (_isDebug)
        {
            // Debug.Log(_rb.velocity);
        }
    }

    private bool isGround()
    {
        _onGround = Physics.Raycast(transform.position, Vector3.down, _distOnGround, _layerMaskGround);
        _animator.SetBool("isJump", !_onGround);
        if (_isDebug)
            Debug.DrawRay(transform.position, Vector3.down * _distOnGround, Color.green, 0.5f);
        return _onGround;
    }

    public void Move(Vector2 velocity)
    {
        velocity *= _moveSpeed;
        _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.y);
    }

    public void DeadAttack()
    {
        _animator.SetTrigger("DeadAttack");
        _isAlive = false;
        if (gameObject.GetComponent<Enemy>()) Destroy(this.gameObject, 1.5f);
    }

    public void Jump()
    {
        if (isGround() && _isAlive)
        {
            _rb.AddForce(Vector3.up * _forceJump, ForceMode.Acceleration);
        }
    }

    public void Attack()
    {
        if (isGround() && _isAlive)
        {
            // Debug.Log("attack");
            _animator.SetTrigger("Attack");
        }
    }

    public void Reset()
    {
        _isAlive = true;
        transform.position = new Vector3(1.26f, -0.4f, 6);
        _animator.SetTrigger("Alive");
    }

    public void ResetForMenu()
    {
        _isAlive = true;
        transform.position = new Vector3(-0.15f, -0.4f, 6);
        _animator.SetTrigger("Alive");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isAlive) return;
        var gameObject = collision.gameObject;
        if (collision.gameObject.GetComponent<Block>())
        {
            _isAlive = false;
            _animator.SetTrigger("DeadCollision");
            if (GetComponent<Enemy>()) Destroy(this.gameObject, 1.5f);
        }
    }
}
