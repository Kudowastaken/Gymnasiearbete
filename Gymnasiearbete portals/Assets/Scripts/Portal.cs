using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal linkedPortal;
    [SerializeField] private MeshRenderer portalScreen;
    
    public Camera playerCam;
    public Camera portalCam;
    public RenderTexture viewTexture;

    private void Awake()
    {
        playerCam = Camera.main;
        portalCam = GetComponentInChildren<Camera>();
        portalCam.enabled = false;
    }

    private void CreateViewTexture()
    {
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            if (viewTexture != null)
            {
                viewTexture.Release();
            }

            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            //Render camera view onto the view texture
            portalCam.targetTexture = viewTexture;
            //Set the linked portals texture to the view texture from the main portal
            linkedPortal.portalScreen.material.SetTexture("_MainTexture", viewTexture);
        }
    }

    private void OnPreRender()
    {
        Debug.Log("OnPreRender was called");
        portalScreen.enabled = false;
        CreateViewTexture();
        
        //Set the portal cameras position and rotation relative to the players camera relative to the linked portal
        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix *
                playerCam.transform.localToWorldMatrix;
        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
        
        portalCam.Render();

        portalScreen.enabled = true;
    }
}
