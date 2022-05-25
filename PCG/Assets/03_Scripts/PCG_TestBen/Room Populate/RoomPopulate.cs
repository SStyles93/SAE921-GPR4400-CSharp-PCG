using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomPopulate : MonoBehaviour
{
     protected PrefabTank _prefabTank;

     public List<GameObject> entity = new List<GameObject>();

     private MapNode _myMapNode;
     protected BoxCollider2D _myCollider;

     public virtual void PcgPopulate()
     {
         _myCollider = gameObject.AddComponent<BoxCollider2D>();

         _myCollider.size = _myMapNode.sizeRoom - new Vector2Int(2,2);
         _myCollider.isTrigger = true;
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
             CloseDoors();
         }
     }

     private void CloseDoors()
     {
         foreach (var mapNodeLink in _myMapNode.linkToOtherNode)
         {
             mapNodeLink.CloseDoor();
         }
     }
}
