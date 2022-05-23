using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PaintGroundandWall : MonoBehaviour
{
   [SerializeField] private Tilemap ground;
   [SerializeField] private RuleTile wallTile;
   [SerializeField] private RuleTile floorTile;
   private MapScript _mapScript;
   
   public void PaintAllGround(MapScript map)
   {
      _mapScript = map;
      
      for (int iX = 0; iX < _mapScript.mapSize.x+30; iX++)
      {
         for (int iY = 0; iY < _mapScript.mapSize.y+30; iY++)
         {
            Vector2Int cellpostion = new Vector2Int((int)-(_mapScript.mapSize.x + 30) / 2 +iX,
               (int)-(_mapScript.mapSize.y + 30) / 2 +iY);

            PaintWallCell(cellpostion);
         }
      }
      

      foreach (var mapNode in _mapScript.mapNodes)
      {
         if (mapNode.rootPos != 0)
         {
            var sizeRoom = mapNode.sizeRoom;
            sizeRoom -= new Vector2Int(2, 2);

            for (int iX = 0; iX < sizeRoom.x; iX++)
            {
               for (int iY = 0; iY < sizeRoom.y; iY++)
               {
                  Vector3 centerNode = mapNode.transform.position;

                  Vector2Int cellpostion = new Vector2Int((int) centerNode.x - sizeRoom.x / 2 + iX,
                     (int) centerNode.y - sizeRoom.y / 2 + iY);

                  PaintFloorCell(cellpostion);
               }
            }
         }

      }

      foreach (var linkNode in _mapScript.maplinks)
      {
         if(linkNode.rootPathing)
         {


            var sizeDoor = new Vector2Int(2, 2);

            for (int iX = 0; iX < sizeDoor.x; iX++)
            {
               for (int iY = 0; iY < sizeDoor.y; iY++)
               {
                  Vector3 centerNode = linkNode.transform.position;

                  Vector2Int cellpostion = new Vector2Int((int) centerNode.x - sizeDoor.x / 2 + iX,
                     (int) centerNode.y - sizeDoor.y / 2 + iY);

                  PaintFloorCell(cellpostion);
               }
            }
         }
      }
   }

   private void PaintWallCell(Vector2Int position)
   {
      var posInGrid = ground.WorldToCell(new Vector3(position.x, position.y, 0));
      ground.SetTile(posInGrid, wallTile);
   }
   
   private void PaintFloorCell(Vector2Int position)
   {
      var posInGrid = ground.WorldToCell(new Vector3(position.x, position.y, 0));
      ground.SetTile(posInGrid, floorTile);
   }

   public void ClearMap()
   {
      ground.ClearAllTiles();
   }
}

