using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakerProxy : MonoBehaviour
{
    [SerializeField] private CinemachineCameraOffset cameraOffset;
    [SerializeField] private CinemachineRecomposer recomposer;
    [SerializeField] private GameObject proxy;

    private void Update()
    {
        cameraOffset.m_Offset = proxy.transform.localPosition;

        recomposer.m_Tilt = proxy.transform.localEulerAngles.x;
        recomposer.m_Pan = proxy.transform.localEulerAngles.y;
        recomposer.m_Dutch = proxy.transform.localEulerAngles.z;
    }
}
