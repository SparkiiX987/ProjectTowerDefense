using UnityEngine;

public class StatsBase : ScriptableObject
{
    [Header("Combat Stats")]
    public float attackDamages;
    public float attackSpeed;
    public float AttackRange;
    public DamageType damageType;
}
