using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class GameOver : MonoBehaviour
{
    public ScoreLogic SL;

    // Start is called before the first frame update
    void Start()
    {
       
    
    }


    public void GameOverOn()
    {
        //transform.Find("GameOverUI").gameObject.SetActive(true);
         GameUiManager gum = GameObject.Find("ScoreUI").GetComponent<GameUiManager>();
         SL = GameObject.Find("Score").GetComponent<ScoreLogic>();

         SL.score = gum.Time_s;

         SteamVR_LoadLevel.Begin("GameOver");
 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
