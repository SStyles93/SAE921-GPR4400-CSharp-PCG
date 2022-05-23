using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : DestroyableItem
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject[] _loots;

    public override void DestroyItem()
    {
        _animator.SetBool("Destroy", true);
        if(Random.value > 0.5f)
        {
            Instantiate(_loots[Random.Range(0, _loots.Length)], transform.position, Quaternion.identity);
        }
    }
}
