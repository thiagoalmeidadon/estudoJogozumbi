using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    private void Update()
    {
        // billboard vida do chefe fica sempre olhando para camera 
        transform.LookAt(transform.position - Camera.main.transform.forward);



        
    }

}
