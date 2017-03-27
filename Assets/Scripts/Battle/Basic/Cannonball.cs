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

    float t = 0f;
    bool hitted = false;

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
        // 8 : PlayerObject layer.
        // 9 : EnemyObject layer.
        int layer = this.gameObject.tag.Contains("Ally") ? 9 : 8;
        RaycastHit2D rc = Physics2D.Raycast(
            this.gameObject.transform.position, Vector2.up, 1e4f, layer);

        if (rc.collider != null && rc.distance < speed * Time.fixedDeltaTime)
        {
            Debug.Log(rc.collider);
            Hit(rc.collider);
        }

        this.gameObject.transform.Translate(0f, speed * Time.fixedDeltaTime, 0f);
    }

    virtual public void SetTarget(GameObject[] target) // For tracing missiles and projectiles.
    {
        // Leave it do nothing...
    }

    void OnTriggerEnter2D(Collider2D x)
    { Hit(x); }

    void Hit(Collider2D x)
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
        else
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
        hitted = true;

        // Maintain FX objects...
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
                fx.transform.position = this.gameObject.transform.position;
                Flasher fl = fx.GetComponent<Flasher>();
                if (fl != null) fl.baseSpeed = vdir;
            }
        }
    }
}
