using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class LinkPopulate : MonoBehaviour
{
    protected PrefabLibrary _prefabTank /*= new PrefabTank()*/;
    public abstract void PcgPopulate();

    protected List<GameObject> _entity = new List<GameObject>();


    public virtual void CloseDoor()
    {
        _entity.Add(Instantiate(_prefabTank.Door, transform.position, Quaternion.identity));
    }
    
    public virtual void SetPrefabTank(PrefabLibrary prefabTank)
    {
        _prefabTank = prefabTank;
    }

    public virtual PrefabLibrary GetPrefabTank()
    {
        return _prefabTank;
    }

    public virtual void OpenDoor()
    {
        for (int i = _entity.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(_entity[i]);
            _entity.RemoveAt(i);
        }
    }

}
