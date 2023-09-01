using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal linkedPortal;
    [SerializeField] private MeshRenderer portalScreen;
    
    public Camera playerCam;
    public Camera portalCam;
    public RenderTexture viewTexture;

    private void Awake()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        playerCam = Camera.main;
        portalCam = GetComponentInChildren<Camera>();
        portalCam.enabled = false;
    }

    private void CreateViewTexture()
    {
        Debug.LogError("CreateViewTexture was called");
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            Debug.LogError("Passed the first IF statement");
            if (viewTexture != null)
            {
                Debug.LogError("Releasing view texture");
                viewTexture.Release();
            }
            Debug.LogError("Should draw a new render texture");
            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            //Render camera view onto the view texture
            portalCam.targetTexture = viewTexture;
            //Set the linked portals texture to the view texture from the main portal
            linkedPortal.portalScreen.material.SetTexture("_BaseMap", viewTexture);
        }
    }

    private void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        portalScreen.enabled = false;
        CreateViewTexture();
        
        //Set the portal cameras position and rotation relative to the players camera relative to the linked portal
        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix *
                playerCam.transform.localToWorldMatrix;
        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        portalScreen.enabled = true;
    }
    void OnDestroy()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
    }
}
