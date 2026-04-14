using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerStats", menuName = "Stats/TowerStats")]
public class TowerStats : StatsBase
{
    [Header("BuyTable")]
    public int Cost;
    public List<TowerStats> NextLevels = new List<TowerStats>();

    [Header("Visual")]
    public Mesh mesh;
    //public Material material;
}
