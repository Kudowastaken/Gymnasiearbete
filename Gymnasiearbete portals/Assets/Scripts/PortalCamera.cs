using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalCamera : MonoBehaviour
{
    [SerializeField] private GameObject linkedPortal;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Camera portalCam;
    [SerializeField] private GameObject playerShadowThroughPortal;
    [SerializeField] private GameObject player;
    
    private void LateUpdate()
    {
        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix *
                playerCam.transform.localToWorldMatrix;
        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        var n = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * 
                player.transform.localToWorldMatrix;
        playerShadowThroughPortal.transform.SetPositionAndRotation(n.GetColumn(3), n.rotation);
    }
}
