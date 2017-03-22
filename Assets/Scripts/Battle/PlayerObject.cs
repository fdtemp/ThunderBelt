﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Mount on player plane or ship.
/// Defines All properties.
/// </summary>
public class PlayerObject : SpecObject
{
    public float acceFront = 1.5f; // Acceleration front.
    public float acceBack = 1.0f; // Acceleration back.
    public float acceGlide = 1.5f; // Acceleration of left moving or right moving.
    public float maxSpeedFront = 2.0f;
    public float maxSpeedBack = 1.5f;
    public float maxSpeedGlide = 2.0f;
    
    public float mpMax = 1f;
    public float mp = 1f; // Energy to shoot and use skills.
    public float mpRegen = 0f;
    
    public Weapon[] weapons; // Weapons assignment.
    public Device[] devices; // Skills assignment.

    // Shield properties is here.
    public Shield shield;
    public float sp { get { return shield.sp; } }
    public float spMax { get { return shield.spMax; } }
    public float spRegen { get { return shield.spRegen; } }

    public Vector2 v; // Velocity instead of RigidBody.velocity.
	
	protected override void Update() 
	{
        // Ship properties update...
        if (shield != null)
        {
            if (!shield.shutdown) mp -= shield.energyCost * Time.deltaTime;
        }
        mp += mpRegen * Time.deltaTime;
        if (mp >= mpMax) mp = mpMax;

        // Player ship/plane control...
        Vector2 a; // Acceleration at this frame.
        bool lc = Input.GetKey(KeyCode.LeftArrow);
        bool rc = Input.GetKey(KeyCode.RightArrow);
        bool bc = Input.GetKey(KeyCode.DownArrow);
        bool fc = Input.GetKey(KeyCode.UpArrow);
        if (lc && !rc) // Accelerate to the left.
            a.x = -acceGlide;
        else if (!lc && rc) // Accelerate to the right.
            a.x = acceGlide;
        else // Try to stop glide.
            a.x = (v.x != 0f ? -Mathf.Sign(v.x) * acceGlide : 0f);

        if (bc && !fc) // Accelerate to the back.
            a.y = -acceBack;
        else if (!bc && fc) // Accelerate to the front.
            a.y = acceFront;
        else // Try to stop glide.
            a.y = v.y > 0f ? -acceBack : v.y < 0f ? acceFront : 0f;

        Vector2 dv = a * Time.deltaTime; // Delta v.

        if (dv.x + v.x > maxSpeedGlide) dv.x = maxSpeedGlide - v.x;
        if (dv.x + v.x < -maxSpeedGlide) dv.x = -maxSpeedGlide - v.x;

        if (dv.y + v.y > maxSpeedFront) dv.y = maxSpeedFront - v.y;
        if (dv.y + v.y < -maxSpeedBack) dv.y = -maxSpeedBack - v.y;

        this.gameObject.transform.Translate(dv * Time.deltaTime * 0.5f + v * Time.deltaTime);
        v += dv;


        // Player Shield Control...
        if (shield != null)
        {
            if (Input.GetKeyDown(KeyCode.R)) shield.SwitchShield();
        }
    }
}