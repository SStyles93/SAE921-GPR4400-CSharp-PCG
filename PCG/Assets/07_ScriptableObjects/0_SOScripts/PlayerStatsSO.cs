using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats",
    menuName = "ScriptableObject/Stats/PlayerStats", order = 2)]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Final values")]
    public float currentHealth = 100.0f;
    public float maxHealth = 100.0f;

    public void ResetPlayerStats()
    {
        currentHealth = 100.0f;
        maxHealth = 100.0f;
    }
}

