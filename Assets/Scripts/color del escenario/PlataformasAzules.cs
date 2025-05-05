using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformasAzules : MonoBehaviour
{
    public Renderer Plataforma;
    // Start is called before the first frame update
    private void Start()
    {
        Plataforma = GetComponent<Renderer>();
        Plataforma.material.color = Color.blue;
    }
    // Update is called once per frame
    void Update()
    {
        //Plataforma.material.color = Color.blue;
    }
}
