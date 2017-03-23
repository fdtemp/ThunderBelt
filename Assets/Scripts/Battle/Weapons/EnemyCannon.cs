using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : EnemyWeapon
{
    public override bool shouldFireNow { get { return true; } }
    
    /// <summary>
    /// Default select player plane/ship.
    /// </summary>
    /// <returns>targets.</returns>
    public override GameObject[] GetTargets()
    {
        if (targets == null || targets.Length == 0)
        {
            targets = new GameObject[1];
            targets[0] = BattleManager.playerObject;
        }

        return targets;
    }

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
        // Source cannonball's direction is (and should be) always up.
        // Set the cannonball directing to the target.
        Quaternion q = Quaternion.FromToRotation(Vector2.up, 
            t.transform.position - this.gameObject.transform.position);
        x.transform.rotation = q;
    }
}
