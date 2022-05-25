#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PCGControlePanel))]
public class PCGControlePanelGUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PCGControlePanel myScript = (PCGControlePanel)target;
        
        if(GUILayout.Button("Generate room(Map Node)"))
        {
            myScript.GenerateRoom();
        }
        if (GUILayout.Button("Generate Link(Link Node)"))
        {
            myScript.GenerateLink();
        }
        if (GUILayout.Button("Generate rootRoad(LinkNode,Map Node"))
        {
            myScript.CreateRoot();
        }
        if (GUILayout.Button("Clear UnusedNode(LinkNode,Map Node)"))
        {
            myScript.ClearListFromUnused();
        }
        if (GUILayout.Button("Paint Ground(TileMap)"))
        {
            myScript.paintGround();
        }
        if (GUILayout.Button("Clear Ground(TileMap)"))
        {
            myScript.clearGround();
        }
        
        
    }
}
#endif