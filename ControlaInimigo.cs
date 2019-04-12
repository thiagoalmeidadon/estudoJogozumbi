using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel {

	public GameObject Jogador;
	// public float Velocidade = 5;
    // private Rigidbody contInimigo;
    // private Animator animInimigo;
    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusInimigo;
    public AudioClip SomZumbiMorrendo;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorioas = 4;
    private float porcentagemGerarKitMedico = 0.1f;
    public GameObject KitMedicoPrefab;
    private ControlaInterface scriptControlaInterface;
    [HideInInspector]
    public GeradorZumbis meuGerador;
    public GameObject ParticulaSangueZumbi;


	// Use this for initialization
	void Start () {
        Jogador = GameObject.FindWithTag(Tags.Jogador);
        AleatorizarZumbi();


        // contInimigo = GetComponent<Rigidbody>();
        // animInimigo = GetComponent<Animator>();

        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        statusInimigo = GetComponent<Status>();
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;

	}
	

    void FixedUpdate()
    {


        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
       

        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);

        if(distancia > 15)
        {
            Vagar();
        }

        else if (distancia > 2.5)
        {
            direcao = Jogador.transform.position - transform.position;

            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);

            animacaoInimigo.Atacar(false);

        } else
        {
            direcao = Jogador.transform.position - transform.position;
            animacaoInimigo.Atacar(true);
        }
			

	}

    void AtacaJogador ()
    {
        /*  versao antiga, encapsular , cada objeto fica com o seu comportamento 
            Time.timeScale = 0;
            Jogador.GetComponent<ControlaJogador>().TextoGameOver.SetActive(true);
            Jogador.GetComponent<ControlaJogador>().Vivo = false;*/

        // foi criado um método no jogador que controla a vida 
        int dano = Random.Range(20, 30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizarZumbi()
    {
        int geraTipoZumbi = Random.Range(1, 4);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int Dano)
    {
        statusInimigo.Vida -= Dano;

        if (statusInimigo.Vida <= 0)
            Morrer();

    }


    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(ParticulaSangueZumbi, posicao, rotacao);
        
    }

    public void Morrer()
    {
        Destroy(gameObject, 2);
        animacaoInimigo.Morrer();
        movimentaInimigo.Morrer();
        ControlaAudio.instancia.PlayOneShot(SomZumbiMorrendo);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos();
        meuGerador.DiminuirQuantidadeDeZumbisVivos();
        this.enabled = false;
    }

    void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if(Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;

        if(contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorioas + Random.Range(-1f, 1f);
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;

        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Rotacionar(direcao);
        }
        
        
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;
        return posicao;
    }
}
