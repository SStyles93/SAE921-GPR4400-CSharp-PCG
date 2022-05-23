using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserDeath : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _sprites;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
    }
}
