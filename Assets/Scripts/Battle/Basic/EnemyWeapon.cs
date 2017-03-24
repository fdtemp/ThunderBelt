using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyWeapon : MonoBehaviour 
{

    public GameObject source;
    public GameObject[] targets;
    public float cd = 1f;

    protected float t;
    void Update()
    {
        t -= Time.deltaTime;
        if (t < 0f) t = 0f;

        if (t == 0 && shouldFireNow)
        {
            Launch(GetTargets());
            t += cd;
        }
    }

    // Custom functions below to control enemy weapon behaviour.
    // DONOT use any other functions.
    virtual public bool shouldFireNow
    {
        get
        {
            Vector2 v = Camera.main.WorldToViewportPoint(this.gameObject.transform.position);
            return v.x <= 1f && v.x >= -1f && v.y <= 1f && v.y >= -1f;
        }
    }

    abstract public void Launch(GameObject[] targets);

    /// <summary>
    /// Default select player plane/ship.
    /// </summary>
    /// <returns>targets.</returns>
    virtual public GameObject[] GetTargets()
    {
        if (targets == null || targets.Length == 0)
        {
            targets = new GameObject[1];
            targets[0] = BattleManager.playerObject;
        }

        return targets;
    }

}
