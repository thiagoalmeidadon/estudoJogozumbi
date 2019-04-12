using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ControlaInterface : MonoBehaviour {

    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameover;
    public Text TextoTempoSobrevivencia;
    public Text TextoTempoMaximoSobrevivencia;
    private float tempoPontuacaoSalva;
    private int quantidadeDeZumbisMortos;
    public Text TextoDaQuantidadeDeZumbisMortos;
    public Text TextoChefeAparece;

	// Use this for initialization
	void Start () {
        scriptControlaJogador = GameObject.FindWithTag(Tags.Jogador).
        GetComponent<ControlaJogador>();

        //scriptControlaJogador = GetComponent<ControlaJogador>();


        Time.timeScale = 1;
        tempoPontuacaoSalva = PlayerPrefs.GetFloat("PontuacaoMaxima");
     

    }

    // Update is called once per frame
    void Update () {
        
	}

    public void AtualizarQuantidadeDeZumbisMortos()
    { 
        quantidadeDeZumbisMortos++;
        TextoDaQuantidadeDeZumbisMortos.text = string.Format("x {0}", quantidadeDeZumbisMortos) ;

    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }


    public void GameOver()
    {
        PainelDeGameover.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);

        TextoTempoSobrevivencia.text = "Você sobreviveu por " + minutos + "minutos e " + segundos + " segundos" ;
        AjustarPontuacaoMaxima(minutos, segundos);

    }

    void AjustarPontuacaoMaxima(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalva)
        {
            tempoPontuacaoSalva = Time.timeSinceLevelLoad;
            TextoTempoMaximoSobrevivencia.text = string.Format("O recorde de tempo foi  {0} min e {1} seg ", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalva);
        }
        if (TextoTempoMaximoSobrevivencia.text == "")
        {
            min = (int)tempoPontuacaoSalva / 60;
            seg = (int)tempoPontuacaoSalva % 60;
            TextoTempoMaximoSobrevivencia.text = string.Format("O recorde de tempo foi {0} min e {1} seg ", min, seg);

        }


    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("game");
    }

    public void aparecerTextoChefeCriado()
    {
        StartCoroutine(DesaparecerTexto(2, TextoChefeAparece));

    }

    IEnumerator DesaparecerTexto (float tempoDeSumico, Text textoParaSumir)
    {
        textoParaSumir.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        float contador = 0;

        while(textoParaSumir.color.a > 0)
        {
            contador += Time.deltaTime / tempoDeSumico;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;

            if(textoParaSumir.color.a <= 0)
            {
                textoParaSumir.gameObject.SetActive(false);
            }

            yield return null;
        }
        


    }
}
