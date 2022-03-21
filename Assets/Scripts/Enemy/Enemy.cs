using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private AudioSource _punch;
    [SerializeField]
    private Transform _playerPosition;
    [SerializeField]
    private Character _player;
    [SerializeField]
    private Character _playerAttack;
    private bool _isAlive;
    [SerializeField]
    private float _timeToAttack;
    [SerializeField]
    private float _delayToAttack = 1.3f;
    [SerializeField]
    private float _delayJump;

    [SerializeField]
    private Character _character;

    void Start()
    {
        _character = GetComponent<Character>();
    }

    void Update()
    {
        if (_character.IsAlive && _playerAttack && _playerAttack.IsAlive)
        {
            if (_timeToAttack < _delayToAttack)
            {
                _timeToAttack += Time.deltaTime;
            }
            else
            {
                _character.Attack();
                _playerAttack.DeadAttack();
                _punch.Play();
                _timeToAttack = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (_character.IsAlive && _player.IsAlive)
        {
            if (!_player.OnGround && _character.OnGround && _delayJump > 1)
            {
                _character.Jump();
                _delayJump = 0;
            }
            if (!_character._isDebug) MoveToPlayer();
            _delayJump += Time.fixedDeltaTime;
        }
        else if (!_isAlive && _player.IsAlive)
        {
            Vector2 velocity = Vector2.down;
            _character.Move(velocity);
        }
    }

    private void MoveToPlayer()
    {
        Vector3 direction = _playerPosition.position - transform.position;
        float magnitude = direction.magnitude;
        direction.Normalize();
        var velocity = new Vector2(direction.x, direction.z);
        if (magnitude < 2)
        {
            velocity /= 2;
        }
        _character.Move(velocity);
    }

    public void SetPlayer(Transform player)
    {
        _playerPosition = player;
        _player = player.gameObject.GetComponent<Character>();
    }

    public void DeadPlayer()
    {
        if (_playerAttack && _playerAttack.IsAlive)
        {
            _playerAttack.DeadAttack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (!player) return;
        _playerAttack = player;
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (!player) return;
        _playerAttack = null;
        _timeToAttack = 0;
    }
}
