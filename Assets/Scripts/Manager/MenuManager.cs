using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Manager _manager;
    [SerializeField]
    private GameObject _startCamera;
    [SerializeField]
    private GameObject _endCamera;
    [SerializeField]
    private GameObject _inputUi;
    [SerializeField]
    private GameObject _menuStart;
    [SerializeField]
    private GameObject _endPositionCamera;
    [SerializeField]
    private GameObject _startPositionCamera;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private bool _startGame;

    [SerializeField]
    private bool _gameIsStart;


    void Start()
    {
        _inputUi.SetActive(false);
        _gameIsStart = false;
        if (_manager.IsDebug) DebugStartGame();
    }

    public void StartGame()
    {
        _startCamera.transform.position = _startPositionCamera.transform.position;
        _startCamera.transform.rotation = _startPositionCamera.transform.rotation;
        _menuStart.SetActive(false);
        _startGame = true;
    }

    public void StopGame()
    {
        _manager.StopGame();
        _inputUi.SetActive(false);
        // _menuStart.SetActive(true);
        _startGame = false;
    }

    private void DebugStartGame()
    {
        _menuStart.SetActive(false);
        _endCamera.SetActive(true);
        _startCamera.SetActive(false);
        _inputUi.SetActive(true);
        _gameIsStart = true;
        _startGame = true;
        _manager.StartGame();
    }

    void Update()
    {
        if (!_gameIsStart && _startGame)
        {
            if (_startCamera.transform.position.x < _endPositionCamera.transform.position.x)
            {
                _startCamera.transform.position = Vector3.Lerp(_startCamera.transform.position, _endPositionCamera.transform.position, Time.deltaTime * _speed);
                _startCamera.transform.rotation = Quaternion.Lerp(_startCamera.transform.rotation, _endPositionCamera.transform.rotation, Time.deltaTime * _speed);
            }
            if (_startCamera.transform.position.x >= _endPositionCamera.transform.position.x - 0.1f)
            {
                // _endCamera.SetActive(true);
                // _startCamera.SetActive(false);
                _inputUi.SetActive(true);
                _gameIsStart = true;
                _manager.StartGame();
            }
        }
        if (_gameIsStart && !_startGame)
        {
            if (_startCamera.transform.position.x > 1.2f)
            {
                _startCamera.transform.position = Vector3.Lerp(_startCamera.transform.position, _startPositionCamera.transform.position, Time.deltaTime * _speed);
                _startCamera.transform.rotation = Quaternion.Lerp(_startCamera.transform.rotation, _startPositionCamera.transform.rotation, Time.deltaTime * _speed);
            }
            if (_startCamera.transform.position.x <= 1.99f)
            {
                _gameIsStart = false;
                _menuStart.SetActive(true);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
