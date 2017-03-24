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

    float t = 0f;

    void FixedUpdate()
    { 
        t += Time.deltaTime;
        if (t > lifeTime) Destroy(this.gameObject);
        Move();
    }

    /// <summary>
    /// The Default moving function.
    /// Do the direct moving depends on speed.
    /// </summary>
    virtual protected void Move()
    {
        this.gameObject.transform.Translate(0f, speed * Time.deltaTime, 0f);
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

        // Notice that all *can-be-hit* cannonball & missiles *should* mount a SpecObject.
        SpecObject s = t.GetComponent<SpecObject>();
        if (s == null) return;

        s.RecieveDamage(damageType, damage);

        Destroy(this.gameObject);
    }
}
