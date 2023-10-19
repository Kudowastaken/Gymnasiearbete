using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    [SerializeField] private Camera cameraA;
    [SerializeField] private Camera cameraB;
    [SerializeField] private Material cameraMatA;
    [SerializeField] private Material cameraMatB;
    
    void Update()
    {
        if (cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }

        if (cameraA.targetTexture != null)
        {
            cameraA.targetTexture.Release();
        }

        var targetTextureA = new RenderTexture(Screen.width, Screen.height, 24);
        targetTextureA.format = RenderTextureFormat.RGB111110Float;


        cameraA.targetTexture = targetTextureA;
        cameraMatA.mainTexture = cameraA.targetTexture;

        var targetTextureB = new RenderTexture(Screen.width, Screen.height, 24);
        targetTextureB.format = RenderTextureFormat.RGB111110Float;
        cameraB.targetTexture = targetTextureB;
        cameraMatB.mainTexture = cameraB.targetTexture;
    }
}
