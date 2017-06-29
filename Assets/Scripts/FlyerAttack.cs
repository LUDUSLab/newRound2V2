using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerAttack : MonoBehaviour {
    private DinoBehaviour   thePlayer;
    private FlyerBehaviour  theFlyer;

    public float            moveSpeed;
    public float            playerRange;
    
    public bool             playerInRange;

    public LayerMask playerLayer;
    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<DinoBehaviour>();
        theFlyer = FindObjectOfType<FlyerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange,playerLayer);
        if (playerInRange)
        {
            theFlyer.transform.position = Vector3.MoveTowards(theFlyer.transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, playerRange);

    }
}
