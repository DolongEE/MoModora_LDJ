using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class csInitStart : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync("scTitle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
