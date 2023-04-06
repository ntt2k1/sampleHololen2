using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class PlayerManager
{
    private GameObject _instance = null;
    public GameObject Instance { get => _instance; }

    [SerializeField] private string _id;

    [SerializeField] private string _name;

    public PlayerManager(string name, string ID) {
        _name = name;
        _id = ID;
    }

    public void SpawnInstance(Vector3 position, Quaternion rotation)
    {
        if(_instance == null)
        {
            _instance = PhotonNetwork.Instantiate(GameManager.Instance.playerPrefab.name, position, rotation);
            _instance.GetComponent<Player>().SyncPlayerInfo(this);
        }
    }

    public string GetPlayerID()
    {
        return _id;
    }

    public string GetPlayerName()
    {
        return _name;
    }
}
