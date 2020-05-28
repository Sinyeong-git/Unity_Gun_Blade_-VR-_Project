using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
public class GoTitle : MonoBehaviour,IMenuAble
{
    public void OnDamage()
    {
        SteamVR_LoadLevel.Begin("Menu");

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
