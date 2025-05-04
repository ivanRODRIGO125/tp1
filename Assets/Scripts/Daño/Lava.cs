using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))// si colisiopna con pildora

        {
            Destroy(other.gameObject);
            SceneManager.LoadScene(0);




        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
