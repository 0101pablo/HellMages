using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilAtributos : MonoBehaviour {

    public GameObject pjA, pjB, pjC, pjCm;
    public AudioClip som;
    AudioSource origemSom;

    private void Awake(){
        origemSom = GetComponent<AudioSource>();
    }

    int toque = 1;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 15);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnCollisionEnter2D(Collision2D collision){
        origemSom.PlayOneShot(som);
        if (gameObject.name == pjA.name){
            if (toque >= 5) Destroy(gameObject);
            else toque++;
        }

        if (gameObject.name == pjB.name){
            Destroy(gameObject, 5);
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = rb.velocity * 0.75f;
        }

        if (gameObject.name == pjC.name){
            GameObject projetilClone1 = Instantiate(pjCm, pjC.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject projetilClone2 = Instantiate(pjCm, pjC.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject projetilClone3 = Instantiate(pjCm, pjC.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            Rigidbody2D rb1 = projetilClone1.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = projetilClone2.GetComponent<Rigidbody2D>();
            Rigidbody2D rb3 = projetilClone3.GetComponent<Rigidbody2D>();
            rb1.velocity = new Vector2(-1,-1);
            rb2.velocity = new Vector2(0, -1);
            rb3.velocity = new Vector2(1, -1);
            Destroy(gameObject);
        }

        if (gameObject.name == pjCm.name){
            if (toque > 3) Destroy(gameObject);
            else toque++;
        }
    }
}
