using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR.Management;
using Vuforia;

public partial class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance = null;

    [Header("User")]
    public bool isAudience;
    public Player playerManager;

    [Header("Hololens")]
    public GameObject ARCamera;
    public Transform imageTarget;
    public GameObject playerPrefab;
  

    [Header("ZedCamera")]
    public Transform zedCameraTransform;
    public GameObject marker;
    public GameObject zedRigStereo;
    public GameObject zedCaptureToOpenCV;
    public GameObject ArUcoDetectManager;

    private bool init = false;
    private void Update()
    {
        if (init)
        {
            if (isAudience)
            {
                UpdateZed();
            }
            else
            {
                UpdateHololens();

            }
        }
    }


   
}