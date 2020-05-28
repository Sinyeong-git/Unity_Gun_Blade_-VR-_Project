using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Gun")
        {       
            GunScript gun = col.GetComponent<GunScript>();
            gun.TryReload();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
