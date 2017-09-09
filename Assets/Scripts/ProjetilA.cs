using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilA : MonoBehaviour {

    int toque = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision){
        if (toque >= 4) Destroy(gameObject);
        else toque++;
    }
}
