using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorGreen : MonoBehaviour
{
    public GameObject Player;
    public Material Material;
    public bool TienePildoraVerde;
    public bool CambioVerde;

    private Renderer player;


    private void Start()
    {
        player = GetComponent<Renderer>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Green"))// si colisiopna con pildora

        {
            Destroy(other.gameObject);
            TienePildoraVerde = true;

            

        }



    }
    



    // Update is called once per frame
    void Update()
    {
        if (TienePildoraVerde == true && Input.GetKeyDown(KeyCode.C))
        {
            player.material.color = Color.green;
        }




        

    }
}
