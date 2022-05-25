using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGInGame : MonoBehaviour
{
    
    [SerializeField] private PCGControlePanel pcgControlePanel;
    [SerializeField] AstarPath _aStar;
    [SerializeField] PlayerSpawner _playerSpawner;

    private MapNode _spawningNode;


    private void Awake()
    {
        GenerateAll();
        Pathfinding.GridGraph gridGraph = _aStar.data.gridGraph;
        gridGraph.SetDimensions((int)pcgControlePanel.Map.mapSize.x, (int)pcgControlePanel.Map.mapSize.y, 1.0f);
        StartCoroutine(ScanMap(0.2f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Launches successively all the methods in order to create a map
    /// </summary>
    public void GenerateAll()
    {
        pcgControlePanel.clearGround();
        pcgControlePanel.GenerateRoom();
        pcgControlePanel.GenerateLink();
        pcgControlePanel.CreateRoot();
        pcgControlePanel.ClearListFromUnused();
        pcgControlePanel.CallPopulate();
        pcgControlePanel.paintGround();

        //Spawn player
        foreach (var node in pcgControlePanel.Map.mapNodes)
        {
            if(node.roomType == PcgPopulate.RoomType.PlayerBase)
            {
                _spawningNode = node;
            }
        }
        _playerSpawner.SpawnPosition = _spawningNode.transform.position;
    }

    /// <summary>
    /// Coroutine used to scan the map with A* pathfinding (delayed)
    /// </summary>
    /// <returns></returns>
    IEnumerator ScanMap(float delay)
    {
        yield return new WaitForSeconds(delay);
        AstarPath.active.Scan();
    }
}
