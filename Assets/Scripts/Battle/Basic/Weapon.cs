using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Player weapons. Do not use for enemies.
/// </summary>
abstract public class Weapon : Device
{
    public GameObject source = null; // Cannonball source.
    public GameObject[] fireLocator = null;

    /// <summary>
    /// Deal with launching cannonball and missiles.
    /// Those mp decreasing and cd is deat *after* this function returns.
    /// </summary>
    public override void Launch()
    {
        for (int i = 0; i < fireLocator.Length; i++)
        {
            GameObject x = Instantiate(source);
            Cannonball c = x.GetComponent<Cannonball>();
            if (c == null)
            {
                Debug.Log("WARNNING: Cannonball-source is not valid : " + this.gameObject.name);
                return;
            }

            x.transform.position = fireLocator[i].transform.position;
            x.tag = "AllyCannonball";
        }
    }
}
