using UnityEngine;
using System.Collections;

/// <summary>
/// The basic class for devices(skills) and weapons.
/// Provides cd and so on.
/// Notice this is to mount on Weapon and Skill object but not player's plane.
/// </summary>
public class Device : MonoBehaviour
{
    public float cd = 1f; // Cool down time.

    float t = 0f; // Time counter.

    virtual protected void Update()
    {
        t -= Time.deltaTime;
        if (t <= 0f) t = 0f;
    }

    /// <summary>
    /// Try to do action according to cd.
    /// </summary>
    virtual public void TryAct()
    {
        if (t == 0) { Act(); t += cd; }
    }

    /// <summary>
    /// Do an action once.
    /// </summary>
    virtual protected void Act()
    {
        // Do nothing.
    }


}
