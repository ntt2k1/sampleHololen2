using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class HololensVideoCapture : MonoBehaviour
{

    private bool startCapture = false;
    private bool photoModeStart = false;
    public Texture2D m_texture;

    private void FixedUpdate()
    {
        if(GameManager.Instance.TrackedWithVuforia && !startCapture){
            PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
            startCapture = true;
        }
        else if(GameManager.Instance.TrackedWithVuforia && startCapture && photoModeStart ){
            photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
        }
    }

    private PhotoCapture photoCaptureObject = null;

    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 1.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.BGRA32;

        captureObject.StartPhotoModeAsync(c, OnPhotoModeStarted);
    }


    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            // photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
            photoModeStart = true;

        }
        else
        {
            Debug.LogError("Unable to start photo mode!");
        }
    }


    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }


    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        if (result.success)
        {
            List<byte> imageBufferList = new List<byte>();
            // Copy the raw IMFMediaBuffer data into our empty byte list.
            photoCaptureFrame.CopyRawImageDataIntoBuffer(imageBufferList);

            // In this example, we captured the image using the BGRA32 format.
            // So our stride will be 4 since we have a byte for each rgba channel.
            // The raw image data will also be flipped so we access our pixel data
            // in the reverse order.
            // int stride = 4;
            // float denominator = 1.0f / 255.0f;
            // List<Color> colorArray = new List<Color>();
            // for (int i = imageBufferList.Count - 1; i >= 0; i -= stride)
            // {
            //     float a = (int)(imageBufferList[i - 0]) * denominator;
            //     float r = (int)(imageBufferList[i - 1]) * denominator;
            //     float g = (int)(imageBufferList[i - 2]) * denominator;
            //     float b = (int)(imageBufferList[i - 3]) * denominator;

            //     colorArray.Add(new Color(r, g, b, a));
            // }
            // Now we could do something with the array such as texture.SetPixels() or run image processing on the list

            photoCaptureFrame.UploadImageDataToTexture(m_texture);

        }
    }


    public void StopCapture(){
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }
}
