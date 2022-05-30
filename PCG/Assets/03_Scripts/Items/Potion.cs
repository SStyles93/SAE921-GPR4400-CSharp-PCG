using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : LootableItem
{
    [SerializeField] private float _healthValue = 10f;

    public override void OnPickUp()
    {
        _player.GetComponent<Player.PlayerStats>().RegainHealth(_healthValue);
    }
}
