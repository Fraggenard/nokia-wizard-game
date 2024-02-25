using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehavior : MonoBehaviour
{
    [SerializeField]
    public int movementSpeed = 1;
    
    //how often this entity acts
    [SerializeField]
    public int behaviorInterval = 6;

    public int currentBehaviorInterval = 0;

    [SerializeField]
    public int playerAggroDistance = 35;

    //distance enemy must be at least as close as to player to shoot
    [SerializeField]
    public int shootDistance = 10;

    //minimum number of frames that must elapse between shots
    [SerializeField]
    public int shootCooldown = 5;

    public bool facingLeft = true;

    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBehaviorInterval >= behaviorInterval) {
            performAction();
            currentBehaviorInterval = 0;
        } else {
            currentBehaviorInterval++;
        }

        if (facingLeft) {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        } else {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void performAction() {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= playerAggroDistance) { //aggro action
            float posXDistanceToPlayer = Vector3.Distance(gameObject.transform.position + new Vector3(1, 0, 0), player.transform.position);
            float negXDistanceToPlayer = Vector3.Distance(gameObject.transform.position + new Vector3(-1, 0, 0), player.transform.position);
            float posYDistanceToPlayer = Vector3.Distance(gameObject.transform.position + new Vector3(0, 1, 0), player.transform.position);
            float negYDistanceToPlayer = Vector3.Distance(gameObject.transform.position + new Vector3(0, -1, 0), player.transform.position);
            float minDistance = Math.Min(Math.Min(posXDistanceToPlayer, negXDistanceToPlayer), Math.Min(posYDistanceToPlayer, negYDistanceToPlayer));
            if (minDistance > shootDistance) {
                if (minDistance == posXDistanceToPlayer) {
                    gameObject.transform.position += new Vector3(movementSpeed, 0, 0);
                    facingLeft = false;
                } else if (minDistance == negXDistanceToPlayer) {
                    gameObject.transform.position -= new Vector3(movementSpeed, 0, 0);
                    facingLeft = true;
                } else if (minDistance == posYDistanceToPlayer) {
                    gameObject.transform.position += new Vector3(0, movementSpeed, 0);
                } else if (minDistance == negYDistanceToPlayer) {
                    gameObject.transform.position -= new Vector3(0, movementSpeed, 0);
                }
            } else {
                //check shoot cooldown, then shoot, or move away from player if shoot cooldown is still active
            }
        } else { //idle action
            int random = (int) MathF.Floor(UnityEngine.Random.Range(1.0f, 4.9f));
            if (random == 1) {
                gameObject.transform.position += new Vector3(1, 0, 0);
                facingLeft = false;
            } else if (random == 2) {
                gameObject.transform.position += new Vector3(-1, 0, 0);
                facingLeft = true;
            } else if (random == 3) {
                gameObject.transform.position += new Vector3(0, 1, 0);
            } else {
                gameObject.transform.position += new Vector3(0, -1, 0);
            }
        }
    }

    void shoot() {

    }
}
