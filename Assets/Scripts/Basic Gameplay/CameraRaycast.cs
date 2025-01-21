using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public Camera playerCamera; // Reference to the Camera
    public float rayDistance = 10f; // Maximum distance of the ray
    public GameObject lastHoverInteractible;
    void Update()
    {
        SendRay();
    }
    /// <summary>
    /// Detect if the player can grab an interactible and change the color of the interactible as a feedback
    /// </summary>
    public void SendRay()
    {
        // Create a ray starting from the center of the camera
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Interactible"))
            {
                if(lastHoverInteractible!=null && lastHoverInteractible != hit.collider.gameObject){
                    lastHoverInteractible.GetComponent<Interactible>().ReplaceMaterialWithOrigin();
                }
                hit.collider.gameObject.GetComponent<Interactible>().ReplaceMaterialWithHover();
                lastHoverInteractible = hit.collider.gameObject;
                return;
            }
        }
        if (lastHoverInteractible != null)
        {
            lastHoverInteractible.GetComponent<Interactible>().ReplaceMaterialWithOrigin();
            lastHoverInteractible = null;
        }
    }
    public bool IsHoverAnInteractible(){
        return lastHoverInteractible != null;
    }

}