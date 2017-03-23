using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Enemy weapons use some unique logic instead of player Device.
/// </summary>
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
    virtual public bool shouldFireNow { get { return true; } }
    abstract public GameObject[] GetTargets();
    abstract public void Launch(GameObject[] targets);
}
