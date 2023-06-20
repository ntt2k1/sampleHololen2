using Microsoft.MixedReality.Toolkit;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using Vuforia;

public partial class GameManager
{
    private float trackMarkerDuration = 3f;
    private float trackMarkerCount = 0f;
    public bool HololensMarkerTracked { get => hololensMarkerTracked; set => hololensMarkerTracked = value; }
    private bool hololensMarkerTracked = false;

    private Transform ARPlaySpace;
    

    public void InitHololens()
    {
        ARPlaySpace = ARCamera.transform.parent;
        ARCamera.transform.localPosition = Vector3.zero;
        GameObject playerModel = PhotonNetwork.Instantiate("Prefabs/" + playerPrefab.name, ARCamera.transform.position, ARCamera.transform.rotation);
        ARCamera.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;


        if (playerModel.GetComponent<PhotonView>().IsMine)
        {
            playerModel.GetComponent<MoveARCamera>().ARCamera = ARCamera.transform;
        }


        init = true;
    }

    public bool TrackedWithVuforia { get => trackedWithVuforia; }
    private bool trackedWithVuforia = false;

    public void UpdateHololens()
    {
        if(trackMarkerCount >= trackMarkerDuration && !TrackedWithVuforia)
        {
            TurnOffVuforia();
            ARCamera.GetComponent<TrackedPoseDriver>().enabled = true;
            trackedWithVuforia = true;
        }
        else
        {
            if (HololensMarkerTracked)
            {
                trackMarkerCount += Time.deltaTime;
            }
            else
            {
                trackMarkerCount = 0;
            }
        }


    }

   
    private void TurnOffVuforia()
    {
        Debug.Log("Turn off vuforia");
        ARCamera.GetComponent<VuforiaBehaviour>().enabled = false;

        ARCamera.transform.SetParent(imageTarget);
        imageTarget.transform.position = Vector3.zero;
        imageTarget.transform.rotation = Quaternion.identity;



        ARPlaySpace.position = ARCamera.transform.position;
        ARPlaySpace.rotation = ARCamera.transform.rotation;

        ARCamera.transform.SetParent(ARPlaySpace);
        ARCamera.transform.localPosition = Vector3.zero;
        ARCamera.transform.localRotation = Quaternion.identity;

      
    }

}
