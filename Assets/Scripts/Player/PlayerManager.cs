using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class PlayerManager
{
    private GameObject _instance = null;
    public GameObject Instance { get => _instance; }

    private string _id;
    public string ID { get => _id; set => _id = value; }

    private string _name;
    public string Name { get => _name; }

    public PlayerManager(string name, string ID) {
        _name = name;
        _id = ID;
    }

    public void SpawnInstance(Vector3 position, Quaternion rotation)
    {
        if(_instance == null)
        {
            _instance = PhotonNetwork.Instantiate(GameManager.Instance.playerPrefab.name, position, rotation);
        }
    }
}
