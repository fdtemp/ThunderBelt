using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour
{
    public float lifetime;
    public Vector2 baseSpeed;

    SpriteRenderer rd;
    float t = 0;

    Vector2 bscale; // Base scale.
    Color bcolor; // Base color.

    void Start()
    {
        rd = this.gameObject.GetComponent<SpriteRenderer>();
        bscale = this.gameObject.transform.localScale;
        bcolor = rd.color;
    }

    void Update()
    {
        t += Time.deltaTime;
        float rate = t / lifetime;
        if (rate < 0f) { Destroy(this.gameObject); return; }

        // Scale...
        float srate = Mathf.Exp(1 - rate);
        
        Vector2 scale = this.gameObject.transform.localScale;
        scale.x = bscale.x * srate;
        scale.y = bscale.y * srate;
        this.gameObject.transform.localScale = scale;

        // Transparent...
        float trate = 1 - rate;
        rd.color = new Color(bcolor.r, bcolor.g, bcolor.b, bcolor.a * trate);

        // Global moving...
        this.gameObject.transform.position += 
            (Vector3)((Global.flySpeed * Vector2.down - baseSpeed) * rate + baseSpeed) * Time.deltaTime;
    }


}
