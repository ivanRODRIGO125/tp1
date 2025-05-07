﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundedBlue : MonoBehaviour
{
    public tp tpReferencia;
    public BarraDeVida barraDeVida;
    // Método para comparar colores con tolerancia
    bool IsColorApproximately(Color a, Color b, float tolerance = 0.1f)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }
    private Renderer player;

    private Renderer playerRenderer;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Obtener el Renderer del objeto con el que colisionamos
        Renderer otherRenderer = collision.gameObject.GetComponent<Renderer>();

        if (otherRenderer == null) return;

        Color playerColor = playerRenderer.material.color;
        Color otherColor = otherRenderer.material.color;

        // Si el jugador es verde y el objeto con el que choca no es verde → destruir
        if (IsColorApproximately(playerColor, Color.blue) &&
            !IsColorApproximately(otherColor, Color.blue))
        {
            barraDeVida.TakeDamage(40);
            barraDeVida.UpdateHealthBar();
            tpReferencia.Player.transform.position = tpReferencia.tpPosition.transform.position;
        }
    }
}

