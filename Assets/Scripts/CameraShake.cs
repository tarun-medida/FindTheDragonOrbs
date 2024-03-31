using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Camera Shake Parameters
    private CinemachineVirtualCamera virtualCamera;
    private float shakeIntentity = 8f;
    private float shakeTime = 3f;
    private float timer;
    private CinemachineBasicMultiChannelPerlin m_MultiChannelPerlin;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StopCameraShake();
    }

    private void Update()
    {
        if (GameManager.instance.shakeCamera)
        {
            ShakeCamera();
            GameManager.instance.shakeCamera = false;
        }
        // Camera shake timer
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StopCameraShake();
                    
            }
            
        }
    }

   // *** CAMERA SHAKE EFFECT FUNCTIONS
    void ShakeCamera()
    {
        if (virtualCamera != null)
        {
            m_MultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            m_MultiChannelPerlin.m_AmplitudeGain = shakeIntentity;
            m_MultiChannelPerlin.m_FrequencyGain = shakeTime;
            timer = 5f;
        }
    }

    void StopCameraShake()
    {
        if (virtualCamera != null)
        {
            m_MultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            m_MultiChannelPerlin.m_AmplitudeGain = 0f;
            m_MultiChannelPerlin.m_FrequencyGain = 0f;
            timer = 0f;
        }
    }
    //**********************************
}
