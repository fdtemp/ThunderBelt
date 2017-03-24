using UnityEngine;
using System.Collections;

public class Shield : SpecObject
{
    public enum ShieldType { Round, Omni };
    public ShieldType shieldType;
    public bool shutdown = false; // Whether the shield is closed.
    public bool broken = false; // Whether the shield can't be raised.
    public bool turnoff = false; // State description whether i should raise the shield.
    public float sp { get { return hp; } set { hp = value; } }
    public float spMax { get { return hpMax; } }
    public float spRegen { get { return hpRegen; } }
    public float resetTime = 5f;
    public float energyCost = 0f; // Opening a shield costs energy every second.
    public SpriteRenderer rd; // Renderer rendering this shield.
    public Collider2D cd; // Collider of this shield.

    float t = 0f;
    float originalAlpha;

    void Start()
    {
        t = 0f;
        if (rd == null)
        {
            rd = this.gameObject.GetComponent<SpriteRenderer>();
            Debug.Log("WARNNING: Renderer not assigned at : " +
                this.gameObject.name + this.gameObject.transform.parent.gameObject.name);
        }
        if (cd == null)
        {
            cd = this.gameObject.GetComponent<Collider2D>();
            Debug.Log("WARNNING: Collider not assigned at : " +
                this.gameObject.name + this.gameObject.transform.parent.gameObject.name);
        }
        originalAlpha = rd.color.a;
    }

    void TryRaiseShield()
    {
        if (!shutdown) return; // No need to operate.
        if (broken) return; // I can't raise the shield.
        shutdown = false;
    }

    void TryCloseShield()
    {
        shutdown = true;
    }

    /// <summary>
    /// It's overrided here to avoid direct change of hp and it's relative variable.
    /// Also redefine the behaviour when hp(sp) is lower then 0.
    /// </summary>
    protected override void Update()
    {
        if (sp <= 0f) // Make shiled broken and shutdown it.
        {
            shutdown = true;
            broken = true;
            t = resetTime;
            sp = 0f;
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
            x.a = originalAlpha;
            rd.color = x;
        }

        if (shieldType == ShieldType.Round)
            this.gameObject.transform.Rotate(0f, 0f, 30f * Time.deltaTime);
        // Effect changing.
        if (shutdown) cd.enabled = false;
        else cd.enabled = true;

    }

    /// <summary>
    /// Use this function in PlayerControl module.
    /// </summary>
    public void SwitchShield()
    {
        turnoff = !turnoff;
    }


    /// <summary>
    /// This function is for dealing with shield-ship or shield-wreckage collides.
    /// </summary>
    /// <param name="x"></param>
    void OnTriggerStay2D(Collider2D x)
    {
        if (x.gameObject.tag != "Enemy" && x.gameObject.tag != "Wreckage") return;
        
        // Colliding damage is defined here.
        float dmgps = 1500;
        float dmg = dmgps * Time.deltaTime;
        sp -= dmg;
        SpecObject s = x.gameObject.GetComponent<SpecObject>();
        if (s != null)
        {
            s.RecieveDamage(DamageType.Energy, dmg);
        }

        // **No physics effects yet.**

        // Shuold have some special FX when colliding.
    }
}
