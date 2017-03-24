using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRolling : MonoBehaviour
{
    public float speed = 5f;
    public float topEdge = 20f;
    public float bottomEdge = -20f;
    void FixedUpdate()
    {
        Vector2 loc = (Vector2)this.gameObject.transform.position + Vector2.down * speed * Time.deltaTime;
        if (loc.y > topEdge) loc.y = loc.y - topEdge + bottomEdge;
        if(loc.y < bottomEdge) loc.y = loc.y + topEdge - bottomEdge;
        this.gameObject.transform.position = loc;
    }
    
}
