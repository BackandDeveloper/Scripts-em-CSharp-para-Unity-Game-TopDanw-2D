// Script usando para salvar um jogo tanto 2D, como 3D na Unity:

sing System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{

    public float vidaJogador = 100f;
    public bool podeSalvar = false;

    private Vector3 ultimaPosicaoSalva;
    private float ultimaVidaSalva;
    private bool temDadosSalvos = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}