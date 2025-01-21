using System;
using UnityEngine;


/// <summary>
/// This class handles all ther control of the player (movement, fps, grab, throw item)
/// </summary>
/// <remarks>
/// need a rigidobdy and a cameraRaycast
/// </remarks>
[RequireComponent(typeof(Rigidbody),typeof(CameraRaycast))]
public class PlayerControllerNoGravity : MonoBehaviour
{
    public static PlayerControllerNoGravity instance;

    [Header("Movement")]   
    [Tooltip("Must be an empty object carrying a camera")]
    public Transform cameraPlayer;
    [Tooltip("Give a local orientation to move the player depending on it")]
    public Transform orientation;
    public float moveSpeed = 10f;
    private Rigidbody _rb;

    [Header("Rotation and look")]
    public float mouseSensitivity = 100f;
    private float _upAndDownRotation = 0f;
    private float _leftAndRightRotation = 0f;

    [Header("Grab System")]
    private CameraRaycast _cameraRaycast;
    [Tooltip("The reference of where an item will be if grabed")]
    public GameObject RightHandAnchor;
    private bool _canBeThrown = false;
    private GameObject _itemCarried;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraRaycast = GetComponent<CameraRaycast>();
    }

    private void Update()
    {
        if (!GameManager.instance.isPlaying)
            return;
        Look();
        Movement();
        if (Input.GetMouseButtonDown(0))
            GrabInteractible();

        if (Input.GetMouseButtonDown(1))
            ThrowInteractible();
    }
    /// <summary>
    /// When the interactible is in hand and in the point zone throw the interactible
    /// </summary>
    private void ThrowInteractible()
    {
        if (!_canBeThrown || _itemCarried == null)
            return;

        _itemCarried.transform.parent = null;
        PrepareItemToThrow();
        _itemCarried = null;
        _canBeThrown = false;
    }
    /// <summary>
    /// give back the parameter that were removed when grabed
    /// </summary>
    private void PrepareItemToThrow()
    {
        _itemCarried.GetComponent<Rigidbody>().useGravity = true;
        _itemCarried.GetComponent<Rigidbody>().isKinematic = false;
    }
    /// <summary>
    /// When the player look at the interactible at a certain distance grab the item with his right hand
    /// </summary>
    private void GrabInteractible()
    {
        if (!_cameraRaycast.IsHoverAnInteractible())
        {
            return;
        }
        _itemCarried = _cameraRaycast.lastHoverInteractible;
        _itemCarried.transform.parent = RightHandAnchor.transform;
        PrepareItemToGrab();


    }
    /// <summary>
    /// Change some parameter to make the intactible look like it is in his right hand
    /// </summary>
    private void PrepareItemToGrab()
    {
        _itemCarried.GetComponent<Rigidbody>().useGravity = false;
        _itemCarried.GetComponent<Rigidbody>().isKinematic = true;
        _itemCarried.transform.rotation = Quaternion.Euler(0, 0, 0);
        _itemCarried.transform.position = RightHandAnchor.transform.position;
    }

    public bool IsItemCarried(GameObject item)
    {
        return _itemCarried == item;
    }
    /// <summary>
    /// Use the information of the mouse to change the field of view
    /// </summary>
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        //Find current look rotation
        Vector3 rot = cameraPlayer.localRotation.eulerAngles;
        _leftAndRightRotation = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        _upAndDownRotation -= mouseY;
        _upAndDownRotation = Mathf.Clamp(_upAndDownRotation, -90f, 90f);

        //Perform the rotations
        cameraPlayer.localRotation = Quaternion.Euler(_upAndDownRotation, _leftAndRightRotation, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, _leftAndRightRotation, 0);
    }
    /// <summary>
    /// Use the information of the board zqsd to move the player
    /// </summary>
    private void Movement()
    {
        float _x = Input.GetAxisRaw("Horizontal");
        float _y = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(_x, 0f, _y);
        if (movement.magnitude > 1)
            movement.Normalize();
        Vector3 frontToBack = orientation.forward * _y * moveSpeed * Time.deltaTime;
        Vector3 leftToRight = orientation.right * _x * moveSpeed * Time.deltaTime;
        transform.position = transform.position + leftToRight + frontToBack;

        Vector3 newPosition = _rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }

    public void ItemCanBeThrown()
    {
        _canBeThrown = true;
    }
}
