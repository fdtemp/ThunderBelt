using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlasherStay : MonoBehaviour
{
    public float lifetime;

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
        if (rate > 1f) { Destroy(this.gameObject); return; }

        // Scale...
        float srate = Mathf.Exp(1 - rate) - 1;

        Vector2 scale = this.gameObject.transform.localScale;
        scale.x = bscale.x * srate;
        scale.y = bscale.y * srate;
        this.gameObject.transform.localScale = scale;

        // Transparent...
        float trate = 1 - rate;
        rd.color = new Color(bcolor.r, bcolor.g, bcolor.b, bcolor.a * trate);
    }


}
