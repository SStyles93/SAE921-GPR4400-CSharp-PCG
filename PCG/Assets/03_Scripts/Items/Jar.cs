using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : DestroyableItem
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject[] _loots;
    [SerializeField] private bool _isDestroyed = false;

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

        //Instantiate loots
        if(Random.value > 0.5f)
        {
            Instantiate(_loots[Random.Range(0, _loots.Length)], transform.position, Quaternion.identity);
        }
    }
}
