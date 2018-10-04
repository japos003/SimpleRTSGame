using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerScript : MonoBehaviour {

    public float _followerMoveSpeed = 5;

    private Vector3 _playerLocation;
    private bool _calledByPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(_calledByPlayer){
            MoveTowardsPlayer();
        }
	}

    private void MoveTowardsPlayer(){
        transform.position =
            Vector3.MoveTowards(transform.position,
                                _playerLocation, _followerMoveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _playerLocation) < 0.5f){
            _calledByPlayer = false;
        }
    }


    public void GoToPlayerLocation(Vector3 playerLocation){
        _calledByPlayer = true;
        _playerLocation = playerLocation;
    }
}
