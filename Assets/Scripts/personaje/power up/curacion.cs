using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class curacion : MonoBehaviour
{
    
    
    public BarraDeVida barraDeVida;  //el primer BarraDeVida es referencia a la clase.. el segundo barradevida es una variable que contiene todos los datos de la clase BarraDeVida

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("curacion"))// si colisiopna con pildora

        {
            Destroy(other.gameObject);
            
           // GetComponent<BarraDeVida>().Heal(20); 
            
            barraDeVida.Heal(20);
            barraDeVida.UpdateHealthBar();



        }
    }
   
    // Update is called once per frame
    void Update()
    {
        barraDeVida.UpdateHealthBar();
    }
}
