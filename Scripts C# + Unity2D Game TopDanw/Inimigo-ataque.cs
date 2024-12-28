// Script para o inimigo relizar um taque em um game TopDanw 2D na Unity:

using System.Collections;
using UnityEngine;

public class AtaqueInimigo : MonoBehaviour

    {
    public int vidaDoInimigo = 10;
    public Animator animador;

    private Renderer renderizador;
    private Collider2D colisor;
    private bool isDead = false;
    private bool isOnCooldown = false; // Variável para controlar o cooldown

    public float cooldownTempo = 1.5f; // Tempo de cooldown entre ataques, ajuste conforme necessário
    public float alcanceAtaque = 5f; // Distância em que o inimigo pode atacar o player
    private Transform playerTransform;

    void Start()
    {
        if (animador == null)
        {
            animador = GetComponent<Animator>();
        }
        renderizador = GetComponent<Renderer>();
        colisor = GetComponent<Collider2D>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Assumindo que o player tenha a tag "Player"
    }

    void Update()
    {
        if (isDead) return; // Impede ações após a morte do inimigo

        // Verifica a distância até o player
        float distancia = Vector2.Distance(transform.position, playerTransform.position);

        // Se o player estiver dentro do alcance de ataque, o inimigo ataca
        if (distancia <= alcanceAtaque && !isOnCooldown)
        {
            StartCoroutine(Atacar());
        }
    }

    public void InimigoRecebeDano(int dano)
    {
        if (isDead) return; // Impede dano após a morte

        vidaDoInimigo -= dano;
        Debug.Log("Inimigo levou " + dano + " de dano! Vida restante: " + vidaDoInimigo);

        if (vidaDoInimigo <= 0)
        {
            Morrer();
        }
        else if (animador != null)
        {
            animador.SetTrigger("Machucado");
        }
    }

    void Morrer()
    {
        if (isDead) return; // Impede múltiplas execuções da morte
        isDead = true;

        Debug.Log("Inimigo morreu!");

        if (renderizador != null) renderizador.enabled = false;
        if (colisor != null) colisor.enabled = false;

        if (animador != null)
        {
            animador.SetTrigger("Morte");
        }

        StartCoroutine(DestruirInimigo());
    }

    IEnumerator DestruirInimigo()
    {
        yield return new WaitForSeconds(1f); // Ajuste o tempo conforme necessário
        Destroy(gameObject); // Remove o objeto permanentemente da cena
    }

    IEnumerator Atacar()
    {
        if (isDead) yield break; // Se o inimigo estiver morto, não realiza o ataque
        isOnCooldown = true; // Inicia o cooldown
        animador.SetTrigger("Atacar"); // Inicia a animação de ataque
        Debug.Log("Inimigo está atacando de volta!");

        // Aguarda o tempo do cooldown antes de permitir outro ataque
        yield return new WaitForSeconds(cooldownTempo);

        animador.SetTrigger("Idle"); // Retorna para o estado Idle após o ataque

        isOnCooldown = false; // Reseta o cooldown para permitir outro ataque
    }

    // Usando OnTriggerEnter2D para detectar o player entrando na zona de ataque (caso queira usar colisões)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOnCooldown && !isDead)
        {
            StartCoroutine(Atacar()); // Inicia o ataque se o player entrar na zona
        }
    }
