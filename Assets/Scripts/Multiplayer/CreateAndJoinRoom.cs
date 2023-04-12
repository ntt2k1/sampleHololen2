using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using ExitGames.Client.Photon;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField createRoomInput;
    public TMP_InputField joinRoomInput;
    public TMP_InputField userNameInput;

    public string gameSceneName;
    private bool isHost = false;

    public void CreateRoom()
    {
        if (createRoomInput.text.Trim() == string.Empty)
        {
            Debug.LogWarning("Need room's name to create room");
        }
        else if (userNameInput.text.Trim() == string.Empty)
        {
            Debug.LogWarning("Username cannot be empty");
        }
        else
        {
            Debug.Log("Create Room: " + createRoomInput.text);
            isHost = true;
            PhotonNetwork.CreateRoom(createRoomInput.text);
        }
    }

    public void JoinRoom()
    {
        if (joinRoomInput.text.Trim() == string.Empty)
        {
            Debug.LogWarning("Need room's name to join room");
        }
        else if (userNameInput.text.Trim() == string.Empty)
        {
            Debug.LogWarning("Username cannot be empty");
        }
        else
        {
            Debug.Log("Join Room: " + joinRoomInput.text);
            isHost = false;
            PhotonNetwork.JoinRoom(joinRoomInput.text);
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom != null)
        {
            string name = userNameInput.text;
            string id = PhotonNetwork.LocalPlayer.UserId;
            if (isHost)
            {
                PhotonNetwork.CurrentRoom.CustomProperties.Add("HostPlayerName", name);
                PhotonNetwork.CurrentRoom.CustomProperties.Add("HostPlayerID", id);

            }
            else
            {
                PhotonNetwork.CurrentRoom.CustomProperties.Add("PlayerName", name);
                PhotonNetwork.CurrentRoom.CustomProperties.Add("PlayerID", id);
            }

            
            Debug.Log("PhotonNetwork.CurrentRoom.CustomProperties: " + PhotonNetwork.CurrentRoom.CustomProperties);

        }
            PhotonNetwork.LoadLevel(gameSceneName);
        
    }
}
