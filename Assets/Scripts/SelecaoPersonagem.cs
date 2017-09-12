using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelecaoPersonagem : MonoBehaviour {

    private List<GameObject> pers;
    private int selecao = 0;
    private int p1 = 0;

    // Use this for initialization
	void Start () {

        pers = new List<GameObject>();
        foreach (Transform t in transform) {
            pers.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
	}

    public void Select(int index) {

        if (p1 == 0) {

            selecao = index;
            pers[selecao].SetActive(true);
            p1 = 1;

        }
        else
        {
            if (selecao == index)
            {
                return;
            }
            else
            {
                selecao = index + 4;
                pers[selecao].SetActive(true);
                SceneManager.LoadScene(2);
            }
        }

    }

	// Update is called once per frame
	void Update () {
		
	}
}
