using System;
using UnityEngine;

public class SnakeBehavior : MonoBehaviour
{
    [SerializeField]
    public int movementSpeed = 1;
    
    //how often this entity acts
    [SerializeField]
    public int behaviorInterval = 6;

    public int currentBehaviorInterval = 0;
    
    [SerializeField]
    public int animationInterval = 6;

    public int currentAnimationInterval = 0;

    [SerializeField]
    public int playerAggroDistance = 35;

    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAnimationInterval >= animationInterval) {
            gameObject.transform.localScale = new Vector3(1, -gameObject.transform.localScale.y, 1);
            currentAnimationInterval = 0;
        } else {
            currentAnimationInterval++;
        }

        if (currentBehaviorInterval >= behaviorInterval) {
            performAction();
            currentBehaviorInterval = 0;
        } else {
            currentBehaviorInterval++;
        }
    }

    void performAction() {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= playerAggroDistance) { //aggro action
            float posXDistanceToPlayer = Vector3.Distance(gameObject.transform.position + new Vector3(1, 0, 0), player.transform.position);
            float negXDistanceToPlayer = Vector3.Distance(gameObject.transform.position + new Vector3(-1, 0, 0), player.transform.position);
            float posYDistanceToPlayer = Vector3.Distance(gameObject.transform.position + new Vector3(0, 1, 0), player.transform.position);
            float negYDistanceToPlayer = Vector3.Distance(gameObject.transform.position + new Vector3(0, -1, 0), player.transform.position);
            float minDistance = Math.Min(Math.Min(posXDistanceToPlayer, negXDistanceToPlayer), Math.Min(posYDistanceToPlayer, negYDistanceToPlayer));
            if (minDistance == posXDistanceToPlayer) {
                gameObject.transform.position += new Vector3(movementSpeed, 0, 0);
            } else if (minDistance == negXDistanceToPlayer) {
                gameObject.transform.position -= new Vector3(movementSpeed, 0, 0);
            } else if (minDistance == posYDistanceToPlayer) {
                gameObject.transform.position += new Vector3(0, movementSpeed, 0);
            } else if (minDistance == negYDistanceToPlayer) {
                gameObject.transform.position -= new Vector3(0, movementSpeed, 0);
            }
        } else { //idle action
            int random = (int) MathF.Floor(UnityEngine.Random.Range(1.0f, 4.9f));
            if (random == 1) {
                gameObject.transform.position += new Vector3(1, 0, 0);
            } else if (random == 2) {
                gameObject.transform.position += new Vector3(-1, 0, 0);
            } else if (random == 3) {
                gameObject.transform.position += new Vector3(0, 1, 0);
            } else {
                gameObject.transform.position += new Vector3(0, -1, 0);
            }
        }
    }
}
