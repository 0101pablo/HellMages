using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public GameObject go;
    private Vector3 vec;
    public int controleObjeto;
    private float posicao = 0f;
    public float distanciaLR = 16f;
    public float velocidadeLR = 4f;
    public float distanciaUD = 4f;

    private void Start() {
        PlayerPrefs.SetInt("posVert", 1);
        PlayerPrefs.SetInt("posHori", 1);
        //var goParticula = go.GetComponentInChildren<ParticleSystem>().emission;
        //goParticula.enabled = false;
        //go.GetComponentInChildren<ParticleSystem>().Stop();
    }

    /*public void OnCollisionEnter2D(Collision2D collision){
        var goParticula = go.GetComponentInChildren<ParticleSystem>().emission;
        goParticula.enabled = true;
        if (go.GetComponentInChildren<ParticleSystem>().isPlaying){
            go.GetComponentInChildren<ParticleSystem>().Play();
            go.GetComponentInChildren<ParticleSystem>().Stop();
        }
        else if (!go.GetComponentInChildren<ParticleSystem>().isPlaying){
            go.GetComponentInChildren<ParticleSystem>().Play();
            go.GetComponentInChildren<ParticleSystem>().Stop();
        }
    }*/

    private void Update() {
        if(controleObjeto == 1) { 
            switch (PlayerPrefs.GetInt("posVert")) {
                case 1:
                    vec = new Vector3(go.transform.position.x + /*posicao*/ velocidadeLR * Time.deltaTime, go.transform.position.y, go.transform.position.z);
                    go.transform.SetPositionAndRotation(vec, Quaternion.identity);
                    //print("indo = " + posicao);
                    if (posicao <= 0f) PlayerPrefs.SetInt("posVert", 2);
                    else posicao = posicao - 0.05f;
                    break;
                case 2:
                    vec = new Vector3(go.transform.position.x - /*posicao*/ velocidadeLR * Time.deltaTime, go.transform.position.y, go.transform.position.z);
                    go.transform.SetPositionAndRotation(vec, Quaternion.identity);
                    //print("vindo = " + posicao);
                    if (posicao >= distanciaLR) PlayerPrefs.SetInt("posVert", 1);
                    else posicao = posicao + 0.05f;
                    break;
            }
        }
        else if(controleObjeto == 2) {
            switch (PlayerPrefs.GetInt("posHori")) {
                case 1:
                    vec = new Vector3(go.transform.position.x, go.transform.position.y + posicao * Time.deltaTime, go.transform.position.z);
                    go.transform.SetPositionAndRotation(vec, Quaternion.identity);
                    //print("indo = " + posicao);
                    if (posicao <= 0f) PlayerPrefs.SetInt("posHori", 2);
                    else posicao = posicao - 0.05f;
                    break;
                case 2:
                    vec = new Vector3(go.transform.position.x, go.transform.position.y - posicao * Time.deltaTime, go.transform.position.z);
                    go.transform.SetPositionAndRotation(vec, Quaternion.identity);
                    //print("vindo = " + posicao);
                    if (posicao >= distanciaUD) PlayerPrefs.SetInt("posHori", 1);
                    else posicao = posicao + 0.05f;
                    break;
            }
        }
    }
}