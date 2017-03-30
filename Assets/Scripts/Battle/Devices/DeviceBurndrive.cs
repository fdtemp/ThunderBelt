using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Device Burndrive:
/// Set acceleration to a multiply.
/// Set max speed to a multiply.
/// </summary>
public class DeviceBurndrive : Device
{
    public float lasttime;

    public float ft;
    public bool running;

    public float acceBounty = 3.0f;
    public float speedBounty = 1.0f;

    public override void Launch()
    {
        ft = lasttime;
        running = true;
        player.acceBack *= 1 + acceBounty;
        player.acceFront *= 1 + acceBounty;
        player.acceGlide *= 1 + acceBounty;
        player.maxSpeedBack *= 1 + speedBounty;
        player.maxSpeedFront *= 1 + speedBounty;
        player.maxSpeedGlide *= 1 + speedBounty;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        ft -= Time.fixedDeltaTime;
        if (ft < 0f) ft = 0f;

        if (ft == 0f && running)
        {
            running = false;
            player.acceBack /= 1 + acceBounty;
            player.acceFront /= 1 + acceBounty;
            player.acceGlide /= 1 + acceBounty;
            player.maxSpeedBack /= 1 + speedBounty;
            player.maxSpeedFront /= 1 + speedBounty;
            player.maxSpeedGlide /= 1 + speedBounty;
        }
    }
    
}
