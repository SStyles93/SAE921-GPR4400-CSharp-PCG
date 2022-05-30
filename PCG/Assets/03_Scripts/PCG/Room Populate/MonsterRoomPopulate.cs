using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoomPopulate : RoomPopulate
{
    public override void PcgPopulate()
    {
        base.PcgPopulate();

        //Spawning for Enemies
        for (int spawnAmount = 0; spawnAmount < Random.Range(_minSpawnAmount, _maxSpawnAmount); spawnAmount++)
        {
            GameObject ennemy = SpawnEntity(_prefabLibrary.Chaser,
            _myMapNode.transform.position);
            _gameManager.TrackedEnemies.Add(ennemy);
            _myMapNode.RoomPopulate.entity.Add(ennemy);
        }
    }
}

