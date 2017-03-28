using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeLight : MonoBehaviour
{
    public float lifetime;
    public Vector2 range;

    SpriteRenderer rd;

    Color baseColor;
    void Start()
    {
        rd = this.gameObject.GetComponent<SpriteRenderer>();
        baseColor = rd.color;
    }

    float t;
    void Update()
    {
        t += Time.deltaTime;
        if (t > lifetime) Destroy(this.gameObject);
        float rate = t / lifetime;

        if (range.x > range.y)
        {
            float t = range.x;
            range.x = range.y;
            range.y = t;
        }
        float tr = Random.Range(range.x, range.y);
        rd.color = new Color(baseColor.r, baseColor.g, baseColor.b, baseColor.a * tr * (1 - rate));
    }

}
