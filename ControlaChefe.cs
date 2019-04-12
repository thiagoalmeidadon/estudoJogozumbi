using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ControlaChefe : MonoBehaviour, IMatavel {

    private Transform jogador;
    private NavMeshAgent agente;
    private Status statusChefe;
    private AnimacaoPersonagem animacaoChefe;
    private MovimentoPersonagem movimentoChefe;
    public Slider SliderVidaChefe;


    
    private void Start()
    {
        jogador = GameObject.FindWithTag(Tags.Jogador).transform;
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentoChefe = GetComponent<MovimentoPersonagem>();
        SliderVidaChefe.maxValue = statusChefe.VidaInicial;
        AtualizarInterface();

        agente.speed = statusChefe.Velocidade;


        
    }

    private void Update()
    {
        agente.SetDestination(jogador.position);

        animacaoChefe.Movimentar(agente.velocity.magnitude);

        if(agente.hasPath == true)
        {

            bool estouPertoDoJogador = agente.remainingDistance <= agente.stoppingDistance;

            if(estouPertoDoJogador)
            {
                animacaoChefe.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimentoChefe.Rotacionar(direcao);
            } else
            {
                animacaoChefe.Atacar(false);
            }

        }
    }


    void AtacaJogador()
    {
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        statusChefe.Vida -= dano;
        AtualizarInterface();
        if (statusChefe.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        animacaoChefe.Morrer();
        movimentoChefe.Morrer();
        this.enabled = false;
        agente.enabled = false;
        Destroy(gameObject, 3);
    }


    void AtualizarInterface()
    {
        SliderVidaChefe.value = statusChefe.Vida;
    }
}
