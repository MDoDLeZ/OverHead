using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    void Start(){
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" && other.GetComponent<Head>().haveHead){

            int temp = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(temp + 1, LoadSceneMode.Single);

        }

    }
}
