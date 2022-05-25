using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapScript : MonoBehaviour
{
     public Vector2 mapSize;
     public List<MapNodeLink> maplinks = new List<MapNodeLink>();
     public List<MapNode> mapNodes = new List<MapNode>();
     public PrefabTank PrefabTank;

     public void ClearListFromUnused()
     {
          for (int i = mapNodes.Count - 1; i >= 0; i--)
          {
               if (mapNodes[i].rootPos == 0)
               {
                    DestroyImmediate(mapNodes[i].gameObject);
                    mapNodes.RemoveAt(i);
               }

          }

          for (int i = maplinks.Count - 1; i >= 0; i--)
          {
               if ((maplinks[i].alreadyCheck && !maplinks[i].rootPathing) || !maplinks[i].alreadyCheck)
               {
                    DestroyImmediate(maplinks[i].gameObject);
                    maplinks.RemoveAt(i);
               }
          }

          foreach (var mapNode in mapNodes)
          {
               for (int i = mapNode.linkToOtherNode.Count - 1; i >= 0; i--)
               {
                    if (mapNode.linkToOtherNode[i] == null)
                    {
                         mapNode.linkToOtherNode.RemoveAt(i);
                    }
               }
          }
     }
     
}
