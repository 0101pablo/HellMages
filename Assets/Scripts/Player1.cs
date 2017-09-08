using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {

    Rigidbody2D rb2d = new Rigidbody2D();

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Movimentacao();
	}

    void Movimentacao()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * 1f * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.right * 1f * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //transform.Translate(Vector2.up * 100f * Time.deltaTime);
            rb2d.AddForce(transform.up * 200);
        }
    }
}
