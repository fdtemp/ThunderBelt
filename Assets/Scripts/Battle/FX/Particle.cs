using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float lifetime = 0.5f;
    public Vector2 speedLimit = new Vector2(0.5f, 1.0f);

    float t = 0f;

    Vector2 basev;
    Vector2 v;

    SpriteRenderer rd;
    Color baseColor;
    

    void Start()
    {
        rd = this.gameObject.GetComponent<SpriteRenderer>();
        baseColor = rd.color;

        // Random generate a velocity...
        float speed = Random.Range(speedLimit.x, speedLimit.y);
        float a = Random.Range(0f, 2f * Mathf.PI);
        basev.x = Mathf.Cos(a) * speed;
        basev.y = -Mathf.Sin(a) * speed;
        v = basev;

    }

    
    void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        float rate = t / lifetime;
        if (rate > lifetime) { Destroy(this.gameObject); return; }

        // Color and transparent...
        rd.color = new Color(baseColor.r, baseColor.g, baseColor.b, baseColor.a * (1 - rate));

        // Movement...
        v.y = basev.y + rate * (-Global.flySpeed - basev.y);
        this.gameObject.transform.Translate(v * Time.fixedDeltaTime);
    }

}
