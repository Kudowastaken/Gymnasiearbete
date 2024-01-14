using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateObliqueProjectionMatrix : MonoBehaviour
{
    [SerializeField] private Transform inTransform;
    [SerializeField] private Transform outTransform;
    [SerializeField] private Camera portalCamera;
    [SerializeField] private bool Invert;

    Plane plane;
    private void Update()
    {
        if (Invert)
        {
            plane = new Plane(-outTransform.forward, inTransform.position);
        } else
        {
            plane = new Plane(outTransform.forward, inTransform.position);
        }
        Vector4 clipPlaneWorldSpace = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);
        Vector4 clipPlaneCameraSpace = 
            Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix)) * clipPlaneWorldSpace;
        var newMatrix = portalCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        portalCamera.projectionMatrix = newMatrix;
    }
}
