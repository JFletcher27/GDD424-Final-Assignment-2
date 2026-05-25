using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private GameLogic gameLogic;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject controlGuide;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 6f;

    [Header("Camera Settings")]
    [SerializeField] private float lookSensitivity = 1.5f;
    [SerializeField] private float cameraPitchMin = -30f;
    [SerializeField] private float cameraPitchMax = 60f;

    [Header("State")]
    [SerializeField] private PlayerTraversalState currentState = PlayerTraversalState.Idle;

    [Header("Input Readout")]
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector2 lookInput;
    public bool FireShot;

    [Header("Player Attributes")]
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float currentHealth;
    public float playerLevel = 1;
    public float experiencePoints;


    [Header("Camera Readout")]
    [SerializeField] private float yaw;
    [SerializeField] private float pitch;

    [Header("Respawn & Reload")]
    [SerializeField] private Transform LastCampfirePos;
    [SerializeField] private bool isPlayerDead;
    public bool isPlayerReloading;



    private void Awake()
    {
        if (currentHealth != maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (rb != null)
        {
            rb.freezeRotation = true;
        }
        if (cameraPivot != null)
        {
            yaw = cameraPivot.eulerAngles.y;
        }
        else
        {
            yaw = transform.eulerAngles.y;
        }
    }

    private void Update()
    {
        if (isPlayerDead != true)
        {
            UpdateState();
            HandleCameraRotation();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.position = new Vector3(LastCampfirePos.position.x, LastCampfirePos.position.y, LastCampfirePos.position.z);
                Debug.Log(LastCampfirePos.position);
                currentHealth = maxHealth;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerDead != true)
        {
            HandleMovement();
        }
        Death();
        UI();
    }

    private void UpdateState()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            currentState = PlayerTraversalState.Walk;
        }
        else
        {
            currentState = PlayerTraversalState.Idle;
        }
    }

    private void HandleMovement()
    {
        if (rb == null)
        {
            Debug.LogError("RigidBody not found");
            return;
        }

        Vector3 moveDirection = GetCameraRelativeMoveDirection();

        Vector3 targetVelocity = moveDirection * moveSpeed;

        targetVelocity.y = rb.velocity.y;

        rb.velocity = targetVelocity;

        if (currentState == PlayerTraversalState.Walk && moveDirection.sqrMagnitude > 0.01f)
        {
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private Vector3 GetCameraRelativeMoveDirection()
    {
        if (cameraPos == null)
        {
            return new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        }

        Vector3 cameraForward = cameraPos.forward;
        Vector3 cameraRight = cameraPos.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraForward * moveInput.y) + (cameraRight * moveInput.x);

        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection.Normalize();
        }

        return moveDirection;
    }

    private void HandleCameraRotation()
    {
        if (cameraPivot == null) return;

        yaw += lookInput.x * lookSensitivity;
        pitch -= lookInput.y * lookSensitivity;

        pitch = Mathf.Clamp(pitch, cameraPitchMin, cameraPitchMax);

        cameraPivot.rotation = Quaternion.Euler(pitch, yaw, 0f);

        if (cameraPos != null)
        {
            cameraPos.localRotation = Quaternion.identity;
        }
    }

    private void UI()
    {
        deathScreen.SetActive(isPlayerDead);
        controlGuide.SetActive(!isPlayerDead);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void OnShoot(InputValue value)
    {
        if (value.isPressed && isPlayerDead != true && isPlayerReloading != true)
        {
            FireShot = true;
        }
    }

    private void Death()
    {
        if (currentHealth <= 0)
        {
            isPlayerDead = true;
            StartCoroutine(DeathTime());
            currentHealth = maxHealth;
        }

    }

    private IEnumerator DeathTime()
    {
        Debug.Log("You Have Died!");
        yield return new WaitForSeconds(5);
        if (LastCampfirePos != null)
        {
            transform.position = LastCampfirePos.position;
        }
        isPlayerDead = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Campfire"))
        {
            LastCampfirePos = other.gameObject.transform;
            Debug.Log("Campfire last visited is" + LastCampfirePos.position);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= gameLogic.bulletDamage;
        }
    }
}
