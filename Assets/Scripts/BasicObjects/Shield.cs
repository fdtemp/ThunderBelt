using UnityEngine;
using System.Collections;

public class Shield : SpecObject
{
    //public enum ShieldType { Round, Omni };
    //public ShieldType shieldType;
    public bool shutdown = false; // Whether the shield is closed.
    public bool broken = false; // Whether the shield can't be raise.
    public bool turnoff = false; // State description whether i should raise the shield.
    public float sp { get { return hp; } set { hp = value; } }
    public float spMax { get { return hpMax; } }
    public float spRegen = 10f;
    public float resetTime = 5f;
    public float energyPerSec = 0f; // Opening a shield costs energy every second.
    public SpriteRenderer rd; // Renderer rendering this shield.

    float t = 0f;

    void Start() { t = 0f; }

    void TryRaiseShield()
    {
        if (!shutdown) return; // No need to operate.
        if (broken) return; // I can't raise the shield.
        shutdown = true;
    }

    void TryCloseShield()
    {
        shutdown = true;
    }

    /// <summary>
    /// It's overrided here to avoid direct change of hp and it's relative variable.
    /// </summary>
    protected override void Update()
    {
        if (sp <= 0f) // Make shiled broken and shutdown it.
        {
            shutdown = true;
            broken = true;
            t = resetTime;
        }

        sp += spRegen * Time.deltaTime;
        if (sp > spMax) sp = spMax;
        
        if (turnoff) TryCloseShield();
        else TryRaiseShield();
        
        // Do the shield reparing.
        if (broken)
        {
            t -= Time.deltaTime;
            if (t < 0f) t = 0f;
            if (t == 0f)
            {
                broken = false;
            }
        }

        // Vision changing.
        if (shutdown)
        {
            Color x = rd.color;
            x.a = 0f;
            rd.color = x;
        }
        else
        {
            Color x = rd.color;
            x.a = 1f;
            rd.color = x;
        }
    }

    /// <summary>
    /// Use this function in PlayerControl module.
    /// </summary>
    public void SwitchShield()
    {
        turnoff = !turnoff;
    }
}
