using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

	public float Velocidade = 20;
    private int tamanhoDoDano = 1;

	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Rigidbody> ().MovePosition 
		(GetComponent<Rigidbody>().position + transform.forward *
			Velocidade * Time.deltaTime);
	}

	void OnTriggerEnter(Collider objetoDeColisao)
	{
        Quaternion rotacaoOpostaABala = Quaternion.LookRotation(-transform.forward);

		switch(objetoDeColisao.tag)
        {

            case Tags.Inimigo :
             ControlaInimigo inimigo = objetoDeColisao.GetComponent<ControlaInimigo>();
                inimigo.TomarDano(tamanhoDoDano);
                inimigo.ParticulaSangue(transform.position, rotacaoOpostaABala);
            break;

            case Tags.Chefe:
             objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(tamanhoDoDano);
            break;

        }

			

		// gameobject com g minusculo quer dizer o proprio objeto que tem o script
		Destroy (gameObject);

	}

}
