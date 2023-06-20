using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR;
using Microsoft.MixedReality.Toolkit.CameraSystem;
public class HoloCamera : MonoBehaviour
{
    WebCamTexture webcamTexture;
    public MixedRealityCameraSystem  mrtkCameraSystem;
    bool play = false;

    Renderer renderer;

    private void Start(){
        mrtkCameraSystem = CoreServices.CameraSystem as MixedRealityCameraSystem;
        renderer = GetComponent<Renderer>();
    }
    void Update()
    {
        if(GameManager.Instance.TrackedWithVuforia && !play){
            // webcamTexture = new WebCamTexture();
            // Renderer renderer = GetComponent<Renderer>();
            // renderer.material.mainTexture = webcamTexture;
            // webcamTexture.Play();
            // Texture mrcTexture = XRDevice.GetNativePtr();
            // if (displaySubsystem != null && displaySubsystem.running)
            // {
            //     renderer.material.mainTexture = displaySubsystem.GetTextureCopy();
            // }
            play = true;
        }
    }

    // Update is called once per frame
    
}
