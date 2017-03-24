using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mounted on an engine object with a line-renderer in it.
/// </summary>
public class EngineTrail : MonoBehaviour
{
    LineRenderer rd;

	void Start()
    {
        rd = this.gameObject.GetComponent<LineRenderer>();
        if (rd == null) 
        {
            Debug.Log("WARNING: Engine component don't have a line renderer : " + this.gameObject.name);
            Destroy(this); // Not the gameobject.
        }
	}

    public float dt = 0.2f;
    float t = 0f;

	void Update()
    {
        t -= Time.deltaTime;
        if (t <= 0f)
        {
            
            t += dt;
        }
	}
}
