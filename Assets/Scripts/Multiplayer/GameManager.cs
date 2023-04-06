using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameManager : MonoBehaviourPunCallbacks
{
    #region Singleton
    public static GameManager Instance = null;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("GameManager already existed => Destroy gameobject");
            Destroy(gameObject);
            return;
        }

        Debug.Log("GameManager created");
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion


    public GameObject playerPrefab;
    [SerializeField] private List<PlayerManager> _players = new List<PlayerManager>();

    private void Start()
    {
        //Get Host Player
        ExitGames.Client.Photon.Hashtable playerCustomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        Debug.Log("len: " + PhotonNetwork.CurrentRoom.CustomProperties.Count);
        string name = (string)playerCustomProperties["HostPlayerName"];
        Debug.Log("HostPlayerName: " + name);
        string id = (string)playerCustomProperties["HostPlayerID"];
        Debug.Log("HostPlayerID: " + id);

        PlayerManager playerInfo = new PlayerManager(name, id);
        if (playerInfo != null)
        {
            _players.Add(playerInfo);
            float radius = 10.0f;
            Vector3 randomPos = Vector3.zero + UnityEngine.Random.insideUnitSphere * radius;
            randomPos.y = 0;
            Debug.Log("SpawnPlayer");
            playerInfo.SpawnInstance(randomPos, Quaternion.identity);
        }

        // spawn player instances
        //SpawnPlayers();
    }


    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
        ExitGames.Client.Photon.Hashtable playerCustomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        string name = (string)playerCustomProperties["PlayerName"];
        Debug.Log("PlayerName: " + name);
        string id = (string)playerCustomProperties["PlayerID"];
        Debug.Log("PlayerID: " + id);

        PlayerManager playerInfo = new PlayerManager(name, id);
        if (AddPlayer(playerInfo) && playerInfo.PlayerInstance == null)
        {
            float radius = 10.0f;
            Vector3 randomPos = Vector3.zero + UnityEngine.Random.insideUnitSphere * radius;
            randomPos.y = 0;
            Debug.Log("SpawnPlayer");
            playerInfo.SpawnInstance(randomPos, Quaternion.identity);
        }
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom");
        RemovePlayer(otherPlayer.UserId);
    }
   
    public bool AddPlayer(PlayerManager newPlayer)
    {
        if (newPlayer == null) return false;
        foreach (PlayerManager player in _players)
        {
            if (player.GetPlayerID() == newPlayer.GetPlayerID())
            {
                Debug.Log("Player " + newPlayer.GetPlayerID() + " already existed");
                return false;
            }
        }
        Debug.Log("Add player " + newPlayer.GetPlayerID());
        _players.Add(newPlayer);
        return true;
    }
    private PlayerManager RemovePlayer(string ID)
    {
        foreach (PlayerManager player in _players)
        {
            if(player.GetPlayerID() == ID)
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
