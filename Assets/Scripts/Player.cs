using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject projetilBPrefab;
    public GameObject projetilCPrefab;
    public GameObject projetilCMiniPrefab;

    public GameObject p1s1,p1s2,p1s3,p2s1,p2s2,p2s3; //shields

    int vida = 3;
    public Text textoDaVitoria;

    public GameObject p1Power, p1Hold, p2Power, p2Hold;
    public Sprite prjA, prjB, prjC; //sprites dos projeteis
    SpriteRenderer srp1p, srp1h, srp2p, srp2h;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();

        //condições especiais no início do jogo:
        PlayerPrefs.SetString("playerTurn", player1.name);
        if (gameObject.name == player2.name) resetAux = true;

        srp1p = p1Power.GetComponent<SpriteRenderer>();
        srp1h = p1Hold.GetComponent<SpriteRenderer>();
        srp2p = p2Power.GetComponent<SpriteRenderer>();
        srp2h = p2Hold.GetComponent<SpriteRenderer>();

        srp1p.sprite = PoderRdm();
        srp1h.sprite = PoderRdm();
        srp2p.sprite = PoderRdm();
        srp2h.sprite = PoderRdm();
    }

    Sprite PoderRdm()
    {
        int rdm = (Mathf.RoundToInt(Random.Range(1.0f, 3.0f)));
        switch (rdm)
        {
            case 1: return prjA;
            case 2: return prjB;
            case 3: return prjC;
            default: return prjA;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(gameObject.name == PlayerPrefs.GetString("playerTurn")){
            if (!barrasIniciadas) Movimentacao();
            Barras();
        }

        if ((player1.activeSelf == false || player2.activeSelf == false) && Input.GetKey(KeyCode.Return)){
            SceneManager.LoadScene(0);
        }

        if(PlayerPrefs.GetInt("p1regen") == 1 && gameObject.name == player1.name || PlayerPrefs.GetInt("p2regen") == 1 && gameObject.name == player2.name) {
            if(vida < 3){
                vida++;
                P1StatusVida();
                P2StatusVida();
            }
            PlayerPrefs.SetInt("p1regen", 0);
            PlayerPrefs.SetInt("p2regen", 0);
        }
	}

    void Movimentacao(){
        if (Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(Vector2.right * 2f * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(Vector2.right * 2f * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    private void OnCollisionStay2D(Collision2D collision){
        if (Input.GetKeyDown(KeyCode.UpArrow) && gameObject.name == PlayerPrefs.GetString("playerTurn")){
            //transform.Translate(Vector2.up * 100f * Time.deltaTime);
            rb2d.AddForce(transform.up * 300);
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

    GameObject ChecaProjetil(){
        if (PlayerPrefs.GetString("playerTurn") == player1.name){
            if (srp1p.sprite.name == prjA.name) return projetilAPrefab;
            else if (srp1p.sprite.name == prjB.name) return projetilBPrefab;
            else if (srp1p.sprite.name == prjC.name) return projetilCPrefab;
            else return directionBar; //se bugar vai ser engraçado :v
        }
        else{
            if (srp2p.sprite.name == prjA.name) return projetilAPrefab;
            else if (srp2p.sprite.name == prjB.name) return projetilBPrefab;
            else if (srp2p.sprite.name == prjC.name) return projetilCPrefab;
            else return directionBar; //se bugar vai ser engraçado² :v
        }
    }

    void HoldParaPower()
    {
        if(PlayerPrefs.GetString("playerTurn") == player1.name){
            srp1p.sprite = srp1h.sprite;
            srp1h.sprite = PoderRdm();
        }
        else if (PlayerPrefs.GetString("playerTurn") == player2.name){
            srp2p.sprite = srp2h.sprite;
            srp2h.sprite = PoderRdm();
        }
    }

    void Tiro(float powerBarValue, float rotacao){
        GameObject projetil = ChecaProjetil();
        GameObject projetilClone = Instantiate(projetil, saidaProjetil.transform.position, Quaternion.Euler(0,0,rotacao)) as GameObject;
        Rigidbody2D rbProjA = projetilClone.GetComponent<Rigidbody2D>();
        rbProjA.velocity = new Vector2((90 - rotacao)/90, (90 - Mathf.Abs(rotacao-90) ) / 90) * powerBarValue * 20;
        HoldParaPower();
        TrocaPlayers();
    }

   

    void TrocaPlayers(){
        if (PlayerPrefs.GetString("playerTurn") == player1.name) PlayerPrefs.SetString("playerTurn", player2.name);
        else if (PlayerPrefs.GetString("playerTurn") == player2.name) PlayerPrefs.SetString("playerTurn", player1.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == projetilAPrefab.layer) {
            if(collision.otherCollider.name == player1.name){
                vida--;
                P1StatusVida();
            }
            else if(collision.otherCollider.name == player2.name)
            {
                vida--;
                P2StatusVida();
            }
        }
    }

    void P1StatusVida(){
        switch (vida){
            case 3: p1s1.SetActive(true); p1s2.SetActive(true); p1s3.SetActive(true); break;
            case 2: p1s1.SetActive(true); p1s2.SetActive(true); p1s3.SetActive(false); break;
            case 1: p1s1.SetActive(true); p1s2.SetActive(false); p1s3.SetActive(false); break;
            case 0: p1s1.SetActive(false); p1s2.SetActive(false); p1s3.SetActive(false); break;
            case -1:  player1.SetActive(false); textoDaVitoria.gameObject.SetActive(true); textoDaVitoria.text = "Jogador 2 Venceu!"; break;
        }
    }

    void P2StatusVida(){
        switch (vida){
            case 3: p2s1.SetActive(true); p2s2.SetActive(true); p2s3.SetActive(true); break;
            case 2: p2s1.SetActive(true); p2s2.SetActive(true); p2s3.SetActive(false); break;
            case 1: p2s1.SetActive(true); p2s2.SetActive(false); p2s3.SetActive(false); break;
            case 0: p2s1.SetActive(false); p2s2.SetActive(false); p2s3.SetActive(false); break;
            case -1: player2.SetActive(false); textoDaVitoria.gameObject.SetActive(true); textoDaVitoria.text = "Jogador 1 Venceu!"; break;
        }
    }
}
