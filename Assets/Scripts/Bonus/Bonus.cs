using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private bool _isUse = false;
    [SerializeField]
    private int _scope;

    void Start()
    {
        _scope = 10;
    }


    public int GetScope()
    {
        if (_isUse) _scope = 0;
        else
        {
            _isUse = true;
            _audio.Play();
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
        return _scope;
    }
}
