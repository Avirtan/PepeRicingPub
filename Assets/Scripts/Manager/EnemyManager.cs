using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private int _countEnemy;
    [SerializeField]
    private Queue<GameObject> _enemys;

    [SerializeField]
    private float respawnPositionZForward = 17;
    [SerializeField]
    private float respawnPositionZBack = -3.5f;
    [SerializeField]
    private GameObject _enemy;
    public int CountEnemy { get { return _countEnemy; } }
    private float time;

    void Update()
    {
        if (!_enemy && _countEnemy > 0 && time > 1)
        {
            GenerateEnemy();
        }
        if (!_enemy) time += Time.deltaTime;
        else time = 0;
        if (_enemy && _enemy.transform.position.y < -1)
        {
            Destroy(_enemy);
        }
    }

    public void CreateEnemy(int countEnemy)
    {
        _countEnemy = countEnemy;
    }

    private void GenerateEnemy()
    {
        var rand = Random.Range(1, 3);
        var position = rand == 1 ? respawnPositionZForward : respawnPositionZBack;
        _enemy = Instantiate(_enemyPrefab, new Vector3(0, -0.4f, position), Quaternion.identity);
        _enemy.GetComponent<Enemy>().SetPlayer(_player.transform);
        _countEnemy -= 1;
    }

    public void Reset()
    {
        if (_enemy) Destroy(_enemy);
        _countEnemy = 0;
    }
}
