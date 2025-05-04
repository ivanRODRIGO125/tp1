using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarraDeVida : MonoBehaviour
{
    public GameObject Player;
    [Header("Health Settings")]
    public float maxHealth = 100f; // Vida máxima del jugador
    public float currentHealth; // Vida actual del jugador
    public Image healthBar; // Referencia a la barra de vida (Image con fillAmount)
    
    void Start()
    {
        currentHealth = maxHealth; // Inicializar la vida al máximo
        UpdateHealthBar(); // Actualizar la barra al inicio

    }
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(15); // Curación de prueba
            UpdateHealthBar(); // Actualizar la barra al inicio
        }




        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10);
        }
        UpdateHealthBar(); // Actualizar la barra al inicio
        
            }  
        

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; // Reducir la vida
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Evitar valores negativos
        UpdateHealthBar(); // Actualizar la UI

        if (currentHealth <= 0)
        {
            Die(); // Llamar a la función de muerte si la vida llega a 0
        }
    }
    public void Heal(float healAmount)
    {
        currentHealth += healAmount; // Aumentar la vida
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Evitar que se pase de la vida máxima
        UpdateHealthBar(); // Actualizar la UI
    }
    public void UpdateHealthBar()
    {
        
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth; // Actualizar la barra de vida
        }
    }

    void Die()
    {
        Debug.Log("El jugador ha muerto");
        // Aquí puedes agregar lógica adicional como respawn o pantalla de muerte
    }



    
}