using UnityEngine;
using System.Collections;

/// <summary>
/// Defines hp, sp and so on.
/// </summary>
public class SpecObject : MonoBehaviour 
{
    public float hpMax = 1f;

    public float hp = 1f; // Strength / Health.

    // Regenurate rate.
    public float hpRegen = 0f;

    /// <summary>
    /// Do this function every frame.
    /// All about Regenereation and so on.
    /// </summary>
	virtual protected void Update()
	{
        hp += hpRegen * Time.deltaTime;
        if (hp >= hpMax) hp = hpMax;
    }

    virtual public void RecieveDamage(DamageType type, float amount)
    {
        hp -= amount;
    }
}
