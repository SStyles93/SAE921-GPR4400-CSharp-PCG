using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomPopulate : RoomPopulate
{
    public override void PcgPopulate()
    {
        base.PcgPopulate();

        //Spawning for Bosses
        for (int spawnAmount = 0; spawnAmount < Random.Range(_minSpawnAmount, _maxSpawnAmount); spawnAmount++)
        {
            GameObject ennemy = SpawnEntity(_prefabLibrary.Shooter,
            _myMapNode.transform.position);
            _gameManager.TrackedBosses.Add(ennemy);
            _myMapNode.RoomPopulate.entity.Add(ennemy);
        }
    }
}
