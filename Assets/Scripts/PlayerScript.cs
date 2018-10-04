using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float _movementSpeed;
    public GameObject _cameraObject;
    public float _cameraMovementSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveCharacter();
        CallFollowers();
        MoveCameraTowardsPlayer();
	}

    private void MoveCameraTowardsPlayer(){
        var mainCamera = _cameraObject.GetComponent<Camera>();

        if(mainCamera != null){
            var cameraPosition = _cameraObject.transform.position;
            mainCamera.transform.position =
                Vector3.Lerp(cameraPosition, transform.position + new Vector3(0, 2.5f, -5f), _cameraMovementSpeed * Time.deltaTime);
        }

    }

    private void MoveCharacter(){
        var horizontalMovement = Input.GetAxis("Horizontal") 
                                      * Time.deltaTime * _movementSpeed;
        var verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime
                                    * _movementSpeed;

        transform.position += new Vector3(horizontalMovement, 0, verticalMovement);
    }

    private void CallFollowers(){
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("calling followers...");
            var worldObject = transform.parent;
            worldObject.BroadcastMessage("GoToPlayerLocation", transform.position, SendMessageOptions.DontRequireReceiver);
        }
    }
}
