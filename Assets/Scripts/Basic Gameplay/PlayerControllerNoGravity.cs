using System;
using UnityEngine;

public class PlayerControllerNoGravity : MonoBehaviour
{
    public Transform cameraPlayer;
    public Transform orientation;
    public float moveSpeed = 10f;
    public float sensitivity = 100f;
    public float xRotation = 0f;
    public float desiredX = 0f;
    private Rigidbody rb;
    [Header("Grab System")]
    private CameraRaycast _cameraRaycast;
    public GameObject RightHandAnchor;
    private bool _canBeThrown = false;
    private GameObject _itemCarried;
    public static PlayerControllerNoGravity instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        _cameraRaycast = GetComponent<CameraRaycast>();
    }
    private void Update()
    {
        if(!GameManager.instance.isPlaying)
            return;
        Look();
        Movement();
        if (Input.GetMouseButtonDown(0))
            GrabInteractible();

        if (Input.GetMouseButtonDown(1))
            ThrowInteractible();
    }

    private void ThrowInteractible()
    {
        if (!_canBeThrown || _itemCarried == null)
            return;

        _itemCarried.transform.parent = null;
        PrepareItemToThrow();
        _itemCarried = null;
        _canBeThrown = false;
    }

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
    private void PrepareItemToThrow()
    {
        _itemCarried.GetComponent<Rigidbody>().useGravity = true;
        _itemCarried.GetComponent<Rigidbody>().isKinematic = false;
    }
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
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;

        //Find current look rotation
        Vector3 rot = cameraPlayer.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        cameraPlayer.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

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

        Vector3 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    public void ItemCanBeThrown()
    {
        _canBeThrown = true;
    }
}
