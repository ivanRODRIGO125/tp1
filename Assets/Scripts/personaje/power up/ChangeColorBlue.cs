using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorBlue : MonoBehaviour
{
    public GameObject Player;
    public Material Material;
    public bool TienePildoraVerde;
    public bool CambioVerde;

    private Renderer player;


    private void Start()
    {
        player = GetComponent<Renderer>();
        player.material.color = Color.blue;
    }
    


    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.X))
        {
            player.material.color = Color.blue;
        }






    }
}
