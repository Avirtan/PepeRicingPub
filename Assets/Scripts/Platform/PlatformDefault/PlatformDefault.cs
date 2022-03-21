using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDefault : MonoBehaviour
{
    [SerializeField]
    private GameObject _bonus;
    [SerializeField]
    private List<GameObject> _blocks;
    private int _indexBlock;
    private TypeBlock _typeBlock = TypeBlock.NOTHING;
    public TypeBlock TypeBlock { get { return _typeBlock; } set { _typeBlock = value; } }

    void Start()
    {
        
    }

    public void CreateBlock()
    {
        _indexBlock = Random.Range(0, _blocks.Count);
        _blocks[_indexBlock].SetActive(true);
        _typeBlock = _blocks[_indexBlock].GetComponent<Block>().typeBlock;
        CreateBonus();
    }

    private void CreateBonus()
    {
        var create = Random.Range(0, 5);
        if (create == 1)
        {
            float posX = Random.Range(-2.0f, 2.5f);
            float posZ = Random.Range(0.0f, 17.5f);
            var tmpBonus = Instantiate(_bonus, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            tmpBonus.transform.parent = gameObject.transform;
            tmpBonus.transform.position = new Vector3(transform.position.x + posX, transform.position.y, transform.position.z + posZ);
            // Debug.Log(posX);
            // Debug.Log(posZ);
        }
        // Debug.Log(create);
    }

    public void RemoveBlock()
    {
        _blocks[_indexBlock].SetActive(false);
    }
}

public enum TypeBlock
{
    LEFTBLOCK,
    CENTERBLOCK,
    RIGHTBLOCK,
    BOTTOM,
    NOTHING
}
