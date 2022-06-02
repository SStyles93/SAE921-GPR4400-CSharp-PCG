using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : DestroyableItem
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<GameObject> _loots;
    [SerializeField] private bool _isDestroyed = false;

    [SerializeField] private GameObject _bossCoin;
    private bool _hasBossCoin = false;

    public bool IsDestroyed { get => _isDestroyed; set => _isDestroyed = value; }

    /// <summary>
    /// Launches the Destroying of the Jar
    /// </summary>
    public override void DestroyItem()
    {
        //Launch the destroying animation
        _animator.SetBool("Destroy", true);
        
        //Sets the state of the Jar to "Destroyed"
        _isDestroyed = true;

        if (_hasBossCoin)
        {
            //coin is always added after, so this spawns the last element (so, the BossCoin)
            Instantiate(_loots[_loots.Count], transform.position, Quaternion.identity);
        }
        else
        {
            //Instantiate loots
            if (Random.value > 0.5f)
            {
                Instantiate(_loots[Random.Range(0, _loots.Count)], transform.position, Quaternion.identity);
            }
        }
        
    }

    /// <summary>
    /// Adds a BossCoin to the Jar
    /// </summary>
    public void AddBossCoin()
    {
        _loots.Add(_bossCoin);
        _hasBossCoin = true;
    }
}
