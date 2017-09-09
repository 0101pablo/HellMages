using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    Rigidbody2D rb2d = new Rigidbody2D();

    bool barrasIniciadas = false;
    public Scrollbar powerBar;
    bool powerBarSwitch = false;
    bool barMove = true; //serve bara ambas as barras
    public GameObject directionBar;
    bool directionBarSwitch = false;
    bool resetAux =false;

    public GameObject saidaProjetil;

    public GameObject projetilAPrefab;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!barrasIniciadas) Movimentacao();
        Barras();
	}

    void Movimentacao(){
        if (Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(Vector2.right * 1f * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(Vector2.right * 1f * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void Barras(){
        if (Input.GetKeyUp(KeyCode.Space) && resetAux){
            resetAux = false;
            return;
        }
        //BARRA 1
        if (Input.GetKeyUp(KeyCode.Space) && !barrasIniciadas){
            powerBarSwitch = true;
            barrasIniciadas = true;
        }
        if (powerBarSwitch && !directionBarSwitch){
            if(powerBar.value <= 0) barMove = true;
            if (powerBar.value >= 1) barMove = false;
            if (barMove) powerBar.value = powerBar.value + 0.05f;
            else powerBar.value = powerBar.value - 0.05f;
            if (Input.GetKeyDown(KeyCode.Space)) powerBarSwitch = false;
        }
        //BARRA 2
        var rotacao = directionBar.transform.rotation.eulerAngles;
        if (Input.GetKeyUp(KeyCode.Space) && barrasIniciadas && !powerBarSwitch) directionBarSwitch = true;
        if (directionBarSwitch){
            if (rotacao.z <=10f) barMove = true;
            if (rotacao.z >= 170f) barMove = false;
            if (barMove) rotacao.z = rotacao.z +2;
            else if (!barMove) rotacao.z = rotacao.z -2;
            directionBar.transform.rotation = Quaternion.Euler(rotacao);
            if (Input.GetKeyDown(KeyCode.Space)){
                directionBarSwitch = false;
                Tiro();
                barrasIniciadas = false;
                ResetBarras();
            }        
        }
    }

    private void OnCollisionStay2D(Collision2D collision){
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            //transform.Translate(Vector2.up * 100f * Time.deltaTime);
            rb2d.AddForce(transform.up * 200);
        }
    }

    void ResetBarras(){
        powerBar.value = 0;
        var rotacao = directionBar.transform.rotation.eulerAngles;
        rotacao.z = 0f;
        directionBar.transform.rotation = Quaternion.Euler(rotacao);
        resetAux = true;
    }

    void Tiro(){
        GameObject projetilAClone = Instantiate(projetilAPrefab) as GameObject;
        projetilAClone.transform.position = saidaProjetil.transform.position;
        Rigidbody2D rbProjA = projetilAClone.GetComponent<Rigidbody2D>();
        //?????
    }
}
