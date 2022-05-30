using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class LootableItem : MonoBehaviour
{
    protected GameObject _player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _player = collision.collider.gameObject;
            OnPickUp();
            Destroy(this.gameObject);
        }
    }

    public abstract void OnPickUp();
}
