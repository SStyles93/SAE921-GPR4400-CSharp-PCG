using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Managers.SceneManagement _sceneManagement;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private PlayerSpawner _playerSpawner;

    private bool _hasWon = false;

    // Update is called once per frame
    void Update()
    {
        //Check for enemy death
        for (int i = 0; i < _enemySpawner.TrackedEnemies.Count; i++)
        {

            if (_enemySpawner.TrackedEnemies[i] == null)
            {
                _enemySpawner.TrackedEnemies.RemoveAt(i);
            }
            else
            {
                //Enables the enemy if he is in a defined range from the player (15.0f)
                if ((_enemySpawner.TrackedEnemies[i].transform.position - _playerSpawner.Player.transform.position).magnitude <= 15.0f)
                {
                    _enemySpawner.TrackedEnemies[i].gameObject.SetActive(true);
                }
                else
                {
                    _enemySpawner.TrackedEnemies[i].gameObject.SetActive(false);
                }
            }
        }

        //Check for boss death
        for (int i = 0; i < _enemySpawner.TrackedBosses.Count; i++)
        {

            if (_enemySpawner.TrackedBosses[i] == null)
            {
                _enemySpawner.TrackedBosses.RemoveAt(i);
            }
            else
            {
                //Enables the enemy if he is in a defined range from the player (15.0f)
                if ((_enemySpawner.TrackedBosses[i].transform.position - _playerSpawner.Player.transform.position).magnitude <= 15.0f)
                {
                    _enemySpawner.TrackedBosses[i].gameObject.SetActive(true);
                }
                else
                {
                    _enemySpawner.TrackedBosses[i].gameObject.SetActive(false);
                }
            }
        }
        if(_enemySpawner.TrackedBosses.Count == 0)
        {
            if (!_hasWon)
            Victory();
        }
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
}
