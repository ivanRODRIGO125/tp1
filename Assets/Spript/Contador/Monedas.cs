using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Monedas : MonoBehaviour
{
   static public int totalCoins = 10; // Se asigna en el GameManager
    private int coinsCollected = 0;
    public TextMeshProUGUI coinCounterText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject); // Elimina la moneda
            coinsCollected++;
            UpdateUI();
            GameManager.Instance.CheckWinCondition(coinsCollected);
        }
    }

    void UpdateUI()
    {
        coinCounterText.text = "Monedas: " + coinsCollected + "/" + totalCoins;
    }
}