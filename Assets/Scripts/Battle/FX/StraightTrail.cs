using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Should be mounted on a single "Trail" gameObject with a SpriteRenderer alongside.
/// </summary>
public class StraightTrail : MonoBehaviour
{
    public float lifetime = 2.0f;

    bool hasBase = false;
    Vector2 baseLoc;
    public Vector2 baseLocation
    {
        get { return baseLoc; }
        set { hasBase = true; baseLoc = value; }
    }

    SpriteRenderer rd;
    Vector2 basesc;
    Cannonball ca;
    float y; // Y axis coord of trail tail.
    
    float t;

    void Start()
    {
        rd = this.gameObject.GetComponent<SpriteRenderer>();
        ca = this.gameObject.transform.parent.GetComponent<Cannonball>();
        basesc = this.gameObject.transform.localScale;

        if (!hasBase) { baseLoc = this.gameObject.transform.position; }
        y = baseLoc.y;

    }

    float vt;
    void Update()
    {
        t += Time.deltaTime;
        float rate = t / lifetime;
        if (rate > 1f) Destroy(this.gameObject);

        Vector2 head;
        if (!ca.hitted) head = this.gameObject.transform.parent.position;
        else { head.y = vt; head.x = this.gameObject.transform.position.x; }
        

        y -= Time.deltaTime * Global.flySpeed;

        this.gameObject.transform.position = new Vector2(head.x, (head.y + y) * 0.5f);

        // Assume that the init length(y) is 1.0f.
        this.gameObject.transform.localScale =
            new Vector2(basesc.x * (1 - rate), basesc.y * (head.y - y));
        

    }


}
