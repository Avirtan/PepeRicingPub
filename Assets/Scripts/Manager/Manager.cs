using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;
    [SerializeField]
    private PlayServiceManager _playServiceManager;
    [SerializeField]
    private PlatformManager _platformManager;
    [SerializeField]
    private InterfaceManager _interfaceManager;
    [SerializeField]
    private EnemyManager _enemyManager;
    [SerializeField]
    private StateGame _state;
    private StateGame _previousState;
    private float _maxTimeEanbleState = 9f;
    private float _minTimeEanbleState = 4f;
    [SerializeField]
    private float _timeLeft = 0;
    [SerializeField]
    private float _timer;
    [SerializeField]
    private float _allTimeGame = 0;
    [SerializeField]
    private int _level = 1;
    [SerializeField]
    private bool _isDebug;
    [SerializeField]
    public bool IsDebug { get { return _isDebug; } }
    [SerializeField]
    private StateGame _debugState;
    [SerializeField]
    private float timeScope;
    [SerializeField]
    private int _countEnemy = 3;
    [SerializeField]
    private Character _player;
    [SerializeField]
    private bool _gameStart;
    [SerializeField]
    private int _scope;

    void Start()
    {
        Screen.SetResolution(2340, 1080, true);
        // UpdateState();
        if (!IsDebug)
        {
            SetStateManage(StateGame.NOTHING);
            _gameStart = false;
        }
        else
        {
            _gameStart = true;
        }
    }

    void Update()
    {
        if (_gameStart)
        {
            UpdateGame();
        }
        else
        {
            _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void UpdateGame()
    {
        if (_player.IsAlive)
        {
            if (_state == StateGame.JUMPPLATFORM && _platformManager.JumpCreateCound == 1)
            {
                // Debug.Log("Update State From Jump");
                UpdateState();
            }
            if (_state == StateGame.BLOCKPLATFORM && _timer > _timeLeft)
            {
                // Debug.Log("Update State From Block");
                UpdateState();
            }
            if (_state == StateGame.ENEMY && _enemyManager.CountEnemy == 0)
            {
                // Debug.Log("Update State From Enemy");
                UpdateState();
            }
            _timer += Time.deltaTime;
            _allTimeGame += Time.deltaTime;
            timeScope += Time.deltaTime;
            if (timeScope > .5f)
            {
                UpdateScope(1);
                timeScope = 0;
            }
            // Debug.Log((_allTimeGame * 10) % 5);
            // if (_allTimeGame > 10 && _level == 1)
            // {
            //     UpdateLevel();
            // }
        }
        else
        {
            if (!_menu.activeSelf)
            {
                _platformManager.StopPlatform();
                // _platformManager.UpdateSpeed(0);
                _menu.SetActive(true);
            }
        }
    }

    private void UpdateState()
    {
        _previousState = _state;
        StateGame tmpState = (StateGame)Random.Range(0, 3);
        // if(tmpState == StateGame.ENEMY) tmpState = StateGame.JUMPPLATFORM;
        if (_previousState == StateGame.JUMPPLATFORM)
        {
            tmpState = StateGame.BLOCKPLATFORM;
        }
        _timeLeft = Random.Range(_minTimeEanbleState, _maxTimeEanbleState);
        _timer = 0;
        if (_isDebug) SetStateManage(_debugState);
        else SetStateManage(tmpState);
    }

    private void UpdateLevel()
    {
        _level += 1;
        var speed = 8;
        _platformManager.UpdateSpeed(speed);
    }

    private void SetStateManage(StateGame stateGame)
    {
        _state = stateGame;
        _platformManager.State = _state;
        _platformManager.JumpCreateCound = 0;
        _platformManager.IsDebug = _isDebug;
        if (stateGame == StateGame.ENEMY)
            _enemyManager.CreateEnemy(_countEnemy);
    }

    public void Restart()
    {
        _playServiceManager.UpdateRaiting(_scope);
        // _interfaceManager.UpdateText(_scope);
        _menu.SetActive(false);
        _platformManager.Reset();
        _enemyManager.Reset();
        _player.Reset();
        _allTimeGame = 0;
        _scope = 0;
        _interfaceManager.UpdateText(0);
        _previousState = StateGame.NOTHING;
    }

    public void StartGame()
    {
        _gameStart = true;
        UpdateState();
    }

    public void StopGame()
    {
        Restart();
        _gameStart = false;
        SetStateManage(StateGame.NOTHING);
        _player.ResetForMenu();
    }

    public void UpdateScope(int scope)
    {
        _scope += scope;
        _interfaceManager.UpdateText(_scope);
    }
}

public enum StateGame
{
    BLOCKPLATFORM,
    JUMPPLATFORM,
    ENEMY,
    NOTHING
}
