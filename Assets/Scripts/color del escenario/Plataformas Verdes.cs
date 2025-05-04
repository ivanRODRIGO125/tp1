using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformasVerdes : MonoBehaviour
{
    private Renderer Plataforma;
    // Start is called before the first frame update
    private void Start()
    {
        Plataforma = GetComponent<Renderer>();

    }
    // Update is called once per frame
    void Update()
    {
        Plataforma.material.color = Color.green;
    }
}
