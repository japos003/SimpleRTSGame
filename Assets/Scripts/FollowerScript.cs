using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerScript : MonoBehaviour {

    public float _followerMoveSpeed = 5;
    public float _playerCallRadius = 1.0f;
    public bool _isClicked = false;

    private Vector3 _playerLocation;
    private bool _calledByPlayer;

    private bool _calledToLocation;
    private Vector3 _clickedCalledLocation;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        if(_calledByPlayer){
            MoveTowardsPlayer();
        }
        else if(_calledToLocation){
            transform.position = Vector3.MoveTowards(transform.position, _clickedCalledLocation, _followerMoveSpeed);
            if(Vector3.Distance(transform.position, _clickedCalledLocation) < 2.0f){
                _calledToLocation = false;
            }
        }
    }

    public void GoToPlayerLocation(Vector3 playerLocation)
    {
        _calledByPlayer = true;
        _playerLocation = playerLocation;
    }

    public void GoToClickedLocation(Vector3 clickedLocation){
        _clickedCalledLocation = clickedLocation;

        _calledToLocation = true;
        _isClicked = false;
        Debug.Log(string.Format("go to {0}", clickedLocation));
    }

    private void MoveTowardsPlayer(){
        var distanceFromPlayer = Vector3.Distance(this.transform.position, _playerLocation);

        if(distanceFromPlayer < _playerCallRadius){
            transform.position =
                    Vector3.MoveTowards(transform.position,
                        _playerLocation, _followerMoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _playerLocation) < 0.5f)
            {
                _calledByPlayer = false;
            }
        }

    }
}
