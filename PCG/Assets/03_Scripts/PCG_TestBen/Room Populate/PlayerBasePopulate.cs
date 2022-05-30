using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasePopulate : RoomPopulate
{
    public override void PcgPopulate()
    {
        base.PcgPopulate();

        _gameManager.Player = Instantiate(_prefabLibrary.player, transform.position, Quaternion.identity);
        //Subscribes the "Pause" Methods to the player Controller
        _gameManager.Player.GetComponent<Player.PlayerController>().GameState += _gameManager.PauseEnemies;
        _gameManager.Player.GetComponent<Player.PlayerController>().GameState += _gameManager.SceneManagement.PauseCanvas;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
