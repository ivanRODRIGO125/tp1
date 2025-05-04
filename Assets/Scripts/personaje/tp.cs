using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tp : MonoBehaviour
{
    public GameObject Player;
    public GameObject tpPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tp"))// si colisiopna con pildora

        {Player.transform.position = tpPosition.transform.position;
            



        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
