using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpPlataformaOculta : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject tpPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tp oculto"))// si colisiopna con pildora

        {
            Player.transform.position = tpPosition.transform.position;




        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
