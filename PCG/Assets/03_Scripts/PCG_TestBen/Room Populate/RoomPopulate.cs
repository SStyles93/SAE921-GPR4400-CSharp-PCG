using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public abstract class RoomPopulate : MonoBehaviour
{
    protected PrefabTank _prefabTank;

    public List<GameObject> entity = new List<GameObject>();

    protected MapNode _myMapNode;
    protected BoxCollider2D _roomCollider;
    protected bool _roomActive = false;

     public virtual void PcgPopulate()
     {
        _roomCollider = gameObject.AddComponent<BoxCollider2D>();
        _roomCollider.size = _myMapNode.sizeRoom - new Vector2Int(3,3);
        _roomCollider.isTrigger = true;
     }

    public void SetPrefabTank(PrefabTank prefabTank)
    {
        _prefabTank = prefabTank;
    }

     public void SetMapNode(MapNode mapNode)
     {
         _myMapNode = mapNode;
     }

     private void OnTriggerEnter2D(Collider2D col)
     {
         if (col.CompareTag("Player"))
         {
             _roomActive = true;
             if (entity.Count != 0)
             {
                 CloseDoors();
             }
             
         }
     }

     

     private void CloseDoors()
     {
         foreach (var mapNodeLink in _myMapNode.linkToOtherNode)
         {
             mapNodeLink.CloseDoor();
         }
     }

     private void Update()
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
             if (entity[i] == null)
             {
                 entity.RemoveAt(i);
                 
             }
         }
     }
     
     
}
