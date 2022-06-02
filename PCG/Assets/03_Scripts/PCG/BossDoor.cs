using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [SerializeField] private Canvas _messageCanvas;
    [SerializeField] private bool _bossDoor;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_bossDoor)
            {
                if (collision.GetComponent<PlayerInventory>()?.BossCoinCount > 0)
                {
                    //Open door
                    Destroy(gameObject);
                }
                else
                {
                    //Activate warning message
                    _messageCanvas.gameObject.SetActive(true);
                }
            }
            else
            {
                //Open door
                Destroy(gameObject);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        //Disactivate warning message
        _messageCanvas.gameObject.SetActive(false);
    }
}
