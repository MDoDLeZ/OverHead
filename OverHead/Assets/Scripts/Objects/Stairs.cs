using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour {

    public float oldGravityScale;

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {

            oldGravityScale = other.GetComponent<Rigidbody2D>().gravityScale;
            other.GetComponent<Rigidbody2D>().gravityScale = 0f;

        }

    }

    private void OnTriggerExit2D(Collider2D other){

        if (other.tag == "Player") {

            other.GetComponent<Rigidbody2D>().gravityScale = oldGravityScale;

        }

    }

}
