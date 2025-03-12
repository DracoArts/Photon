using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using Photon.Pun;
public class PlayerSpawner :MonoBehaviourPunCallbacks
{
     public GameObject playerPrefab;  
     public Transform[] spawnPoints; 

    void Start()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
         GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }
    }
