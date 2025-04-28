using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBall : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f; // Velocidad de la bola
    public float jumpForce = 5f; // Fuerza de salto
    public float rotationSpeed = 10f; // Suavidad de la rotación
    private Rigidbody rb;
    private bool isGrounded;

    [Header("Camera Settings")]
    public Transform playerCamera; // Cámara principal
    public Transform cameraPivot; // Objeto vacío que sirve de pivote
    public float cameraDistance = 5f; // Distancia de la cámara
    public float cameraHeight = 2f; // Altura de la cámara
    public float cameraSmoothTime = 0.1f; // Suavidad del seguimiento de la cámara
    public float mouseSensitivity = 2f; // Sensibilidad del mouse
    private Vector3 cameraVelocity = Vector3.zero;
    private float yaw, pitch;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
    }

    void FixedUpdate()
    {
        SmoothCameraFollow();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Dirección basada en la cámara
        Vector3 moveDirection = cameraPivot.forward * moveZ + cameraPivot.right * moveX;
        moveDirection.y = 0f; // Evitar que se incline
        moveDirection.Normalize();

        // Aplicar fuerza al Rigidbody
        rb.AddForce(moveDirection * speed, ForceMode.Force);

        // Si hay movimiento, rotar la bola en la dirección deseada
        if (moveDirection.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        cameraPivot.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void SmoothCameraFollow()
    {
        Vector3 targetPosition = transform.position - cameraPivot.forward * cameraDistance + Vector3.up * cameraHeight;
        playerCamera.position = Vector3.SmoothDamp(playerCamera.position, targetPosition, ref cameraVelocity, cameraSmoothTime);
        playerCamera.LookAt(transform.position + Vector3.up * 1.5f);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}