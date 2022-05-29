using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CrateRoomPopulate : RoomPopulate
{
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
            if (entity[i].GetComponent<Jar>()?.IsDestroyed == true)
            {
                entity.RemoveAt(i);
            }
        }
    } 
}
