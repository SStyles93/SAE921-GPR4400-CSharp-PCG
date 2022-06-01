using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Reference Scripts
    [Header("Reference Scripts")]
    [Tooltip("The SceneManager has to be pugged here")]
    [SerializeField] private SceneManagement _sceneManagement;

    //Ref GameObjects
    [Header("The tracked objects")]
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _trackedEnemies;
    [SerializeField] private List<GameObject> _trackedBosses;
    [SerializeField] private List<GameObject> _trackedObjects;

    private bool _hasWon = false;
    private bool _hasLost = false;

    public GameObject Player { get => _player; set => _player = value; }
    public List<GameObject> TrackedEnemies { get => _trackedEnemies; set => _trackedEnemies = value; }
    public List<GameObject> TrackedBosses { get => _trackedBosses; set => _trackedBosses = value; }
    public List<GameObject> TrackedObjects { get => _trackedObjects; set => _trackedObjects = value; }
    public SceneManagement SceneManagement { get => _sceneManagement; private set => _sceneManagement = value; }

    // Update is called once per frame
    private void Update()
    {
        //Check for enemy death
        for (int i = 0; i < _trackedEnemies.Count; i++)
        {

            if (_trackedEnemies[i] == null)
            {
                _trackedEnemies.RemoveAt(i);
            }
            else
            {
                //Enables the enemy if he is in a defined range from the player (15.0f)
                if ((_trackedEnemies[i].transform.position - _player.transform.position).magnitude <= 15.0f)
                {
                    _trackedEnemies[i].gameObject.SetActive(true);
                }
                else
                {
                    _trackedEnemies[i].gameObject.SetActive(false);
                }
            }
        }

        //Check for boss death
        for (int i = 0; i < _trackedBosses.Count; i++)
        {

            if (_trackedBosses[i] == null)
            {
                _trackedBosses.RemoveAt(i);
            }
            else
            {
                //Enables the enemy if he is in a defined range from the player (15.0f)
                if ((_trackedBosses[i].transform.position - _player.transform.position).magnitude <= 15.0f)
                {
                    _trackedBosses[i].gameObject.SetActive(true);
                }
                else
                {
                    _trackedBosses[i].gameObject.SetActive(false);
                }
            }
        }
        if(_trackedBosses.Count == 0)
        {
            if (!_hasWon)
            Victory();
        }

        if (_player.GetComponent<Player.PlayerStats>().IsDead)
        {
            if(!_hasLost)
                Loss();
        }
    }

    /// <summary>
    /// Sets all the enemies in Play(true)/Pause(false) state
    /// </summary>
    /// <param name="state">state in which the enemies have to be</param>
    public void PauseEnemies(bool state)
    {
        foreach (GameObject enemy in _trackedEnemies)
        {
            enemy.GetComponent<Enemy.EnemyAI>().PauseEnemy(state);
        }
    }

    /// <summary>
    /// Sets the game state to "Play"
    /// </summary>
    public void ResumeGame()
    {
        _player.GetComponent<Player.PlayerController>().GameState(true);
        _player.GetComponent<Player.PlayerController>().play = true;
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Delete all enemies and clear lists
    /// </summary>
    public void ClearLists()
    {
        //Destroy and clear Enemies
        foreach(var entity in _trackedEnemies)
        {
            DestroyImmediate(entity);
        }
        _trackedEnemies.Clear();

        //Destroy and clear Bosses
        foreach (var entity in _trackedBosses)
        {
            DestroyImmediate(entity);
        }
        _trackedBosses.Clear();

        //Destroy and clear Objects
        foreach (var entity in _trackedObjects)
        {
            DestroyImmediate(entity);
        }
        _trackedObjects.Clear();

        //Destroy and clear Player
        if(_player != null)
        DestroyImmediate(_player.gameObject);
        _player = null;
    }

    private void Victory()
    {
        //Set victory
        Debug.Log("Victory !");
        //Sets the scene index to 2 (end menu)
        _sceneManagement.SceneIndex = 2;
        //Launches the fade out to the scene
        _sceneManagement.FadeOut = true;
        _hasWon = true;
    }
    private void Loss()
    {
        Debug.Log("Lost !");
        //Reset scene
        _sceneManagement.SceneIndex = 1;
        //Launch scene
        _sceneManagement.FadeOut = true;
        _hasLost = true;
    }
}
