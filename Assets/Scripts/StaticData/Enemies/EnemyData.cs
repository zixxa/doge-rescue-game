using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData", order = 0)]
public class EnemyData: ScriptableObject
{
    public string name;
    public int countInSpawn;
    public Enemy typeOfEnemies;
}