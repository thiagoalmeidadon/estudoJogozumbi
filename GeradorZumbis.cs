using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject Zumbi;
    float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaDeGeracao = 3;
    private float distanciaDoJogadorParaGeracao = 20;
    private GameObject jogador;
    private int quantidadeMaximaDeZumbisVivos = 3;
    private int quantidadeDeZumbisVivosAtual;
    private float tempoProximoAumentoDeDificuldade = 30;
    private float contadorAumentarDificuldade;

    private void Start()
    {
        jogador = GameObject.FindWithTag(Tags.Jogador);

        contadorAumentarDificuldade = tempoProximoAumentoDeDificuldade;

        // gerar zumbis no começo do game
        for(int i=0;  i < quantidadeMaximaDeZumbisVivos; i++) StartCoroutine(GerarNovoZumbi());

    }
    // Update is called once per frame
    void Update () {

        // verificar se pode gerar zumbi por distancia e limitar a quantidade
        // divido em variaveis e verificado no if 

        bool possoGerarZumbisPorDistancia = Vector3.Distance(transform.position, jogador.transform.position) >
            distanciaDoJogadorParaGeracao;

        bool possoGerarPorQuantidade = quantidadeDeZumbisVivosAtual < quantidadeMaximaDeZumbisVivos;

        if (possoGerarZumbisPorDistancia == true && possoGerarPorQuantidade == true )
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                // utilização do método IEnumerator
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }


        // aumentar a dificuldade do jogo por tempo 
        if(Time.timeSinceLevelLoad > contadorAumentarDificuldade)
        {
            quantidadeMaximaDeZumbisVivos++;
            contadorAumentarDificuldade = Time.timeSinceLevelLoad +
                tempoProximoAumentoDeDificuldade;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    // método que corrige o problema de loop infinito criado pelo while 
    // quando usar o metodo tem que usar  startCoroutine(metodo());
    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisor = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while(colisor.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisor = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }


        // atribuiçao mais rapida, estudar isso com mais calma para entender melhor
        // ******************* !!!!
        ControlaInimigo zumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation).
            GetComponent<ControlaInimigo>();
        zumbi.meuGerador = this;
        // unity parte 3 criando um chefao , aula 03 , video 4 balanceando qual....


        quantidadeDeZumbisVivosAtual++;
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    public void DiminuirQuantidadeDeZumbisVivos()
    {
        quantidadeDeZumbisVivosAtual--;
    }

}
