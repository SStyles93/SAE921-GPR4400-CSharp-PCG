using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoin : LootableItem
{
    public override void OnPickUp()
    {
        _player.GetComponent<PlayerInventory>()?.IncreaseBossCoinCount();
        _audioSource.clip = _audioClip;
        _audioSource.Play();
    }
}
