using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    private IndicatorManager _indicatorManager;
    [SerializeField]
    private List<PlatformDefault> _platformDefaults;
    [SerializeField]
    private PlatformJump _platformJump;
    [SerializeField]
    private GameObject _startPlatform;
    [SerializeField]
    private float _destroyPlatformPositionZ;
    [SerializeField]
    private Queue<GameObject> _platforms;
    public Queue<GameObject> Platforms { get { return _platforms; } }
    [SerializeField]
    private GameObject _oldPlatform;
    private GameObject _newPlatform;
    private float _lenPlatform;
    [SerializeField]
    private float _speedPlatform;
    [SerializeField]
    private float _startSpeedPlatform;
    [SerializeField]
    private int _countPlatform = 4;
    private StateGame _state;
    private bool _isDebug;
    private int _jumpCreateCount;
    public int JumpCreateCound { get { return _jumpCreateCount; } set { _jumpCreateCount = value; } }
    public bool IsDebug { set { _isDebug = value; } }
    public StateGame State { get { return _state; } set { _state = value; } }

    void Start()
    {
        _platforms = new Queue<GameObject>();
        _lenPlatform = 24.26f;
        _startPlatform.GetComponent<Platform>().Speed = _speedPlatform;
        _platforms.Enqueue(_startPlatform);
        for (int i = 1; i < _countPlatform; i++)
        {
            int rand = Random.Range(0, _platformDefaults.Count);
            _newPlatform = GeneratePlatform(_platformDefaults[rand].gameObject, new Vector3(0, 0, i * _lenPlatform));
        }
        _oldPlatform = _platforms.Peek();
    }

    void Update()
    {
        UpdatePlatforms();
    }

    private void UpdatePlatforms()
    {
        if (_oldPlatform.transform.position.z <= _destroyPlatformPositionZ)
        {
            Destroy(_platforms.Dequeue());
            _oldPlatform = _platforms.Peek();
            Vector3 position = new Vector3(0, 0, _newPlatform.transform.position.z + _lenPlatform);
            _newPlatform = CreatePlatform(position);
        }
    }

    private void ResetPlatform()
    {
        _platforms = new Queue<GameObject>();
        for (int i = 0; i < _countPlatform; i++)
        {
            int rand = Random.Range(0, _platformDefaults.Count);
            _newPlatform = GeneratePlatform(_platformDefaults[rand].gameObject, new Vector3(0, 0, i * _lenPlatform));
        }
        _oldPlatform = _platforms.Peek();
    }

    private GameObject CreatePlatform(Vector3 position)
    {
        GameObject tmpObj = null;
        switch (_state)
        {
            case StateGame.BLOCKPLATFORM:
            case StateGame.ENEMY:
                tmpObj = CreatePlatformWithBlocks(position);
                break;
            // case StateGame.ENEMY:
            //     tmpObj = CreateDefaultPlatform(position);
            //     break;
            case StateGame.JUMPPLATFORM:
                tmpObj = CreateJumpPlatform(position);
                break;
            default:
                tmpObj = CreateDefaultPlatform(position);
                break;
        }
        return tmpObj;
    }

    private GameObject CreatePlatformWithBlocks(Vector3 position)
    {
        int rand = Random.Range(0, _platformDefaults.Count);
        var tmpPlatform = GeneratePlatform(_platformDefaults[rand].gameObject, position).GetComponent<PlatformDefault>();
        tmpPlatform.CreateBlock();
        _indicatorManager.AddPlatform(tmpPlatform);
        return tmpPlatform.gameObject;
    }

    private GameObject CreateJumpPlatform(Vector3 position)
    {
        var emptyPlatform = CreateDefaultPlatform(position).GetComponent<PlatformDefault>();
        Vector3 tmpPosition = new Vector3(0, 0, emptyPlatform.transform.position.z + _lenPlatform);
        var jumpPlatform = GeneratePlatform(_platformJump.gameObject, tmpPosition).GetComponent<PlatformJump>();
        jumpPlatform.EnableJump(_isDebug);
        _jumpCreateCount += 1;
        emptyPlatform.TypeBlock = jumpPlatform.TypeBlock;
        _indicatorManager.AddPlatform(emptyPlatform, 1);
        tmpPosition = new Vector3(0, 0, jumpPlatform.transform.position.z + _lenPlatform);
        emptyPlatform = CreateDefaultPlatform(tmpPosition).GetComponent<PlatformDefault>();
        return emptyPlatform.gameObject;
    }

    private GameObject CreateDefaultPlatform(Vector3 position)
    {
        int rand = Random.Range(0, _platformDefaults.Count);
        var tmpPlatform = GeneratePlatform(_platformDefaults[rand].gameObject, position);
        return tmpPlatform;
    }

    private GameObject GeneratePlatform(GameObject platform, Vector3 position)
    {
        var tmpPlatform = Instantiate(platform, position, Quaternion.identity);
        tmpPlatform.GetComponent<Platform>().Speed = _speedPlatform;
        _platforms.Enqueue(tmpPlatform);
        return tmpPlatform;
    }

    public void UpdateSpeed(float speed)
    {
        var tmpPlatforms = _platforms.ToArray();
        _speedPlatform += speed;
        for (int i = 0; i < tmpPlatforms.Length; i++)
        {
            tmpPlatforms[i].GetComponent<Platform>().Speed = _speedPlatform;

        }
    }

    public void StopPlatform()
    {
        UpdateSpeed(-_speedPlatform);
    }

    private void RemoveBlocks()
    {
        var tmpPlatforms = _platforms.ToArray();
        for (int i = 0; i < tmpPlatforms.Length; i++)
        {
            var tmpPlatform = tmpPlatforms[i];
            if (tmpPlatform.GetComponent<PlatformDefault>())
            {
                tmpPlatform.GetComponent<PlatformDefault>().RemoveBlock();
            }
            Destroy(tmpPlatforms[i].gameObject);
        }
    }

    public void Reset()
    {
        _indicatorManager.Reset();
        RemoveBlocks();
        ResetPlatform();
        UpdateSpeed(_startSpeedPlatform);
    }
}
