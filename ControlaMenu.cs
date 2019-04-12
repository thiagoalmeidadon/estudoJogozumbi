using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlaMenu : MonoBehaviour {

    public GameObject BotaoSair;

    private void Start()
    {

        #if UNITY_STANDALONE || UNITY_EDITOR
            BotaoSair.SetActive(true);

        #endif
    }

    public void JogarJogo()
    {
        StartCoroutine(MudarCena("game"));
    }


    IEnumerator MudarCena(string name)
    {
        SceneManager.LoadScene(name);
        yield return new WaitForSecondsRealtime(0.8f);
    }

    public void SariDoJogo()
    {
            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
            #endif

        Application.Quit();
    }

}
