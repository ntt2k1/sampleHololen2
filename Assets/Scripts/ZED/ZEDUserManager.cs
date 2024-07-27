using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using Vuforia;

public partial class GameManager
{
    public Transform cameraEyes;
    public Transform leftEye;

    public float retrackMarkerInterval = 10f;
    private float retrackTimer = 0f;
    private void InitZED()
    {
        print("init zed");
        ARCamera.GetComponent<VuforiaBehaviour>().enabled = false;
        MixedRealityToolkit.Instance.enabled = false;
        ARCamera.SetActive(false);

        zedRigStereo?.SetActive(true);

        leftEye.gameObject.tag = "MainCamera";

        //Instantiate(zedCaptureToOpenCV);
        //Instantiate(ArUcoDetectManager);
        zedCaptureToOpenCV?.SetActive(true);
        ArUcoDetectManager?.SetActive(true);
        //SkeletonTrackerModule?.SetActive(true);

        init = true;
    }

    private void UpdateZed()
    {
        if(retrackTimer >= retrackMarkerInterval){
            if (marker.activeSelf)
            {
                zedCameraTransform.position = leftEye.transform.position;
                zedCameraTransform.rotation = leftEye.transform.rotation;
                zedCameraTransform.SetParent(marker.transform);


                marker.transform.position = Vector3.zero;
                marker.transform.eulerAngles = new Vector3(-90, 180, 0);
                leftEye.position = zedCameraTransform.position;
                leftEye.rotation = zedCameraTransform.rotation;
                retrackTimer = 0f;
            }
            else{
                retrackTimer = retrackMarkerInterval;
            }

        }
        else{
            retrackTimer += Time.deltaTime;
        }
        
    }




}
