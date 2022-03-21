using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformJump : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftJump;
    [SerializeField]
    private GameObject _centerJump;
    [SerializeField]
    private GameObject _rightJump;
    private TypeBlock _typeBlock = TypeBlock.NOTHING;
    public TypeBlock TypeBlock { get { return _typeBlock; } set { _typeBlock = value; } }


    public void EnableJump(bool isDebug = false)
    {
        if (isDebug)
        {
            _typeBlock = TypeBlock.LEFTBLOCK;
            _leftJump.SetActive(true);
            return;
        }
        var rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                _typeBlock = TypeBlock.LEFTBLOCK;
                _leftJump.SetActive(true);
                break;
            case 1:
                _typeBlock = TypeBlock.CENTERBLOCK;
                _centerJump.SetActive(true);
                break;
            case 2:
                _typeBlock = TypeBlock.RIGHTBLOCK;
                _rightJump.SetActive(true);
                break;
        }
    }

}
