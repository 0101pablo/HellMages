using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {


    public GameObject go;
    private Vector3 vec;
    public int controleObjeto;
    private float posicao = 0.2f;

    private void Start()
    {

        PlayerPrefs.SetInt("posVert", 1);
        PlayerPrefs.SetInt("posHori", 1);

    }


    private void FixedUpdate()
    {

        if(controleObjeto == 1) { 
        
            switch (PlayerPrefs.GetInt("posVert"))
            {

                case 1:

                    vec = new Vector3(go.transform.position.x + posicao * Time.deltaTime, go.transform.position.y, go.transform.position.z);
                    go.transform.SetPositionAndRotation(vec, Quaternion.identity);
                    print("indo = " + posicao);
                    if (posicao <= 0.5f)
                    {

                        PlayerPrefs.SetInt("posVert", 2);

                    }
                    else
                    {

                        posicao = posicao - 0.1f;

                    }

                    break;

                case 2:



                    vec = new Vector3(go.transform.position.x - posicao * Time.deltaTime, go.transform.position.y, go.transform.position.z);
                    go.transform.SetPositionAndRotation(vec, Quaternion.identity);
                    print("vindo = " + posicao);
                    if (posicao >= 2f)
                    {
                        PlayerPrefs.SetInt("posVert", 1);

                    }
                    else
                    {

                        posicao = posicao + 0.1f;
                    }
               
                

                    break;

            }
        }else if(controleObjeto == 2)
        {
            switch (PlayerPrefs.GetInt("posHori"))
            {

                case 1:

                    vec = new Vector3(go.transform.position.x, go.transform.position.y + posicao * Time.deltaTime, go.transform.position.z);
                    go.transform.SetPositionAndRotation(vec, Quaternion.identity);
                    print("indo = " + posicao);
                    if (posicao <= 0.5f)
                    {

                        PlayerPrefs.SetInt("posHori", 2);

                    }
                    else
                    {

                        posicao = posicao - 0.1f;

                    }

                    break;

                case 2:



                    vec = new Vector3(go.transform.position.x, go.transform.position.y - posicao * Time.deltaTime, go.transform.position.z);
                    go.transform.SetPositionAndRotation(vec, Quaternion.identity);
                    print("vindo = " + posicao);
                    if (posicao >= 2f)
                    {
                        PlayerPrefs.SetInt("posHori", 1);

                    }
                    else
                    {

                        posicao = posicao + 0.1f;
                    }



                    break;

            }



        }





    }
        

}