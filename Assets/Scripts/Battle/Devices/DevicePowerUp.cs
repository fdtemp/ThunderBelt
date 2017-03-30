using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Device PowerUp:
/// Accelerate 20% of power regen and 50% of fire speed.
/// </summary>
public class DevicePowerUp : Device
{
    public float lasttime;

    float ft;
    bool running;

    public override void Launch()
    {
        // Find weapons from UI canvas...
        int cc = this.transform.parent.childCount;
        for (int i = 0; i < cc; i++)
        {
            GameObject x = this.transform.parent.GetChild(i).gameObject;
            if (x.name.Contains("weapon") || x.name.Contains("Weapon"))
            {
                Weapon w = x.GetComponent<Weapon>();
                w.cd *= 0.5f;
            }
        }

        ft = lasttime;
        running = true;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        ft -= Time.fixedDeltaTime;
        if (ft < 0f) ft = 0f;
        
        if (ft == 0f && running)
        {
            // Find weapons from UI canvas...
            int cc = this.transform.parent.childCount;
            for (int i = 0; i < cc; i++)
            {
                GameObject x = this.transform.parent.GetChild(i).gameObject;
                if (x.name.Contains("weapon") || x.name.Contains("Weapon"))
                {
                    Weapon w = x.GetComponent<Weapon>();
                    w.cd *= 2f;
                }
            }

            running = false;
        }
        
    }
}
