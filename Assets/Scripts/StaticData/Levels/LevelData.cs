using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 0)]
public class LevelData: ScriptableObject
{
    public int countdown;
    public List<EnemyData> typesOfEnemies;
    public PlayerData player;
}