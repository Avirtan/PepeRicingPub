using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField]
    private Text _textScope;


    public void UpdateText(int scope){
        _textScope.text = string.Format("{0}", scope);
    } 
}
