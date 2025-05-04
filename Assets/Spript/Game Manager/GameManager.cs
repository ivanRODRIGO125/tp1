using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public BarraDeVida playerHealth;
    public int totalCoins;
    public GameObject PanelVictoria;
    public bool Reiniciar = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()


    {
         totalCoins = Monedas.totalCoins;
        // Verifica si la vida llegó a 0
        if (playerHealth.currentHealth <= 0)
        {
            GameOver();
        }
        if(Input.GetKeyDown(KeyCode.R) && Reiniciar == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia la escena
        }

        
    }

    public void CheckWinCondition(int coinsCollected)
    {
        if (coinsCollected >= totalCoins)
        {
            WinGame();
        }
    }
    
    
   

    void GameOver()
    {
       
        Debug.Log("Game Over! Reiniciando...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia la escena
    }

    void WinGame()
    {
        Reiniciar = true;
       PanelVictoria.SetActive(true);
        Debug.Log("¡Has ganado!");
        // Aquí puedes agregar una pantalla de victoria
    }


    
}
