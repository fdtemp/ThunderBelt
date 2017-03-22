using UnityEngine;
using System.Collections;

abstract public class Weapon : Device
{
    public GameObject cannonballSource = null;
    public PlayerObject playerPlane = null;
    public GameObject fireLocator = null;


    void Start()
    {
        if (playerPlane == null)
        {
            Debug.Log("WARNING: Weapon don't have playerPlane mounted:" + this.gameObject.name);
        }
    }

    protected override void Update()
    {
        // These time-updating thing is written here dure to override.
        t -= Time.deltaTime;
        if (t <= 0f) t = 0f;

        if (Input.GetKey(keyBind)) TryFire(fireLocator.transform.position, playerPlane.targets);
    }

    /// <summary>
    /// Fire without a target.
    /// </summary>
    /// <param name="loc">Location creating the cannonball.</param>
    virtual public void TryFire(Vector2 loc)
    { TryFire(loc, null); }

    /// <summary>
    /// Fire a cannonball or so at loc, and set its targets.
    /// </summary>
    /// <param name="loc">Location creating the cannonball.</param>
    /// <param name="target">Target for those cannonball.</param>
    virtual public void TryFire(Vector2 loc, GameObject[] target)
    {
        // Donnot write anything out of this section.
        if (TryAct())
        {
            GameObject x = Instantiate(cannonballSource);
            Cannonball c = x.GetComponent<Cannonball>();
            x.tag = "Ally";
            x.transform.position = loc;
            c.SetTarget(target);
            // And then do nothing...
        }
    }
}
