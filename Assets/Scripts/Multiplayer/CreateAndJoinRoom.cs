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


            ExitGames.Client.Photon.Hashtable playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
            playerCustomProperties.Add("HostPlayerName", userNameInput.text);
            playerCustomProperties.Add("HostPlayerID" , PhotonNetwork.LocalPlayer.UserId);

            PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);

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


            ExitGames.Client.Photon.Hashtable playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
            playerCustomProperties.Add("PlayerName", userNameInput.text);
            playerCustomProperties.Add("PlayerID", PhotonNetwork.LocalPlayer.UserId);
            
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);

            PhotonNetwork.JoinRoom(joinRoomInput.text);
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        PhotonNetwork.LoadLevel(gameSceneName);
    }
}
