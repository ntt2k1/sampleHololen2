using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Network : MonoBehaviourPunCallbacks
{
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
}