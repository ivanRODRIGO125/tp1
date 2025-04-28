using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f; // Velocidad de caminata del jugador
    public float runSpeed = 10f; // Velocidad al correr
    public float jumpForce = 5f; // Fuerza del salto
    public float rotationSpeed = 10f; // Velocidad de rotaci�n del personaje
    public float cameraFollowSpeed = 5f; // Velocidad de seguimiento de la c�mara

    [Header("Camera Settings")]
    public float cameraDistance = 5f; // Distancia de la c�mara al personaje
    public float cameraHeight = 2f; // Altura de la c�mara respecto al personaje
    public float cameraSmoothTime = 0.1f; // Suavidad del seguimiento de la c�mara
    public float mouseSensitivity = 2f; // Sensibilidad del mouse

    [Header("References")]
    public Transform playerCamera; // Referencia a la c�mara del jugador
    public Transform cameraPivot; // Punto de pivote de la c�mara
    public Rigidbody rb; // Referencia al Rigidbody del jugador

    private bool isGrounded; // Indica si el jugador est� en el suelo
    private Vector3 cameraVelocity = Vector3.zero; // Velocidad de la c�mara (para suavizado)
    private float yaw; // Rotaci�n horizontal de la c�mara
    private float pitch; // Rotaci�n vertical de la c�mara

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Oculta y bloquea el cursor al centro de la pantalla
        rb.freezeRotation = true; // Evita que la f�sica afecte la rotaci�n del personaje
    }

    void Update()
    {
        HandleMovement(); // Manejar el movimiento del personaje
        HandleCameraRotation(); // Manejar la rotaci�n de la c�mara
    }

    void FixedUpdate()
    {
        SmoothCameraFollow(); // Actualizar la posici�n de la c�mara de forma suave
    }

    void HandleMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed; // Determinar si el personaje camina o corre
        float moveX = Input.GetAxis("Horizontal"); // Entrada en el eje horizontal (A/D o flechas izquierda/derecha)
        float moveZ = Input.GetAxis("Vertical"); // Entrada en el eje vertical (W/S o flechas arriba/abajo)

        // Calcular la direcci�n de movimiento basada en la orientaci�n de la c�mara
        Vector3 move = (cameraPivot.forward * moveZ + cameraPivot.right * moveX).normalized;
        move.y = 0; // Mantener el movimiento en el plano horizontal

        // Si hay movimiento, rotar el personaje en la direcci�n deseada
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
        // Obtener la entrada del mouse para rotaci�n
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX; // Rotaci�n horizontal (izquierda/derecha)
        pitch -= mouseY; // Rotaci�n vertical (arriba/abajo)
        pitch = Mathf.Clamp(pitch, -30f, 60f); // Limitar la inclinaci�n de la c�mara

        // Aplicar la rotaci�n al pivote de la c�mara
        cameraPivot.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void SmoothCameraFollow()
    {
        // Calcular la posici�n de la c�mara detr�s del personaje
        Vector3 targetPosition = transform.position - cameraPivot.forward * cameraDistance + Vector3.up * cameraHeight;
        // Aplicar un suavizado en el movimiento de la c�mara
        playerCamera.position = Vector3.SmoothDamp(playerCamera.position, targetPosition, ref cameraVelocity, cameraSmoothTime);
        // Hacer que la c�mara mire al personaje
        playerCamera.LookAt(transform.position + Vector3.up * 1.5f);
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true; // Si el personaje est� tocando el suelo, permitir el salto
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false; // Si el personaje deja de tocar el suelo, deshabilitar el salto
    }
}
