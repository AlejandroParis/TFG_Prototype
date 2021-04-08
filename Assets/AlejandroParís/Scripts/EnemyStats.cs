using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Sporeborn/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public GameObject Target;
    public int life;
    public float speed;
    public float rotationSpeed;
    public int damage;
}
