using UnityEngine;
using System.Collections;

/// <summary>
/// The basic class for *player* devices(skills) and weapons.
/// Provides cd and so on.
/// Notice this is to mount on Weapon and Skill object but not player's plane.
/// </summary>
abstract public class Device : MonoBehaviour
{
    public float cd = 1f; // Cool down time.
    public float mpCost = 0f; // Energy cost (per shoot in default).
    public Player player = null;

    public KeyCode keyBind;

    protected void Start()
    {
        if (this.gameObject.tag == "Enemy" || this.gameObject.tag == "EnemyCannonball")
        {
            Debug.Log("WARNING: player device is mounted on an enemy object : " + this.gameObject.name);
        }
    }

    protected float t = 0f; // Time counter.
    bool isPrepared { get { return t == 0f && player.mp >= mpCost; } }

    virtual protected void FixedUpdate()
    {
        t -= Time.deltaTime;
        if (t <= 0f) t = 0f;

        if (Input.GetKey(keyBind) && isPrepared)
        {
            Launch();
            player.mp -= mpCost;
            t += cd;
        }
    }

    abstract public void Launch();
}
