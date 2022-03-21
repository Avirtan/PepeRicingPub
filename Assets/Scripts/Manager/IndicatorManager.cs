using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _prviewPlanes;
    [SerializeField]
    private Queue<DateIndicator> _platformsForIndicator;
    // [SerializeField]
    // private Queue<int> _typeIndicators;
    // public DateIndicator[] PlatformDefaults { get { return _platformsForIndicator.ToArray(); } }
    [SerializeField]
    private float _hideIndicator = -4;
    [SerializeField]
    private TypeBlock _typeIndicatorShow;
    [SerializeField]
    private List<Material> _materials;
    void Start()
    {
        _platformsForIndicator = new Queue<DateIndicator>();
        // _typeIndicators = new Queue<int>();
    }

    private void Update()
    {
        UpdateIndicator();
    }

    void UpdateIndicator()
    {
        if (_platformsForIndicator.Count > 0)
        {
            var platform = _platformsForIndicator.Peek().platform;
            var typeIndicator = _platformsForIndicator.Peek().idMaterial;
            if (platform.transform.position.z < 13)
            {
                _typeIndicatorShow = platform.TypeBlock;
                ShowIndicator(true, typeIndicator);
            }
            if (platform.transform.position.z < _hideIndicator)
            {
                _platformsForIndicator.Dequeue();
                // _typeIndicators.Dequeue();
                ShowIndicator(false, typeIndicator);
            }
        }
    }

    private void ShowIndicator(bool enabled, int typeIndicator = 0)
    {
        switch (_typeIndicatorShow)
        {
            case TypeBlock.LEFTBLOCK:
                _prviewPlanes[0].SetActive(enabled);
                SetMaterial(_prviewPlanes[0], typeIndicator);
                break;
            case TypeBlock.CENTERBLOCK:
                _prviewPlanes[1].SetActive(enabled);
                SetMaterial(_prviewPlanes[1], typeIndicator);
                break;
            case TypeBlock.RIGHTBLOCK:
                _prviewPlanes[2].SetActive(enabled);
                SetMaterial(_prviewPlanes[2], typeIndicator);
                break;
            case TypeBlock.BOTTOM:
                _prviewPlanes[3].SetActive(enabled);
                // _prviewPlanes[1].SetActive(enabled);
                // _prviewPlanes[2].SetActive(enabled);
                // SetMaterial(_prviewPlanes[0], typeIndicator);
                // SetMaterial(_prviewPlanes[1], typeIndicator);
                SetMaterial(_prviewPlanes[3], typeIndicator);
                break;
        }
    }

    private void SetMaterial(GameObject plane, int idMaterial = 0)
    {
        var render = plane.GetComponent<MeshRenderer>();
        render.material = _materials[idMaterial];
    }

    public void AddPlatform(PlatformDefault platform, int typeIndicator = 0)
    {
        _platformsForIndicator.Enqueue(new DateIndicator(platform, typeIndicator));
        // _typeIndicators.Enqueue(typeIndicator);
    }

    public void Reset()
    {
        _platformsForIndicator = new Queue<DateIndicator>();
        // _typeIndicators = new Queue<int>();
        ShowIndicator(false);
    }
}

struct DateIndicator
{
    public PlatformDefault platform;
    public int idMaterial;

    public DateIndicator(PlatformDefault platform, int idMaterial){
        this.platform = platform;
        this.idMaterial = idMaterial;
    }
}
