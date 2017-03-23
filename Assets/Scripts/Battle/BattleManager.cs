using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    /// <summary>
    ///  this param is here for a faster fetch.
    ///  Do not make enemies and ally ships do like this.
    /// </summary>
    public static GameObject playerObject;


    /// <summary>
    /// Normally:
    /// 0, 1, 2, 3 : Devices(Skills).
    /// 4, 5, 6, 7 : Weapons.
    /// One weapon groups may have multiple weapons.
    /// </summary>
    public static GameObject[][] playerDevices;

    void Start()
    {
        playerObject = GameObject.FindObjectOfType<Player>().gameObject;
    }
}
