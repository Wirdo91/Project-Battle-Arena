using UnityEngine;
using System.Collections;

public enum AttackType
{
    DRAW,           //Bpw
    SWING,          //Sword
    STAP            //Spear
}

public class IWeapon : MonoBehaviour
{

    float _pushBack;

    public float PushBack
    {
        get { return _pushBack; }
    }

    AttackType _attackType;

    public AttackType AttackType
    {
        get { return _attackType; }
    }
}
