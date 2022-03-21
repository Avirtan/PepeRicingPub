using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject _areaAttack;
    private void AttackEnemy()
    {
        _areaAttack.SetActive(true);
    }

    private void DisableAttack()
    {
        _areaAttack.SetActive(false);
    }
}
