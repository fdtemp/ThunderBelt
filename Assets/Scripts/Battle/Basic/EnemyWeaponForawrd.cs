using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Enemy weapons use some unique logic instead of player Device.
/// </summary>
abstract public class EnemyWeaponForward : EnemyWeapon
{
    public AudioSource au;

    /// <summary>
    /// Default direct cannonball aim to the player.
    /// </summary>
    /// <param name="targets"></param>
    public override void Launch(GameObject[] targets)
    {
        // Randomly choose a target in the targets list.
        int r = Mathf.FloorToInt(Random.Range(0, targets.Length));
        GameObject t = targets[r];

        // There's nothing can be attacked anymore!
        // And then it stop attack.

        if (targets == null || t == null) return;
        GameObject x = Instantiate(source);
        x.tag = "EnemyCannonball";
        x.transform.position = this.gameObject.transform.position;
        x.layer = 9; // UserLayer EnemyObject.

        // Source cannonball's direction is (and should be) always up.
        // Set the cannonball directing to direct down.
        Quaternion q = Quaternion.FromToRotation(Vector2.up, Vector2.down);
        x.transform.rotation = q;

        if (au != null) au.Play();

    }
}
