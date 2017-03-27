using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mounted on an engine object with a line-renderer in it.
/// **NOT COMPLETED**
/// </summary>
public class EngineTrail : MonoBehaviour
{
    MeshRenderer rd;


	void Start()
    {
        rd = this.gameObject.GetComponent<MeshRenderer>();
        if (rd == null) 
        {
            Debug.Log("WARNING: Engine component don't have a line renderer : " + this.gameObject.name);
            Destroy(this); // Not the gameobject.
        }
	}

    public float dt = 0.2f;
    float t = 0f;

	void FixedUpdate()
    {
        t -= Time.fixedDeltaTime;
        if (t <= 0f) t += dt;

        // Add a node every d frames...
        


	}
}
