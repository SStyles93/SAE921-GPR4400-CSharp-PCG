using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestroyableItem : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Weapon"))
        {
            DestroyItem();
        }
    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Weapon"))
    //    {
    //        DestroyItem();
    //    }
    //}

    public abstract void DestroyItem();
}
