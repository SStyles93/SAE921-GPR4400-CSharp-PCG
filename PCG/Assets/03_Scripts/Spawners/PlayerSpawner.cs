using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector3 _spawnPosition;

    public Vector3 SpawnPosition { get => _spawnPosition; set => _spawnPosition = value; }

    void Start()
    {
        Instantiate(_playerPrefab, _spawnPosition, Quaternion.identity);        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_spawnPosition, 0.25f);
    }
}
