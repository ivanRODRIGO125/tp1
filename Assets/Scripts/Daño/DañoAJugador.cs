using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoAJugador : MonoBehaviour
{
    [Header("Damage Settings")]
    public BarraDeVida playerHealth;
    public float damageAmount = 10f; // Cantidad de daño total
    public float damageDuration = 2f; // Tiempo en el que se aplicará el daño
    public float damageInterval = 1f; // Tiempo entre repeticiones de daño
    private Coroutine damageCoroutine;
    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            playerInside = true;
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(ApplyDamageOverTime());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            playerInside = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine); // Detener el daño al salir de la zona
                damageCoroutine = null;
            }
        }
    }

    IEnumerator ApplyDamageOverTime()
    {
        while (playerInside)
        {
            float elapsedTime = 0f;
            float damagePerSecond = damageAmount / damageDuration ;

            while (elapsedTime < damageDuration && playerInside)
            {
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime ); // Reducir vida gradualmente
                elapsedTime += Time.deltaTime;
                yield return null; // Esperar al siguiente frame
            }
            yield return new WaitForSeconds(damageInterval); // Esperar antes de volver a hacer daño
        }
        damageCoroutine = null; // Restablecer la variable al terminar
    }
}