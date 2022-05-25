using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class LinkPopulate : MonoBehaviour
{
    protected PrefabTank _prefabTank = new PrefabTank();
    public abstract void PcgPopulate();


    public virtual void CloseDoor()
    {
        Instantiate(_prefabTank.Door, transform.position, quaternion.identity);
    }
    
    public virtual void SetPrefabTank(PrefabTank prefabTank)
    {
        _prefabTank = prefabTank;
    }

}
