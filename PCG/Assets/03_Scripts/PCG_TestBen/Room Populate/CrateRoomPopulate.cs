using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CrateRoomPopulate : RoomPopulate
{
    


    public override void PcgPopulate()
    {
        Instantiate(_prefabTank.jar, transform.position, quaternion.identity,gameObject.transform);
    }
    
}
