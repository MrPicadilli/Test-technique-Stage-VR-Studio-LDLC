using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Rotation and look")]
    public Transform playerCam;
    public Transform orientation;

    [Header("Input")]
    private Rigidbody _rb;
    private float _x, _y;

    [Header("Rotation and look")]
    private float xRotation;
    private float sensitivity = 50f;

    [Header("Movement")]
    public float moveSpeed = 4500;
    public float maxSpeed = 20;
    [Tooltip("Force of the correction of movements so that the player don't slide too much")]
    public float counterMovementForce = 0.175f;
    private float _threshold = 0.01f;
    public ForceMode forceMode = ForceMode.Force;    

    [Header("Grab System")]
    private CameraRaycast _cameraRaycast;
    public GameObject RightHandAnchor;
    private bool _carryAnInteractible;
    private GameObject _itemCarried;


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraRaycast = GetComponent<CameraRaycast>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        MyInput();
        Look();
    }

    /// <summary>
    /// Find user input.
    /// </summary>
    private void MyInput()
    {
        _x = Input.GetAxisRaw("Horizontal");
        _y = Input.GetAxisRaw("Vertical");
        Debug.Log(Input.GetMouseButtonDown(0)+ "yo");
        if (Input.GetMouseButtonDown(0))
            GrabInteractible();

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed right-click.");


    }
    private void GrabInteractible()
    {
        if (!_cameraRaycast.IsHoverAnInteractible())
        {
            return;
        }

        _itemCarried = _cameraRaycast.lastHoverInteractible;
        Debug.Log(_itemCarried.gameObject.name + "");
        _itemCarried.transform.parent = RightHandAnchor.transform;



    }


    private void Movement()
    {
        //Extra gravity
        _rb.AddForce(Vector3.down * Time.deltaTime * 10);

        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();

        //Rectify the movement
        CounterMovement(_x, _y, mag);
        LimitSpeed(mag);

        //Apply forces to move player
        _rb.AddForce(orientation.transform.forward * _y * moveSpeed * Time.deltaTime, forceMode);
        _rb.AddForce(orientation.transform.right * _x * moveSpeed * Time.deltaTime, forceMode);
    }

    /// <summary>
    /// If speed is larger than maxspeed, cancel out the input so you don't go over max speed
    /// </summary>
    private void LimitSpeed(Vector2 mag)
    {
        if (_x > 0 && mag.x > maxSpeed) _x = 0;
        if (_x < 0 && mag.x < -maxSpeed) _x = 0;
        if (_y > 0 && mag.y > maxSpeed) _y = 0;
        if (_y < 0 && mag.y < -maxSpeed) _y = 0;
    }

    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;

        //Find current look rotation
        Vector3 rotationCamera = playerCam.transform.localRotation.eulerAngles;
        desiredX = rotationCamera.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }
    /// <summary>
    /// Counter the sliding of the player movement caused by adding force velocity to the player rigibody;
    /// </summary>
    private void CounterMovement(float x, float y, Vector2 mag)
    {
        // left / right velocity control
        if (Math.Abs(mag.x) > _threshold && Math.Abs(x) < 0.05f || (mag.x < -_threshold && x > 0) || (mag.x > _threshold && x < 0))
        {
            _rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovementForce);
        }
        // forward / backward velocity control
        if (Math.Abs(mag.y) > _threshold && Math.Abs(y) < 0.05f || (mag.y < -_threshold && y > 0) || (mag.y > _threshold && y < 0))
        {
            _rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovementForce);
        }
    }

    /// <summary>
    /// Find the velocity relative to where the player is looking
    /// Useful for vectors calculations regarding movement and limiting movement
    /// </summary>
    /// <returns></returns>
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(_rb.linearVelocity.x, _rb.linearVelocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = _rb.linearVelocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

}



