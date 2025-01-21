using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform headPlayer;

    void Update() {
        transform.position = headPlayer.transform.position;
    }
}