using UnityEngine;

/// <summary>
/// This class handles position of the camera
/// </summary>
/// <remarks>
/// Attach this script to the camera parent which should be an empty object attached to nothing
/// </remarks>
public class MoveCamera : MonoBehaviour {

    public Transform headPlayer;

    void Update() {
        transform.position = headPlayer.transform.position;
    }
}