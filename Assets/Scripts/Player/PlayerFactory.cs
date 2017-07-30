using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerFactory {

	public static Player CreatePlayer()
    {
        GameObject playerPrefab = Resources.Load("Player/Player") as GameObject;
        Vector3 spawnPosition = GameController.Instance.PlayerSpawnPoint == null ? Vector3.zero : GameController.Instance.PlayerSpawnPoint.position;

        var playerObject = GameObject.Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        return playerObject.GetComponent<Player>();
    }
}
