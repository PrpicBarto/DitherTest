using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    [SerializeField] CinemachineCamera camera;
    [SerializeField] CinemachineBasicMultiChannelPerlin perlinNoise;

    private void Awake()
    {
        Instance = this;
        ResetIntensity();
    }
    public void ShakeCamera(float intesity, float shakeTime)
    {
        perlinNoise.AmplitudeGain = intesity;
        StartCoroutine(WaitTime(shakeTime));
    }

    IEnumerator WaitTime(float shakeTime)
    {
        yield return new WaitForSeconds(shakeTime);
        ResetIntensity();
    }

    void ResetIntensity()
    {
        perlinNoise.AmplitudeGain = 0f;
    }
}
