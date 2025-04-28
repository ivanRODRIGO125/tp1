using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f; // Velocidad de caminata del jugador
    public float runSpeed = 10f; // Velocidad al correr
    public float jumpForce = 5f; // Fuerza del salto
    public float rotationSpeed = 10f; // Velocidad de rotación del personaje
    public float cameraFollowSpeed = 5f; // Velocidad de seguimiento de la cámara

    [Header("Camera Settings")]
    public float cameraDistance = 5f; // Distancia de la cámara al personaje
    public float cameraHeight = 2f; // Altura de la cámara respecto al personaje
    public float cameraSmoothTime = 0.1f; // Suavidad del seguimiento de la cámara
    public float mouseSensitivity = 2f; // Sensibilidad del mouse

    [Header("References")]
    public Transform playerCamera; // Referencia a la cámara del jugador
    public Transform cameraPivot; // Punto de pivote de la cámara
    public Rigidbody rb; // Referencia al Rigidbody del jugador

    private bool isGrounded; // Indica si el jugador está en el suelo
    private Vector3 cameraVelocity = Vector3.zero; // Velocidad de la cámara (para suavizado)
    private float yaw; // Rotación horizontal de la cámara
    private float pitch; // Rotación vertical de la cámara

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Oculta y bloquea el cursor al centro de la pantalla
        rb.freezeRotation = true; // Evita que la física afecte la rotación del personaje
    }

    void Update()
    {
        HandleMovement(); // Manejar el movimiento del personaje
        HandleCameraRotation(); // Manejar la rotación de la cámara
    }

    void FixedUpdate()
    {
        SmoothCameraFollow(); // Actualizar la posición de la cámara de forma suave
    }

    void HandleMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed; // Determinar si el personaje camina o corre
        float moveX = Input.GetAxis("Horizontal"); // Entrada en el eje horizontal (A/D o flechas izquierda/derecha)
        float moveZ = Input.GetAxis("Vertical"); // Entrada en el eje vertical (W/S o flechas arriba/abajo)

        // Calcular la dirección de movimiento basada en la orientación de la cámara
        Vector3 move = (cameraPivot.forward * moveZ + cameraPivot.right * moveX).normalized;
        move.y = 0; // Mantener el movimiento en el plano horizontal

        // Si hay movimiento, rotar el personaje en la dirección deseada
        if (move.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Aplicar el movimiento al Rigidbody
        rb.velocity = new Vector3(move.x * speed, rb.velocity.y, move.z * speed);

        // Manejar el salto
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleCameraRotation()
    {
        // Obtener la entrada del mouse para rotación
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX; // Rotación horizontal (izquierda/derecha)
        pitch -= mouseY; // Rotación vertical (arriba/abajo)
        pitch = Mathf.Clamp(pitch, -30f, 60f); // Limitar la inclinación de la cámara

        // Aplicar la rotación al pivote de la cámara
        cameraPivot.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void SmoothCameraFollow()
    {
        // Calcular la posición de la cámara detrás del personaje
        Vector3 targetPosition = transform.position - cameraPivot.forward * cameraDistance + Vector3.up * cameraHeight;
        // Aplicar un suavizado en el movimiento de la cámara
        playerCamera.position = Vector3.SmoothDamp(playerCamera.position, targetPosition, ref cameraVelocity, cameraSmoothTime);
        // Hacer que la cámara mire al personaje
        playerCamera.LookAt(transform.position + Vector3.up * 1.5f);
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true; // Si el personaje está tocando el suelo, permitir el salto
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false; // Si el personaje deja de tocar el suelo, deshabilitar el salto
    }
}
