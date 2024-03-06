﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameController : MonoBehaviour
{
    public Slider resolveBar;

    public static GameController Instance;

    private float default_amplitude;
    private float default_frequency;

    public CinemachineVirtualCamera CinemachineVirtualCamera;

    public NoiseSettings ShakeNoiseProfile;
    public NoiseSettings DefaultNoiseProfile;

    // public bool TentPic = false;

    [Header("Player Info")]
    [SerializeField] private int _maxResolve = 100;
    [SerializeField] private int _currentResolve;

    void Awake()
    {
        Debug.Assert(CinemachineVirtualCamera != null, "CinemachineVirtualCamera must exist in the scene");
        Debug.Assert(ShakeNoiseProfile != null, "ShakeNoiseProfile must exist in the scene");
        Debug.Assert(DefaultNoiseProfile != null, "DefaultNoiseProfile must exist in the scene");

        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        default_amplitude = cinemachineBasicMultiChannelPerlin.m_AmplitudeGain;
        default_frequency = cinemachineBasicMultiChannelPerlin.m_FrequencyGain;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _currentResolve = _maxResolve;

            this.SetMaxResolve(_maxResolve);


        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMaxResolve(int resolve)
    {
        resolveBar.maxValue = resolve;
        resolveBar.value = resolve;
    }

    public void SetResolve(int resolve)
    {
        resolveBar.value = resolve;
    }

    public void ChangeResolve(int change)
    {
        resolveBar.value += change;
    }

    public void StartShake(float amplitude, float frequency, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_NoiseProfile = ShakeNoiseProfile;

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
        StartCoroutine(StopShake(time));
    }

    private IEnumerator StopShake(float time)
    {
        yield return new WaitForSeconds(time);

        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_NoiseProfile = DefaultNoiseProfile;

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = default_amplitude;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = default_frequency;
    }
}
