using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public float score_f;
    public Text Text_s;
    // Start is called before the first frame update
    void Start()
    {
        ScoreLogic SL = GameObject.Find("Score").GetComponent<ScoreLogic>();
        Debug.Log(SL.score);


        Text_s.text = "Game Over\n" + SL.score;

        SL.score = SL.score.Replace(":", ".");
        score_f = float.Parse(SL.score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
