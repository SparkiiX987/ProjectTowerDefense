using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    private TowerStats stats;

    public abstract void OnTowerClick();

    public abstract void Attack();
}
