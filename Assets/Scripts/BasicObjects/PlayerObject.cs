using UnityEngine;
using System.Collections;

/// <summary>
/// Mount on player plane or ship.
/// Defines All properties.
/// </summary>
public class PlayerObject : SpecObject
{
    public float acceFront = 1.0f; // Acceleration front.
    public float acceBack = 0.5f; // Acceleration back.
    public float acceGlide = 2.0f; // Acceleration of left moving or right moving.
    public float maxSpeedFront = 1.0f;
    public float maxSpeedBack = 1.0f;
    public float maxSpeedGlide = 1.0f;

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

	void Awake()
	{
	    
	}
	
	void Start() 
	{
	    
	}
	
	protected override void Update() 
	{
        // Do the regen and hp control.
        base.Update();

        if (!shield.shutdown)
        {
            
        }

        mp += mpRegen * Time.deltaTime;
        if (mp >= mpMax) mp = mpMax;


    }
	
	void LateUpdate()
	{
	    
	}
}
