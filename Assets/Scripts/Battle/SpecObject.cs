using UnityEngine;
using System.Collections;

/// <summary>
/// Defines hp, sp and so on.
/// </summary>
public class SpecObject : MonoBehaviour 
{
    public float hpMax = 1f;
    public float hp = 1f; // Strength / Health.

    public GameObject[] deadFX;

    // Regenurate rate.
    public float hpRegen = 0f;

    // Armor type. Used to modify damage revieved.
    public ArmorType armorType = ArmorType.Struct;
    
    /// <summary>
    /// Do this function every frame.
    /// All about Regenereation and so on.
    /// </summary>
	virtual protected void FixedUpdate()
	{
        if (hp < 0)
        {
            OnKilled();
            return;
        }
        hp += hpRegen * Time.fixedDeltaTime;
        if (hp >= hpMax) hp = hpMax;
    }

    virtual public void RecieveDamage(DamageType type, float amount)
    {
        hp -= ConvertDamage(amount, type, armorType);
    }

    /// <summary>
    /// Instead of OnDestroy(), this function can include some
    /// objcet-creation code to produce FX objcet and so on.
    /// </summary>
    virtual protected void OnKilled()
    {
        // Create a failed flash...
        for (int i = 0; i < deadFX.Length; i++)
        {
            GameObject x = Instantiate(deadFX[i]);
            x.transform.position = this.gameObject.transform.position;
        }

        // Make this object really dead...
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a">Damage amount.</param>
    /// <param name="dmt">Damage type.</param>
    /// <param name="amt">Armor/Defence type.</param>
    /// <returns></returns>
    float ConvertDamage(float a, DamageType dmt, ArmorType amt)
    {
        switch (amt)
        {
            case ArmorType.Field: // Shield.
                switch (dmt)
                {
                    case DamageType.Energy: return 1.0f * a;
                    case DamageType.Explode: return 1.0f * a;
                    case DamageType.Fragment: return 0.6f * a;
                    case DamageType.Impact: return 1.25f * a;
                    default: return 0f;
                }
            case ArmorType.Atom: // Shield.
                switch (dmt)
                {
                    case DamageType.Energy: return 0.8f * a;
                    case DamageType.Explode: return 1.25f * a;
                    case DamageType.Fragment: return 0.6f * a;
                    case DamageType.Impact: return 0.8f * a;
                    default: return 0f;
                }
            case ArmorType.Nuclear:
                switch (dmt)
                {
                    case DamageType.Energy: return 0.8f * a;
                    case DamageType.Explode: return 0.9f * a;
                    case DamageType.Fragment: return 1.5f * a;
                    case DamageType.Impact: return 1.25f * a;
                    default: return 0f;
                }
            case ArmorType.Steel:
                switch (dmt)
                {
                    case DamageType.Energy: return 1.15f * a;
                    case DamageType.Explode: return 1.15f * a;
                    case DamageType.Fragment: return 0.75f * a;
                    case DamageType.Impact: return 0.75f * a;
                    default: return 0f;
                }
            case ArmorType.Struct:
                switch (dmt)
                {
                    case DamageType.Energy: return 1.0f * a;
                    case DamageType.Explode: return 1.2f * a;
                    case DamageType.Fragment: return 1.5f * a;
                    case DamageType.Impact: return 0.8f * a;
                    default: return 0f;
                }
            default: return a;
        }
    }
}
