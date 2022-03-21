using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Manager _manager;
    [SerializeField]
    private Animator _animator;
    private Rigidbody _rb;
    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive { get { return _isAlive; } }
    [SerializeField]
    private bool _onTramplin;
    private Vector2 _MoveDirection;
    [SerializeField]
    private Character _character;
    [SerializeField]
    private bool _isGround;
    public bool IsGround { get { return _isGround; } }
    [SerializeField]
    private bool _isGod;
    [SerializeField]
    private AudioSource _punch;


    void Start()
    {
        _character = GetComponent<Character>();
        if (_isGod)
        {

        }
    }

    private void FixedUpdate()
    {
        if (!_character.IsAlive) _character.Move(Vector2.zero);
    }

    public void Jump()
    {
        if (!_onTramplin) _character.Jump();
    }


    public void Attack() => _character.Attack();

    private void OnCollisionEnter(Collision collision)
    {
        // if (!_character.IsAlive) return;
        // var gameObject = collision.gameObject;
        if (gameObject.GetComponent<Jump>()) _onTramplin = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!_character.IsAlive) return;
        var gameObject = collision.gameObject;
        if (gameObject.GetComponent<Jump>()) _onTramplin = false;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (_character.IsAlive && !_onTramplin)
        {
            _MoveDirection = ctx.ReadValue<Vector2>();
            Vector2 velocity = new Vector2(-_MoveDirection.y, _MoveDirection.x);
            _character.Move(velocity);
        }
    }

    public void DeadAttack()
    {
        _animator.SetTrigger("DeadAttack");
        _isAlive = false;
        _rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Character>();
        if (other.gameObject.GetComponent<Bonus>())
        {
            var bonus = other.gameObject.GetComponent<Bonus>();
            _manager.UpdateScope(bonus.GetScope());
            Destroy(other.gameObject, 0.5f);
        }
        if (!enemy) return;
        // Debug.Log(enemy);
        enemy.DeadAttack();
        _punch.Play();
        // Destroy(enemy.gameObject);
    }
}
