using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameManager : MonoBehaviourPunCallbacks
{
    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    #endregion


    public GameObject playerPrefab;
    private List<PlayerManager> _players = new List<PlayerManager>();
    public List<PlayerManager> Players { get => _players; }

    private void Start()
    {
        //Get Host Player
        ExitGames.Client.Photon.Hashtable playerCustomProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        string name = (string)playerCustomProperties["HostPlayerName"];
        string id = (string)playerCustomProperties["HostPlayerID"];
        PlayerManager playerInfo = new PlayerManager(name, id);
        if (playerInfo != null)
        {
            _players.Add(playerInfo);
        }

        // spawn player instances
        SpawnPlayers();
    }

    

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        ExitGames.Client.Photon.Hashtable playerCustomProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        string name = (string)playerCustomProperties["PlayerName"];
        string id = (string)playerCustomProperties["PlayerID"];
        PlayerManager playerInfo = new PlayerManager(name, id);
        if (AddPlayer(playerInfo))
        {
            SpawnPlayers();
        }
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        RemovePlayer(otherPlayer.UserId);
    }
    private void SpawnPlayers()
    {
        foreach(PlayerManager player in _players)
        {
            if(player.Instance == null)
            {
                float radius = 10.0f;
                Vector3 randomPos = Vector3.zero + UnityEngine.Random.insideUnitSphere * radius;
                Debug.Log("SpawnPlayer");
                player.SpawnInstance(randomPos, Quaternion.identity);
            }
        }
    }
    public bool AddPlayer(PlayerManager newPlayer)
    {
        if (newPlayer == null) return false;
        foreach (PlayerManager player in _players)
        {
            if (player.ID == newPlayer.ID)
            {
                Debug.Log("Player " + newPlayer.ID + " already existed");
                return false;
            }
        }
        Debug.Log("Add player " + newPlayer.ID);
        _players.Add(newPlayer);
        return true;
    }
    private PlayerManager RemovePlayer(string ID)
    {
        foreach (PlayerManager player in _players)
        {
            if(player.ID == ID)
            {
                Debug.Log("Remove player " + ID);
                _players.Remove(player);
                return player;
            }
        }
        Debug.Log("Can not find player " + ID + " to remove");
        return null;
    }
}
