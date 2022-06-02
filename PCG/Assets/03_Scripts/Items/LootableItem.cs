using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class LootableItem : MonoBehaviour
{
    protected GameObject _player;

    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected AudioClip _audioClip;

    private bool _destroyItem = false;
    private float _destroyingTime = 1f;

    private void Update()
    {
        if (_destroyItem)
        {
            _destroyingTime -= Time.deltaTime;
        }
        if(_destroyingTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //sets the player
            _player = collision.collider.gameObject;
            //Hides the object
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //pick up
            OnPickUp();
            //LaunchDestroy
            _destroyItem = true;
        }
    }

    public abstract void OnPickUp();
}
