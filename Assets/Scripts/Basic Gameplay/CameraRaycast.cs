using UnityEngine;


/// <summary>
/// This class handles the feedback when the field of view is hover an interactible and the possibility to grab the item
/// </summary>
public class CameraRaycast : MonoBehaviour
{
    [Tooltip("Manage the total tieme allowed and the number of point to get to win the game")]
    public Camera playerCamera; // Reference to the Camera
    [Tooltip("Manage the total tieme allowed and the number of point to get to win the game")]
    public float rayDistance = 2f; // Maximum distance of the ray
    [HideInInspector]
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

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Interactible"))
            {
                // in case if the ray was last seen on a different interactible and go through another different object whitout putting the origin material of the lastHoverInteractible
                // or in simple term when you pass your ray from interactible to another interactible
                if(lastHoverInteractible!=null && lastHoverInteractible != hit.collider.gameObject){ 
                    lastHoverInteractible.GetComponent<Interactible>().ReplaceMaterialWithOrigin();
                }
                hit.collider.gameObject.GetComponent<Interactible>().ReplaceMaterialWithHover();
                lastHoverInteractible = hit.collider.gameObject;
                return;
            }
        }
        // when you pass your ray from interactible to nothing
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