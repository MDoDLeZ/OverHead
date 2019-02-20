using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public Transform target;

	void Start () {
		
	}
	
	void Update () {
        Vector3 targ = target.position;
        targ.z = -10f;
        transform.position = Vector3.Lerp(transform.position, targ, 0.15f);
	}
}
