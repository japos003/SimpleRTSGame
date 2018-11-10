using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        CallFollowerByMouseClick();
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

    private void CallFollowerByMouseClick(){
        if(Input.GetMouseButton(0)){
            if(!SelectFollower()){
                CommandClickedFollowerToClickedLocation();
            }
        }
    }

    private void CommandClickedFollowerToClickedLocation(){
        var mousePosition = Input.mousePosition;
        var mainCamera = _cameraObject.GetComponent<Camera>();
        var worldPosition = mainCamera.ScreenPointToRay(mousePosition);

        RaycastHit objectHit;
        if (Physics.Raycast(worldPosition, out objectHit))
        {
            var world = transform.parent;
            var listOfFollowers = world.GetComponentsInChildren<FollowerScript>();
            var clickedFollower = listOfFollowers.Where(fol => fol._isClicked).FirstOrDefault();

            if(clickedFollower != null){
                clickedFollower.GoToClickedLocation(objectHit.point);
            }
        }
    }

    private FollowerScript GetFollowerFromMouseClick(){
        var mousePosition = Input.mousePosition;
        var mainCamera = _cameraObject.GetComponent<Camera>();
        var worldPosition = mainCamera.ScreenPointToRay(mousePosition);

        RaycastHit objectHit;
        if (Physics.Raycast(worldPosition, out objectHit))
        {
            var follower = objectHit.collider.gameObject.GetComponent<FollowerScript>();
            if(follower != null){
                Debug.Log("Name: " + follower.name);
            } else{
                Debug.Log("Is empty");
            }

            return follower;
        }

        return null;
    }

    private bool SelectFollower(){
        var follower = GetFollowerFromMouseClick();
        if(follower == null){
            return false;
        }

        var material = follower.gameObject.GetComponent<Renderer>().material;

        FollowerScript[] followers;
        if(ContainsClickedFollower(out followers)){
            var clickedFollower = followers
                                    .Where(fol => fol._isClicked).FirstOrDefault();

            var clickedFollowerMaterial = clickedFollower
                .gameObject.GetComponent<Renderer>().material;

            Debug.Log("Must turn to white");
            Debug.Log("Previously clicked Follower: " + clickedFollower.name);
            clickedFollowerMaterial.color = Color.white;
            clickedFollower._isClicked = false;
        }

        material.color = Color.yellow;
        follower._isClicked = true;

        return true;
    }

    private bool ContainsClickedFollower(out FollowerScript[] listOfFollowers){
        var world = gameObject.transform.parent;
        var followers = world.GetComponentsInChildren<FollowerScript>();
        listOfFollowers = followers;

        return followers.Any(follower => follower._isClicked);
    }
}
