using UnityEngine;

public class PC_Controller : MonoBehaviour {

    private Movement movement;
    private Head head;
    private Hook hook;

    private void Start() {

        movement = GetComponent<Movement>();
        head = GetComponent<Head>();
        hook = GetComponent<Hook>();

    }

    private void Update() {

        if (Input.GetKey(KeyCode.LeftShift)){
            movement.Move(Input.GetAxis("Horizontal"), true);
        }
        else {
            movement.Move(Input.GetAxis("Horizontal"), false);
        }
        movement.Climb(Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Jump")) {
            movement.Jump();
            GetComponent<LineRenderer>().enabled = false;
            GetComponent<TargetJoint2D>().enabled = false;
            GetComponent<Hook>().isPressed = false;
        }

        if (Input.GetKey(KeyCode.E)) {
            head.Take();
        }
        if (Input.GetKey(KeyCode.Q)) {
            head.Drop();
        }

        if (Input.GetMouseButtonDown(1)){
            hook.Hooking();
        }

    }

}
