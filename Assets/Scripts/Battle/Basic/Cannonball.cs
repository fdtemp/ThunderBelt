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
    public GameObject explodeFX;

    float t = 0f;
    bool exploded = false;

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

        if (exploded) return; // This gameobjcet will be destroyed when timeout in FixedUpdate.


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
        exploded = true;


        if (explodeFX != null)
        {
            GameObject fx = Instantiate(explodeFX);
            fx.transform.position = this.gameObject.transform.position;
            Flasher fl = fx.GetComponent<Flasher>();
            if (fl != null)
            {
                Vector3 dir;
                float a;
                this.gameObject.transform.rotation.ToAngleAxis(out a, out dir);
                if (dir.z < 0) { dir = -dir; a = - a; }
                fl.baseSpeed.y = Mathf.Min(Mathf.Cos(a * Mathf.Deg2Rad) * speed, 0f);
                fl.baseSpeed.x = - Mathf.Sin(a * Mathf.Deg2Rad) * speed;
            }
        }
    }
}
