using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUiManager : MonoBehaviour
{
    public Text Time_t;

    public string Time_s;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Score_s = (timer * 10).ToString();
        Time_s = "" + timer.ToString("00.00");
        Time_s = Time_s.Replace(".", ":");

        //Score_t.text = "Score\n" + Score_s;
        Time_t.text = "Time\n" + Time_s;
        
    }
}
