using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private AudioSource _crush;
    public TypeBlock typeBlock;

    [SerializeField]
    private GameObject _blockCell;
    [SerializeField]
    private GameObject _block;
    [SerializeField]
    private bool _isCrash;

    void Start()
    {
        _isCrash = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        var player = other.gameObject.GetComponent<Character>();
        if (!player || _isCrash) return;
        if (typeBlock == TypeBlock.BOTTOM)
        {
            _crush.Play();
        }
        else
        {
            _block.SetActive(false);
            _blockCell.SetActive(true);
            Vector3 collisionForce = other.impulse / Time.fixedDeltaTime;
            collisionForce /= 100;
            foreach (Rigidbody rb in _blockCell.GetComponentsInChildren<Rigidbody>())
            {
                rb.AddForce(Vector3.forward * 200);
            }
            _crush.Play();
            _isCrash = true;
        }
    }
}
