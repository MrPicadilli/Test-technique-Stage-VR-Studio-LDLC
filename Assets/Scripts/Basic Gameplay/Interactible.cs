using System;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    public Material originMaterial;
    public Material cameraHoverMateral;
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
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name + " interactible");
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
