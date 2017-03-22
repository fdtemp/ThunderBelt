using UnityEngine;
using System.Collections;

/// <summary>
/// The basic class for devices(skills) and weapons.
/// Provides cd and so on.
/// Notice this is to mount on Weapon and Skill object but not player's plane.
/// </summary>
abstract public class Device : MonoBehaviour
{
    public float cd = 1f; // Cool down time.

    protected float t = 0f; // Time counter.

    public KeyCode keyBind;

    virtual protected void Update()
    {
        t -= Time.deltaTime;
        if (t <= 0f) t = 0f;

        if (Input.GetKey(keyBind)) TryAct();
    }

    /// <summary>
    /// Try to do action according to cd.
    /// </summary>
    virtual public bool TryAct()
    {
        if (t == 0) { Act(); t += cd; return true; }
        return false;
    }

    /// <summary>
    /// Do an action once.
    /// </summary>
    virtual protected void Act()
    {
        // Do nothing.
    }
 
}
