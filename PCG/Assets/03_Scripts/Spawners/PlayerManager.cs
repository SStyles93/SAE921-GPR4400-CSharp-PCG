using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Managers.SceneManagement _sceneManagement;
    [SerializeField] private EntityManager _enemySpawner;

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector3 _spawnPosition;

    [SerializeField] private GameObject _player;

    public Vector3 SpawnPosition { get => _spawnPosition; set => _spawnPosition = value; }
    public GameObject Player { get => _player; private set => _player = value; }

    void Start()
    {
        _player = Instantiate(_playerPrefab, _spawnPosition, Quaternion.identity);
        //Subscribes the "Pause" Methods to the player Controller
        _player.GetComponent<PlayerController>().GameState += _enemySpawner.PauseEnemies;
        _player.GetComponent<PlayerController>().GameState += _sceneManagement.PauseCanvas;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_spawnPosition, 0.25f);
    }

    /// <summary>
    /// Sets the game state to "Play"
    /// </summary>
    public void ResumeGame()
    {
        _player.GetComponent<PlayerController>().GameState(true);
        _player.GetComponent<PlayerController>().play = true;
        Time.timeScale = 1.0f;
    }
}
