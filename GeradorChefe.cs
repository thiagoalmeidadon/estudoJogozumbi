using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{

    private float tempoParaAProximaGeracao = 0;
    public float tempoEntreGeracoes = 10;
    public GameObject ChefePrefab;
    private ControlaInterface scriptControlaInterface;
    public Transform[] PosicoesPossiveisDeGeracao;
    private Transform jogador;

    private void Start()
    {
        tempoParaAProximaGeracao = tempoEntreGeracoes;
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag(Tags.Jogador).transform;
    }


    private void Update()
    {
        if (Time.timeSinceLevelLoad > tempoParaAProximaGeracao)
        {
            Vector3 posicaoDeCriacao = CalcularPosicaoMaisDistante();
            Instantiate(ChefePrefab, posicaoDeCriacao, Quaternion.identity);
            scriptControlaInterface.aparecerTextoChefeCriado();
            tempoParaAProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }

    //  calcula a maior posicao dentro de um vetor comparado com o personagem 
    Vector3 CalcularPosicaoMaisDistante()
    {
        Vector3 posicaoMaisDistancia = Vector3.zero;
        float maiorDistancia = 0;

        foreach (Transform posicao in PosicoesPossiveisDeGeracao)
        {
            float distanciaEntreOJogador = Vector3.Distance(posicao.position, jogador.position);
            if(distanciaEntreOJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreOJogador;
                posicaoMaisDistancia = posicao.position;
            }
        }

        return posicaoMaisDistancia;
    }

}
