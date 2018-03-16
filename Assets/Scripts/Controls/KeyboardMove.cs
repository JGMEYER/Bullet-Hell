using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMove : MonoBehaviour {
    public float speed = 6f;
    public float sprintSpeed = 12f;
    public float gravity = -9.8f;
    public float yRotation = 0;
    private CharacterController _charController;

    void Start() {
        _charController = GetComponent<CharacterController>();
    }
	
	void Update () {
        float curSpeed = speed;
        if (Input.GetButton("Sprint")) {
            curSpeed = sprintSpeed; 
        }

        float deltaX = Input.GetAxisRaw("Horizontal") * curSpeed;
        float deltaZ = Input.GetAxisRaw("Vertical") * curSpeed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, curSpeed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        // rotate player controls around y axis
        movement = Quaternion.AngleAxis(yRotation, Vector3.up) * movement;

        _charController.Move(movement);
    }
}
