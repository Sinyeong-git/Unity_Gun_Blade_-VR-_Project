using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
public class RestartGame : MonoBehaviour,IMenuAble
{
    public void OnDamage()
    {
        //Time.timeScale = 1;
        SteamVR_LoadLevel.Begin("Game");

        if (GameObject.Find("Score"))
            Destroy(GameObject.Find("Score"));
    }

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
