using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Device PowerUp:
/// Accelerate *rate1* of power regen and *rate2* of fire speed.
/// </summary>
public class DevicePowerUp : Device
{
    public float lasttime;

    public float ft;
    public bool running;

    public float regenRateBounty = 0.2f;
    public float fireRatebounty = 0.5f;

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
                w.cd *= 1.0f / (1.0f + fireRatebounty);
                player.mpRegen *= 1.0f + regenRateBounty;
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
                    w.cd *= 1.0f + fireRatebounty;
                    player.mpRegen *= 1.0f / (1.0f + regenRateBounty);
                }
            }

            running = false;
        }
        
    }
}
