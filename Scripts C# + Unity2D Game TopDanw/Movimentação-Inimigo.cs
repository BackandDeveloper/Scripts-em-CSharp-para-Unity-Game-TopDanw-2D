// Script para movimentação de um inimigo em um Game TopDanw 2D na Unity: 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentoInimigo : MonoBehaviour
{
    private Rigidbody2D rb;
    public List<Transform> waypoints;
    public float velocidade = 5f;
    int proximoPonto = 0;
    public Animator animator;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D não foi encontrado no objeto " + gameObject.name);
        }
    }

    void Update()
    {

        if (rb == null) return;


        if (waypoints.Count > 0)
        {

            Vector2 direcao = (waypoints[proximoPonto].position - transform.position).normalized;
            rb.MovePosition((Vector2)transform.position + (direcao * velocidade * Time.deltaTime));

            animator.SetFloat("eixoX", direcao.x);
            animator.SetFloat("eixoY", direcao.y);


            if (Vector2.Distance(transform.position, waypoints[proximoPonto].position) <= 0.1f)
            {

                proximoPonto = (proximoPonto + 1) % waypoints.Count;
            }
        }
    }
}