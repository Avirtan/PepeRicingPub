using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private bool _isJump;

    void Start() {
        _isJump = true;    
    }

    private void OnCollisionEnter(Collision other)
    {
        var palyer = other.gameObject.GetComponent<Character>();
        if (!palyer || !_isJump) return;
        palyer.GetComponent<Rigidbody>().AddForce((Vector3.up + Vector3.forward) * 400, ForceMode.Acceleration);
        _isJump = false;
    }
}
