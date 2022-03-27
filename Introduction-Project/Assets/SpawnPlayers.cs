using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;


    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0)
        {
            var posicao = new Vector2(-4.84f, 1.77f);
            PhotonNetwork.Instantiate(playerPrefab.name, posicao, Quaternion.identity);
        }else if(players.Length == 1){
            var posicao = new Vector2(5.43f, 1.77f);
            PhotonNetwork.Instantiate(playerPrefab.name, posicao, Quaternion.identity);
        }
    }
}
