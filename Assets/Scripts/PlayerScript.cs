using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float _movementSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveCharacter();
	}

    private void MoveCharacter(){
        var horizontalMovement = Input.GetAxis("Horizontal") 
                                      * Time.deltaTime * _movementSpeed;
        var verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime
                                    * _movementSpeed;

        transform.position += new Vector3(horizontalMovement, 0, verticalMovement);
    }
}
