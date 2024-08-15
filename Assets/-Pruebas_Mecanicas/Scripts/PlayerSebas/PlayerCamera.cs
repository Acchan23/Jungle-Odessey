using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using EZCameraShake;

public class PlayerCamera : MonoBehaviour
{
    public CameraShaker camShake;
    public CinemachineBrain camBrain;

    [Header("Shaker:")]
    [SerializeField] Vector3 shakeImpact;
    [SerializeField] float shakeTime;
    [SerializeField] bool shakeAction;
    // Start is called before the first frame update
    void Start()
    {
        camShake = CameraShaker.Instance;
        camBrain = CinemachineCore.Instance.GetActiveBrain(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeAction)
        {
            shakeAction = false;
            Shacker();
        }
    }

    public void Shacker()
    {
        camShake.ShakeOnce(shakeImpact.x, shakeImpact.y, shakeImpact.z, shakeTime);
    }
    public void ChangeCamera(float time)
    {
        var blend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, time);
        camBrain.m_DefaultBlend = blend; 
    }
}
