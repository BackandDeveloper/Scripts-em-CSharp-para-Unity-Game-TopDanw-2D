// Script que termina o ataque do Player em um jogo TopDanw 2D na Unity!

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AtaquePlayer : MonoBehaviour
{
    private bool jogadorNoAlcance = false;
    public int vidaDoJogador = 10;
    public Slider barraDeVida;

    public int vidaMaxima = 10;
    public Transform pontoDeRespawn;
    public float delayDeRespawn = 2.0f;
    public int maximoDeRespawns = 3;
    private int contadorDeRespawn = 0;
    public bool estaMorto = false;

    private GameObject inimigoAtual;

    void Start()
    {
        if (jogadorNoAlcance && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Jogador pressionou o botão de ataque e está no alcance do inimigo!");
            if (inimigoAtual != null)
            {
                Atacar(inimigoAtual);
            }
        }
    }


    void Update()
    {
        if (jogadorNoAlcance && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Tentando atacar o inimigo...");
            if (inimigoAtual != null)
            {
                Atacar(inimigoAtual);
            }
            else
            {
                Debug.Log("Nenhum inimigo no alcance para atacar.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Inimigo"))
        {
            jogadorNoAlcance = true;
            inimigoAtual = colisao.gameObject;
            Debug.Log("Inimigo detectado no alcance para ataque.");
        }
    }

    private void OnTriggerExit2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Inimigo"))
        {
            jogadorNoAlcance = false;
            inimigoAtual = null;
            Debug.Log("Inimigo saiu do alcance.");
        }
    }

    public void Atacar(GameObject inimigo)
    {
        Debug.Log("Ataque realizado no inimigo.");
        AtaqueInimigo scriptInimigo = inimigo.GetComponent<AtaqueInimigo>();
        if (scriptInimigo != null)
        {
            scriptInimigo.InimigoRecebeDano(10);
        }
    }

    public void JogadorRecebeDano(int dano)
    {
        if (estaMorto) return;

        vidaDoJogador -= dano;
        barraDeVida.value = (float)vidaDoJogador / vidaMaxima;
        Debug.Log("Jogador recebeu " + dano + " de dano. Vida restante: " + vidaDoJogador);

        if (vidaDoJogador <= 0)
        {
            estaMorto = true;
            Debug.Log("Jogador morreu.");

            if (contadorDeRespawn < maximoDeRespawns)
            {
                contadorDeRespawn++;
                StartCoroutine(respawn());
            }
            else
            {
                FimDeJogo();
            }
        }
    }

    IEnumerator respawn()
    {
        Debug.Log("Respawn do jogador em " + delayDeRespawn + " segundos.");
        yield return new WaitForSeconds(delayDeRespawn);
        transform.position = pontoDeRespawn.position;
        transform.rotation = pontoDeRespawn.rotation;

        vidaDoJogador = vidaMaxima;
        barraDeVida.value = (float)vidaDoJogador / vidaMaxima;
        estaMorto = false;
        Debug.Log("Jogador respawnado com vida total.");
    }

    void FimDeJogo()
    {
        Debug.Log("Fim de jogo! Reiniciando a cena...");
        SceneManager.LoadScene(0);
    }
}