// Script para fazer o player realizar sua movimentação em um Game Top Danw na Unity:
playerController

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControle : MonoBehaviour
{
    public float velocidade = 5f;
    private Rigidbody2D rb;
    public Animator playerAnimator;
    bool isWalking;

    public AudioSource attackSound;
    public AudioSource stepSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isWalking = false;
    }


    void Update()
    {
        float eixoX = Input.GetAxisRaw("Horizontal") * velocidade;
        float eixoY = Input.GetAxisRaw("Vertical") * velocidade;
        isWalking = eixoX != 0 || eixoY != 0;


        rb.velocity = new Vector2(eixoX, eixoY);
        if (isWalking)
        {
            playerAnimator.SetFloat("eixoX", eixoX);
            playerAnimator.SetFloat("eixoY", eixoY);

            if (!stepSound.isPlaying)
            {
                stepSound.Play();
            }
        }
        else
        {
            stepSound.Stop();
        }
        playerAnimator.SetBool("isWalking", isWalking);

        if (Input.GetButtonDown("Fire1"))
        {
            playerAnimator.SetTrigger("attack");
            attackSound.Play();
        }

    }
}