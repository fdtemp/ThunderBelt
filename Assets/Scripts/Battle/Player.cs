using UnityEngine;
using System.Collections;

/// <summary>
/// Mount on player plane or ship.
/// Defines All properties.
/// </summary>
public class Player : SpecObject
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

    public GameObject[] weaponLocator; // Locations where weapons mount.

    public GameObject[] targets;

    public bool shotdown = false;

    // Shield properties is here.
    public Shield shield;
    public float sp { get { return shield.sp; } }
    public float spMax { get { return shield.spMax; } }
    public float spRegen { get { return shield.spRegen; } }

    Rigidbody2D rd;
    public Vector2 v; // Velocity instead of RigidBody.velocity.
    //public Vector2 v { get { return rd.velocity; } set { rd.velocity = value; } }

    void Start()
    {
        rd = this.gameObject.GetComponent<Rigidbody2D>();
    }

    public bool lc, rc, bc, fc;
    protected override void FixedUpdate()
    {
        // Jedge if killed and HP regen.
        base.FixedUpdate();

        // Ship properties update...
        if (!shield.shutdown) mp -= shield.energyCost * Time.fixedDeltaTime;
        
        mp += mpRegen * Time.fixedDeltaTime;
        if (mp >= mpMax) mp = mpMax;

        // Player ship/plane control...
        Vector2 a; // Acceleration at this frame.
        lc = Input.GetKey(KeyCode.LeftArrow);
        rc = Input.GetKey(KeyCode.RightArrow);
        bc = Input.GetKey(KeyCode.DownArrow);
        fc = Input.GetKey(KeyCode.UpArrow);
        bool leftedge = Camera.main.WorldToViewportPoint(this.gameObject.transform.position).x < 0.0f;
        bool rightedge = Camera.main.WorldToViewportPoint(this.gameObject.transform.position).x > 1.0f;
        bool bottomedge = Camera.main.WorldToViewportPoint(this.gameObject.transform.position).y < 0.0f;
        bool topedge = Camera.main.WorldToViewportPoint(this.gameObject.transform.position).y > 1.0f;

        if (lc && !rc) a.x = -acceGlide; // Accelerate to the left.
        else if (!lc && rc) a.x = acceGlide; // Accelerate to the right.
        else // Try to stop glide.
        {
            a.x = v.x > 0f ? -acceGlide : v.x < 0f ? acceGlide : 0f;
            float va = Mathf.Abs(v.x);
            a.x = Mathf.Min(a.x, va / Time.fixedDeltaTime);
            a.x = Mathf.Max(a.x, -va / Time.fixedDeltaTime);
        }

        if (bc && !fc) a.y = -acceBack; // Accelerate to the back.
        else if (!bc && fc) a.y = acceFront; // Accelerate to the front.
        else // Try to stop glide.
        {
            a.y = v.y > 0f ? -acceBack : v.y < 0f ? acceFront : 0f;
            float va = Mathf.Abs(v.y);
            a.y = Mathf.Min(a.y, va / Time.fixedDeltaTime);
            a.y = Mathf.Max(a.y, -va / Time.fixedDeltaTime);
        }

        Vector2 dv = a * Time.fixedDeltaTime; // Delta v.

        v += dv;

        // Speed limit.
        if (v.x > maxSpeedGlide) v = new Vector2(maxSpeedGlide, v.y);
        if (v.x < -maxSpeedGlide) v = new Vector2(-maxSpeedGlide, v.y);
        if (v.y > maxSpeedFront) v = new Vector2(v.x, maxSpeedFront);
        if (v.y < -maxSpeedBack) v = new Vector2(v.x, -maxSpeedBack);
        
        // Edge limit.
        if (leftedge) { v.x = Mathf.Max(v.x, 0.0f); dv.x = Mathf.Max(dv.x, 0f); }
        if (rightedge) { v.x = Mathf.Min(v.x, 0.0f); dv.x = Mathf.Min(dv.x, 0f); }
        if (bottomedge) { v.y = Mathf.Max(v.y, 0.0f); dv.y = Mathf.Max(dv.y, 0f); }
        if (topedge) { v.y = Mathf.Min(v.y, 0.0f); dv.y = Mathf.Min(dv.y, 0f); }

        // Attribute "Simulated" of rigidBody is disabled !!!
        // And this is for avoiding some bugs.
        this.gameObject.transform.position += (Vector3)(dv * 0.5f + v) * Time.fixedDeltaTime;

    }

    /// <summary>
    /// Defines what to do when player plane is taken down.
    /// Normally make a failed tag, destroy ships, and so on.
    /// </summary>
    protected override void OnKilled()
    {
        if (!shotdown)
        {
            shotdown = true;

            // Create a failed flash...
            // Audio is binded with light objects
            for (int i = 0; i < deadFX.Length; i++)
            {
                GameObject x = Instantiate(deadFX[i]);
                x.transform.position = this.gameObject.transform.position;
            }

            // Notice we don't destroy this object directly,
            // to avoid a large number of null-reference error.
            //Destroy(this.gameObject);
            this.gameObject.transform.position = new Vector3(0f, -999999999f, 0f);
        }
    }

    void OnTriggerStay2D(Collider2D x)
    {
        if (x.gameObject.tag != "Enemy" && x.gameObject.tag != "Wreckage") return;
        if (!shield.shutdown) return; // Shield will protect it from collide.

        // Colliding damage is defined here.
        float dmgps = 3000;
        float dmg = dmgps * Time.fixedDeltaTime;
        hp -= dmg; // *Not* RevieveDamage.
        SpecObject s = x.gameObject.GetComponent<SpecObject>();
        if (s != null)
        {
            s.RecieveDamage(DamageType.Energy, dmg * 2f);
            v = (Vector2)(this.gameObject.transform.position -
                x.gameObject.transform.position).normalized * 2f;

        }

        // **No physics effects yet.**
    }
}
