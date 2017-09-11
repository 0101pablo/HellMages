using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

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

        //condições especiais no início do jogo:
        PlayerPrefs.SetString("playerTurn", player1.name);
        if (gameObject.name == player2.name) resetAux = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(gameObject.name == PlayerPrefs.GetString("playerTurn")){
            if (!barrasIniciadas) Movimentacao();
            Barras();
        }
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

    private void OnCollisionStay2D(Collision2D collision){
        if (Input.GetKeyDown(KeyCode.UpArrow) && gameObject.name == PlayerPrefs.GetString("playerTurn")){
            //transform.Translate(Vector2.up * 100f * Time.deltaTime);
            rb2d.AddForce(transform.up * 200);
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
            if (rotacao.z <= 10f) barMove = true;
            if (rotacao.z >= 170f) barMove = false;
            if (barMove) rotacao.z = rotacao.z + 2f;
            else if (!barMove) rotacao.z = rotacao.z - 2f;
            directionBar.transform.rotation = Quaternion.Euler(rotacao);
            if (Input.GetKeyDown(KeyCode.Space)){
                directionBarSwitch = false;
                Tiro(powerBar.value, rotacao.z);
                barrasIniciadas = false;
                ResetBarras();
            }        
        }
    }

    void ResetBarras(){
        powerBar.value = 0;
        var rotacao = directionBar.transform.rotation.eulerAngles;
        if (gameObject.name == player1.name) rotacao.z = 180f;
        else rotacao.z = 0;
        directionBar.transform.rotation = Quaternion.Euler(rotacao);
        resetAux = true;
    }

    void Tiro(float powerBarValue, float rotacao){
        GameObject projetilAClone = Instantiate(projetilAPrefab, saidaProjetil.transform.position, Quaternion.Euler(0,0,rotacao)) as GameObject;
        Rigidbody2D rbProjA = projetilAClone.GetComponent<Rigidbody2D>();
        rbProjA.velocity = new Vector2((90 - rotacao)/90, (90 - Mathf.Abs(rotacao-90) ) / 90) * powerBarValue * 20;
        TrocaPlayers();
    }

    void TrocaPlayers(){
        if (PlayerPrefs.GetString("playerTurn") == player1.name) PlayerPrefs.SetString("playerTurn", player2.name);
        else if (PlayerPrefs.GetString("playerTurn") == player2.name) PlayerPrefs.SetString("playerTurn", player1.name);
    }
}
