using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int _bossCoinCount = 0;

    public int BossCoinCount { get => _bossCoinCount; private set => _bossCoinCount = value; }

    public void IncreaseBossCoinCount()
    {
        _bossCoinCount++;
        Debug.Log($"Player has {_bossCoinCount} boss coin");
    }
}
