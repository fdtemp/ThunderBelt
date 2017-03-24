using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This script need to be mounted to the Main Camera.
/// Read data from Global and set player wing to correct state.
/// </summary>
public class BattleManager : MonoBehaviour
{
    /// <summary>
    ///  this param is here for a faster fetch.
    ///  Do not make enemies and ally ships do like this.
    /// </summary>
    public static GameObject player;
    
    void Start()
    {
        Player pl = FindObjectOfType<Player>();
        if (pl != null) player = pl.gameObject;
        else player = Instantiate(Global.wing);
        player.transform.position = new Vector2(0f, -3f);

        GameObject[] c = new GameObject[player.transform.childCount];
        for (int i = 0; i < c.Length; i++) c[i] = player.transform.GetChild(i).gameObject;

        // In canvas, there should be something named Weaponx and Devicex as locator.
        // These locators will have renderer and scripts the same as scripts.
        GameObject[] dev = new GameObject[4];
        GameObject[] wep = new GameObject[4];
        // Might be null if not assigned.
        for (int i = 0; i < 4; i++)
        {
            dev[i] = GameObject.Find("Device" + i.ToString());
            wep[i] = GameObject.Find("Weapon" + i.ToString());
            if (dev[i] == null) Debug.Log("WARNING: device locator not assigned : " + i.ToString());
            if (wep[i] == null) Debug.Log("WARNING: weapon locator not assigned : " + i.ToString());
        }

        // Weapons and devices assignment...
        // These should be done *after* playerObject is assigned.
        // Only one weapon object (within canvas) and one weapon script can be installed.
        KeyCode[] code = new KeyCode[] { KeyCode.F, KeyCode.D, KeyCode.S, KeyCode.A };
        for (int i = 0; i <= 3; i++)
        {
            if (Global.weapons[i] == null) break; // Not assigned.
            wep[i] = Instantiate(Global.weapons[i]);
            Debug.Log("TODO: Assign weapons to the correct locations.");
            Weapon w = wep[i].GetComponent<Weapon>();
            // Count the weapon mounts.
            // Notice if there has no mount objcet the weapon can not be launch properly.
            int cnt = 0;
            for (int d = 0; d < c.Length; d++)
                if (c[d].name == "weaponMount" + i.ToString()) cnt++;
            w.fireLocator = new GameObject[cnt];
            for (int d = 0; d < cnt; d++)
                if (c[d].name == "weaponMount" + i.ToString())
                    w.fireLocator[--cnt] = c[d];
            w.keyBind = code[i];
            w.player = player.GetComponent<Player>();
            
        }
        code = new KeyCode[] { KeyCode.R, KeyCode.E, KeyCode.W, KeyCode.Q };
        for (int i = 0; i <= 3; i++)
        {
            if (Global.weapons[i] == null) break; // Not assigned.
            dev[i] = Instantiate(Global.devices[i]);
            Debug.Log("TODO: Assign devices to the correct locations.");
            Device v = dev[i].GetComponent<Device>();
            // Devices do not have fire locator.
            v.keyBind = code[i];
            v.player = player.GetComponent<Player>();
        }

    }
}
