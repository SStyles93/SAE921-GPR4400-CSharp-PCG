using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrateRoomPopulate : RoomPopulate
{
    public override void PcgPopulate()
    {
        base.PcgPopulate();

        //Checks for the lowest size value
        if (_myMapNode.sizeRoom.x > _myMapNode.sizeRoom.y)
        {
            //and used it(divided by 4) as the spawn range
            _spawnRange = _myMapNode.sizeRoom.y / 4.0f;
        }
        else
        {
            _spawnRange = _myMapNode.sizeRoom.x / 4.0f;
        }

        for (int spawnAmount = 0; spawnAmount < Random.Range(_minSpawnAmount, _maxSpawnAmount); spawnAmount++)
        {
            GameObject Jar = SpawnEntity(_prefabLibrary.Jar,
            _myMapNode.transform.position);
            _myMapNode.RoomPopulate.entity.Add(Jar);
            _gameManager.TrackedObjects.Add(Jar);
        }
    }

    public void Update()
    {
        if (entity.Count == 0 && _roomActive)
        {
            foreach (var mapNodeLink in _myMapNode.linkToOtherNode)
            {
                mapNodeLink.OpenDoor();
            }

            _roomActive = false;
        }

        for (int i = entity.Count - 1; i >= 0; i--)
        {
            //checks for the Jar's isDestroyed Bool
            if (entity[i].GetComponent<Jar>()?.IsDestroyed == true)
            {
                entity.RemoveAt(i);
            }
        }
    } 
}
