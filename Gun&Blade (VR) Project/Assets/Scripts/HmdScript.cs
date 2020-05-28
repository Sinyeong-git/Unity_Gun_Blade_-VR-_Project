using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HmdScript : MonoBehaviour
{
    public GameObject GameOverManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {


        if (col.gameObject.tag == "Shootable")
        {

            GameObject.Find("GameOverManager").GetComponent<GameOver>().GameOverOn();          
            Debug.Log("게임오버");
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "AvoidCube")
        {
            GameObject.Find("GameOverManager").GetComponent<GameOver>().GameOverOn();
            Debug.Log("게임오버");
            Destroy(col.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}