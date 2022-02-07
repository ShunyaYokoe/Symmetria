using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable/Create PlayerData")]
public class PlayerData : ScriptableObject
{
    /// <summary>
    /// 移動加速度
    /// </summary>
    public float walkforce = 1000.0f;

    /// <summary>
    /// 最高速度
    /// </summary>
    public float maxwalkspeed = 1.5f;
}
