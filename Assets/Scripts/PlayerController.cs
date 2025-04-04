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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        currentSlowMoEnergy = maxSlowMoEnergy;
    }

    void Update()
    {
        HandleSlowMotion();
        Move();
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
        controller.Move(moveSpeed * Time.unscaledDeltaTime * move);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.unscaledDeltaTime;
        controller.Move(velocity * Time.unscaledDeltaTime);
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
        bool hasEnoughEnergy = currentSlowMoEnergy >= 0.2f;

        if (wantsToSlowTime && hasEnoughEnergy)
        {
            isSlowMotionActive = true;
            Time.timeScale = 0.3f;
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
}