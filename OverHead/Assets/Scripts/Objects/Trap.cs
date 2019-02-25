﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour {

    void Start(){
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            
            int temp = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(temp, LoadSceneMode.Single);

        }

    }
}