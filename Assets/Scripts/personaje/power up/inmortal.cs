using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inmortal : MonoBehaviour
{
    public GameObject Player;
    public bool TienePildoraInmortal;
    public bool Inmortal;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("inmortal"))// si colisiopna con pildora

        {
            Destroy(other.gameObject);
            TienePildoraInmortal=true;



        }
    }

    // Update is called once per frame
    void Update()
    {if ( TienePildoraInmortal=true && Input.GetKeyDown(KeyCode.C)) { Inmortal = true;
        
        
       if(GetComponent<BarraDeVida>().currentHealth < 0) { GetComponent<BarraDeVida> ().currentHealth =1; }
        

    }
}
}

