using System.IO;
using System.Linq;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.CameraSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.WebCam;

public class MRCStream : MonoBehaviour
{
    UnityEngine.Windows.WebCam.WebCam webCam;
    VideoCapture m_VideoCapture = null;
    private bool isPlay = false;
    private bool isTiming = false;

    public UnityEvent<string> m_MyEvent = new UnityEvent<string>();

    public TMPro.TextMeshProUGUI text;
    private string videoPath;
    private string videoPathDelete = "";

    private float FPS = 1f;


    // public void OnRecordVideo()
    // {
    //     if (GameManager.Instance.TrackedWithVuforia)
    //         VideoCapture.CreateAsync(true, OnVideoCaptureCreated);
    //     else
    //     {
    //         print("NO CAMERA");
    //     }

    // }

    void Update()
    {
        if (GameManager.Instance.TrackedWithVuforia && !isPlay)
        {
            VideoCapture.CreateAsync(false, OnVideoCaptureCreated);
            isPlay = true;

        }

        if (isTiming)
        {
            if (FPS <= 0f)
            {
                FPS = 1f;
                isTiming = false;
                StopRecordingVideo();
            }
            else
            {
                FPS -= Time.deltaTime;
            }
        }
    }
    void OnVideoCaptureCreated(VideoCapture videoCapture)
    {
        if (videoCapture != null)
        {
            m_VideoCapture = videoCapture;

            Resolution cameraResolution = VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
            float cameraFramerate = VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();

            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 1.0f;
            cameraParameters.frameRate = cameraFramerate;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

            m_VideoCapture.StartVideoModeAsync(cameraParameters,
                                                VideoCapture.AudioState.None,
                                                OnStartedVideoCaptureMode);
        }
        else
        {
            Debug.LogError("Failed to create VideoCapture Instance!");
        }
    }
    void OnStartedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        if (result.success)
        {
            string filename = string.Format("MyVideo_{0}.mp4", Time.time);
            string filepath = System.IO.Path.Combine(Application.persistentDataPath, filename);

            print("FILE PATH +++++++++++> " + filepath);
            text.text = filepath;
            videoPath = filepath;

            m_VideoCapture.StartRecordingAsync(filepath, OnStartedRecordingVideo);
        }
    }
    void OnStartedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Started Recording Video!");
        // We will stop the video from recording via other input such as a timer or a tap, etc.
        StartTiming();
    }
    // The user has indicated to stop recording
    public void StopRecordingVideo()
    {
        m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);

    }

    public void DeleteVideo()
    {
        // File.Delete(videoPath);
        m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
    }

    void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Stopped Recording Video!");
        m_MyEvent.Invoke(videoPath);
        if (!videoPathDelete.Equals(""))
            File.Delete(videoPathDelete);
        videoPathDelete = videoPath;

        string filename = string.Format("MyVideo_{0}.mp4", Time.time);
        string filepath = System.IO.Path.Combine(Application.persistentDataPath, filename);
        videoPath = filepath;

        m_VideoCapture.StartRecordingAsync(filepath, OnStartedRecordingVideo);
        // m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
    }

    void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        m_VideoCapture.Dispose();
        m_VideoCapture = null;
    }

    void StartTiming()
    {
        isTiming = true;
    }
}
