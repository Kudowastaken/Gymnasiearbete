using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectScreenFromCameraClipping : MonoBehaviour
{
    [SerializeField] private Camera playerCam;

    private void Update()
    {
        ProtectScreenFromPlayerNearClipPlane();
    }
    private void ProtectScreenFromPlayerNearClipPlane()
    {
        float halfHeight = playerCam.nearClipPlane * Mathf.Tan(playerCam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float halfWidth = halfHeight * playerCam.aspect;
        float distanceToNearClipPlaneCorner = new Vector3(halfWidth, halfHeight, playerCam.nearClipPlane).magnitude;

        Transform screenT = gameObject.transform;
        bool pCamFacingSameDirAsPortal = Vector3.Dot(transform.forward, transform.position - playerCam.transform.position) > 0;
        screenT.localScale = new Vector3(screenT.localScale.x, screenT.localScale.y, distanceToNearClipPlaneCorner);
        screenT.localPosition = Vector3.forward * distanceToNearClipPlaneCorner * ((pCamFacingSameDirAsPortal) ? 0.5f : -0.5f);
    }
}
