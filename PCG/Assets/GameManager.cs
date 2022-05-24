using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _trackedEnemies;

    [SerializeField] private bool _hasWon = false;


    public GameObject Player { get => _player; set => _player = value; }
    public List<GameObject> TrackedEnemies { get => _trackedEnemies; set => _trackedEnemies = value; }

    // Update is called once per frame
    void Update()
    {
        //Check for enemy death
        for (int i = 0; i < _trackedEnemies.Count; i++)
        {

            if (_trackedEnemies[i] == null)
            {
                _trackedEnemies.RemoveAt(i);
            }
        }
        if(_trackedEnemies.Count == 0)
        {
            if (!_hasWon)
            Victory();
        }
    }

    private void Victory()
    {
        //Set victory
        Debug.Log("Victory !");

        _hasWon = true;
    }
}
