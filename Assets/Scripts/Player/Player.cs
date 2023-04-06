using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    private PhotonView _photonView;


    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine)
        {
            Transform mainCamera = Camera.main.transform;
            mainCamera.position = transform.position;
            transform.SetParent(Camera.main.transform);
        }
    }
}
