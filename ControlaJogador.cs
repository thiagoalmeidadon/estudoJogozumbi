using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel {

	// public float Velocidade = 10;
	Vector3 direcao;
	public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    // foi apagado do codigo antigo public bool Vivo = true;
    private Rigidbody rigidbodyJogador;
    private Animator animatorJogador;
    // public int Vida = 100;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador meuMovimentoJogador;
    public Status statusJogador;

    private void Start()
    {
        
        rigidbodyJogador = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        statusJogador = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update () {

		float eixoX = Input.GetAxis ("Horizontal");
		float eixoZ = Input.GetAxis ("Vertical");
		direcao = new Vector3 (eixoX, 0, eixoZ);

		// controlando a velocidade do movimento do personagem
		// transform.Translate (direcao * Velocidade * Time.deltaTime);
		// movimentando o personagem pelo translate 

		if (direcao != Vector3.zero)
            animatorJogador.SetBool ("Movendo", true);
		else
            animatorJogador.SetBool ("Movendo", false);


      
			
	}

	// Roda em um tempo fixo 
	void FixedUpdate() {
        meuMovimentoJogador.Movimentar(direcao, statusJogador.Velocidade);

        /* Giro do personagem 
		Quaternion giraPersonagem = Quaternion.LookRotation (direcao);
		GetComponent<Rigidbody> ().MoveRotation (giraPersonagem); */

        meuMovimentoJogador.RotacaoJogador(MascaraChao);

    }


    public void TomarDano (int dano)
    {
        statusJogador.Vida -= dano;
        scriptControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if(statusJogador.Vida <= 0)
        {
            Morrer();
        }

       
    }

    public void Morrer()
    {
        
        scriptControlaInterface.GameOver();
        // foi apagado do codigo antigo Vivo = false;
    }

    public void CurarVida(int quantidadeDeCura)
    {
        statusJogador.Vida += quantidadeDeCura;
        if(statusJogador.Vida > statusJogador.VidaInicial)
        {
            statusJogador.Vida = statusJogador.VidaInicial;
        }
        scriptControlaInterface.AtualizarSliderVidaJogador();
    }


}
