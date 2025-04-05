using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpHeight = 1.2f;
    public float gravity = -9.81f;

    [Header("Camera")]
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 80f;

    private CharacterController controller;
    private Vector3 velocity;
    private float verticalLookRotation = 0f;
    private bool isSlowMotionActive = false;

    [Header("Slow Motion Settings")]
    public float maxSlowMoEnergy = 5f;
    public float slowMoDrainRate = 1f;       // energy per second while active
    public float slowMoRegenRate = 0.5f;     // energy per second while not active

    [HideInInspector]
    public float currentSlowMoEnergy;
    private bool isOnMovingPlatform;
    private Vector3 platformVelocity;
    private float ySpeed = 0f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        currentSlowMoEnergy = maxSlowMoEnergy;
    }

    void Update()
    {
        HandleSlowMotion();
        //Move();
        Move1();
        Look();
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, (controller.height / 2f) + 0.1f);
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(moveSpeed * Time.deltaTime * move);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
       


        // Apply platform movement if on a moving platform
        if (isOnMovingPlatform)
        {
            velocity += platformVelocity;
        }

        // Apply movement
        controller.Move(velocity * Time.deltaTime);
    }
    void Move1()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        moveDir = transform.TransformDirection(moveDir); // local rotation

        // Gravity and jumping
        if (IsGrounded())
        {
            if (ySpeed < 0)
                ySpeed = -2f;

            if (Input.GetButtonDown("Jump"))
                ySpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            ySpeed += gravity * Time.unscaledDeltaTime;
        }

        Vector3 finalMove = moveDir * moveSpeed + Vector3.up * ySpeed;

        if (isOnMovingPlatform)
            finalMove += platformVelocity;

        controller.Move(finalMove * Time.unscaledDeltaTime); 
    }
    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -maxLookAngle, maxLookAngle);

        cameraHolder.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleSlowMotion()
    {
        bool wantsToSlowTime = Input.GetMouseButton(1);
        bool wantsToFastForward = Input.GetMouseButton(0);
        bool hasEnoughEnergy = currentSlowMoEnergy >= 0.2f;

        if (wantsToSlowTime && hasEnoughEnergy)
        {
            isSlowMotionActive = true;
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            currentSlowMoEnergy -= slowMoDrainRate * Time.unscaledDeltaTime;
            currentSlowMoEnergy = Mathf.Clamp(currentSlowMoEnergy, 0f, maxSlowMoEnergy);
        }
        else if (wantsToFastForward && hasEnoughEnergy)
        {
            isSlowMotionActive = false;
            Time.timeScale = 1.5f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            currentSlowMoEnergy -= slowMoDrainRate * Time.unscaledDeltaTime;
            currentSlowMoEnergy = Mathf.Clamp(currentSlowMoEnergy, 0f, maxSlowMoEnergy);
        }
        else
        {
            isSlowMotionActive = false;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;

            currentSlowMoEnergy += slowMoRegenRate * Time.unscaledDeltaTime;
            currentSlowMoEnergy = Mathf.Clamp(currentSlowMoEnergy, 0f, maxSlowMoEnergy);
        }
        

        float targetFOV = isSlowMotionActive ? 75f : 90f;
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.unscaledDeltaTime * 5f);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the player is standing on a moving platform
        MoverScript platform = hit.collider.GetComponent<MoverScript>();
        if (platform != null && hit.normal.y > 0.9f)
        {
            // Player has landed on a moving platform
            isOnMovingPlatform = true;
            // Calculate the platform's velocity
            platformVelocity = platform.speedVector;
            Debug.Log("is on platform");
        }
        else
        {
            // Player is no longer on a moving platform
            isOnMovingPlatform = false;
            platformVelocity = Vector3.zero;
        }
    }
}