using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRegen : MonoBehaviour {

    public GameObject player1, player2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision){
        if(PlayerPrefs.GetString("playerTurn") == player1.name){
            PlayerPrefs.SetInt("p2regen", 1);
        }
        if(PlayerPrefs.GetString("playerTurn") == player2.name){
            PlayerPrefs.SetInt("p1regen", 1);
        }
    }
}
