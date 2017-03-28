using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic cannonball class. Providing a interface-like class.
/// </summary>
abstract public class Cannonball : MonoBehaviour 
{
    public float damage = 0f;
    public float lifeTime = 20f;
    public float speed = 1.0f;
    public DamageType damageType = DamageType.Energy;
    public GameObject[] explodeFX;
    public GameObject[] explodeParticles;

    float t = 0f;
    bool hitted = false;

    void Update()
    {
        int layer = (this.gameObject.tag.Contains("Ally") || this.gameObject.tag == "Player") ? 
            LayerMask.GetMask("EnemyObject"): LayerMask.GetMask("PlayerObject"); // Binary mask!

        Vector2 dir;
        {
            Vector3 dd; float al;
            this.gameObject.transform.rotation.ToAngleAxis(out al, out dd);
            if (dd.z < 0) { dd = -dd; al = -al; }
            al += 90;
            dir.x = Mathf.Cos(al * Mathf.Deg2Rad);
            dir.y = Mathf.Sin(al * Mathf.Deg2Rad);
        }
        
        RaycastHit2D rc = Physics2D.Raycast(this.gameObject.transform.position, dir, 1e4f, layer);
        
        if (rc.collider != null && rc.distance < speed * Time.deltaTime * 1.1f)
        {
            if (rc.collider.GetComponent<SpecObject>() != null)
                Hit(rc.collider, rc.point);
        }
    }

    void FixedUpdate()
    { 
        t += Time.fixedDeltaTime;
        if (t > lifeTime) Destroy(this.gameObject);
        Move();
    }

    /// <summary>
    /// The Default moving function.
    /// Do the direct moving depends on speed.
    /// </summary>
    virtual protected void Move()
    {
        this.gameObject.transform.Translate(0f, speed * Time.fixedDeltaTime, 0f);
    }

    virtual public void SetTarget(GameObject[] target) // For tracing missiles and projectiles.
    {
        // Leave it do nothing...
    }

    void OnTriggerEnter2D(Collider2D x)
    { Hit(x, this.gameObject.transform.position); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="loc">The correct hit location.</param>
    void Hit(Collider2D x, Vector2 loc)
    {
        GameObject t = x.gameObject;

        // Should do nothing with the friendly.
        if (this.gameObject.tag == "AllyCannonball")
        {
            if (t.tag == "Ally" || t.tag == "AllyCannonball" | t.tag == "Player") return;
        }
        else if (this.gameObject.tag == "EnemyCannonball")
        {
            if (t.tag == "Enemy" || t.tag == "EnemyCannonball") return;
        }
        else if(this.gameObject.tag != "UsedCannonball")
        {
            Debug.Log("WARNING: Cannonball tag is not set properly : " + this.gameObject.name);
            return;
        }

        if (hitted) return; // This gameobjcet will be destroyed when timeout in FixedUpdate.


        // Notice that all *can-be-hit* cannonball & missiles *should* mount a SpecObject.
        SpecObject s = t.GetComponent<SpecObject>();
        if (s == null) return;

        s.RecieveDamage(damageType, damage);

        this.GetComponent<AudioSource>().Play();

        // Not to destroy this object immediatly for playing sounds.
        //Destroy(this.gameObject);
        Collider2D cd = this.gameObject.GetComponent<Collider2D>();
        if (cd != null) Destroy(cd);
        SpriteRenderer rd = this.gameObject.GetComponent<SpriteRenderer>();
        if (rd != null) Destroy(rd);
        this.gameObject.tag = "UsedCannonball";
        hitted = true;

        // FX objects...
        if (explodeFX != null)
        {
            // Maintain direction...
            Vector3 dir;
            float a;
            this.gameObject.transform.rotation.ToAngleAxis(out a, out dir);
            if (dir.z < 0) { dir = -dir; a = -a; }
            Vector2 vdir;
            vdir.y = Mathf.Min(Mathf.Cos(a * Mathf.Deg2Rad) * speed, 0f);
            vdir.x = -Mathf.Sin(a * Mathf.Deg2Rad) * speed;
            if (vdir.magnitude > Global.flySpeed * 0.5f)
                vdir = vdir.normalized * Global.flySpeed * 0.5f;

            // Create and assign fx objects...
            for (int i = 0; i < explodeFX.Length; i++)
            {
                GameObject fx = Instantiate(explodeFX[i]);
                fx.transform.position = loc;
                Flasher fl = fx.GetComponent<Flasher>();
                if (fl != null) fl.baseSpeed = vdir;
            }
        }

        // particle objects...
        if (explodeParticles != null)
        {
            for (int i = 0; i < explodeParticles.Length; i++)
            {
                GameObject pc = Instantiate(explodeParticles[i]);
                pc.transform.position = loc;
                // Turn the color to red as enemies' ship damaged.

                SpriteRenderer prd = pc.GetComponent<SpriteRenderer>();
                Shield sd = x.gameObject.GetComponent<Shield>();
                if (prd != null && sd == null)
                {
                    SpriteRenderer spr = s.gameObject.GetComponent<SpriteRenderer>();
                    if (spr != null)
                    {
                        float hrate = 0.5f + 0.5f * s.hp / s.hpMax;
                        prd.color = new Color(
                            spr.color.r,
                            spr.color.g * hrate,
                            spr.color.b * hrate,
                            spr.color.a);
                    }
                }
            }
        }
    }
}
