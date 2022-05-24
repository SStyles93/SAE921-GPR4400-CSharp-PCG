using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomPopulate : MonoBehaviour
{
     protected PrefabTank _prefabTank;

     public abstract void PcgPopulate();

     public void SetPrefabTank(PrefabTank prefabTank)
     {
          _prefabTank = prefabTank;
     }
}
