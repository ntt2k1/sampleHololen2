using Photon.Pun;
using Photon.Realtime;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Network : MonoBehaviourPunCallbacks
{
    public bool isAudience;

    public Transform ARCamera;
    public GameObject zedCamera;

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.UseRpcMonoBehaviourCache = true;
        PhotonNetwork.EnableCloseConnection = true;
    }

    private void OnApplicationQuit()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.EmptyRoomTtl = 0;
            PhotonNetwork.CurrentRoom.PlayerTtl = 0;

            foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                if (!player.IsMasterClient)
                {
                    PhotonNetwork.CloseConnection(player);
                    PhotonNetwork.SendAllOutgoingCommands();
                }
            }
        }
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        if (!PhotonNetwork.InRoom)
        {
            PhotonNetwork.JoinOrCreateRoom("selabi87", new RoomOptions { IsOpen = true, IsVisible = true, MaxPlayers = 0 }, null, null);
            Debug.Log("onJoinRoom");
        }
    }

    public override void OnDisconnected(DisconnectCause disconnectCause)
    {
        base.OnDisconnected(disconnectCause);
        Application.Quit();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Application.Quit();
    }

    public override void OnJoinedRoom()
    {
        if (isAudience)
        {
           InitZED();
        }
        else
        {
           InitHololens();
        }
    }

    private void InitHololens()
    {
        GameObject playerModel = PhotonNetwork.Instantiate("Player", ARCamera.transform.position, ARCamera.transform.rotation);
        if (playerModel.GetComponent<PhotonView>().IsMine) {
            playerModel.transform.parent = ARCamera;
        }
    }

    private void InitZED()
    {
        Instantiate(zedCamera);
        ARCamera.gameObject.SetActive(false);
    }
}