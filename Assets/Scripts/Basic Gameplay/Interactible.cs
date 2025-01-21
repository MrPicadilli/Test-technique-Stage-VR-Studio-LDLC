using System;
using UnityEngine;

/// <summary>
/// This class handles position of the camera
/// </summary>
/// <remarks>
/// Attach this script to the camera parent which should be an empty object attached to nothing
/// </remarks>

[RequireComponent(typeof(MeshRenderer))]
public class Interactible : MonoBehaviour
{
    [Header("Interactible aspect")]   
    private MeshRenderer _meshRenderer;
    [Tooltip("Basic materal used on the interactible")]
    public Material originMaterial;
    [Tooltip("Temporary materal used when the interactible is catchable")]
    public Material cameraHoverMateral;
    [Tooltip("Temporary materal used when the interactible carried is throwable")]
    public Material throwableMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = originMaterial;
    }
    public void ReplaceMaterialWithOrigin()
    {
        _meshRenderer.material = originMaterial;
    }
    public void ReplaceMaterialWithThrowable()
    {
        _meshRenderer.material = throwableMaterial;
    }
    public void ReplaceMaterialWithHover()
    {
        _meshRenderer.material = cameraHoverMateral;
    }
    /// <summary>
    /// Will change the color of the object if the interactible carried is in the point zone
    /// </summary>
    private void OnTriggerEnter(Collider other) {
        if(PlayerControllerNoGravity.instance.IsItemCarried(gameObject) && other.gameObject.layer == LayerMask.NameToLayer("Container")){
            ReplaceMaterialWithThrowable();
            PlayerControllerNoGravity.instance.ItemCanBeThrown();
        }
    }
    private void OnTriggerExit(Collider other) {
        ReplaceMaterialWithOrigin();
    }

    internal void DesactivateItem()
    {
        gameObject.layer = LayerMask.NameToLayer("");
    }
}
