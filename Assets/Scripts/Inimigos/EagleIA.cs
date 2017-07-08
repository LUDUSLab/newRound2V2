using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleIA : MonoBehaviour
{

    public Transform eagleHome;
    private Transform player;
    private Vector3 positionPlayerLost;
    private Vector3 positionPlayerFind;
    private Transform eagle;

    public float speed;
    private float startTime;
    private float journeyLenght;

    public bool lostPlayer = false;
    public bool canFly = false;

    void Start()
    {
        eagle = GetComponent<Transform>();
        eagleHome = eagle.transform.parent;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        positionPlayerLost = eagleHome.position;
        BackToHome();
    }

    // Update is called once per frame
    void Update()
    {
        if (canFly)
        {
            if (lostPlayer)
            {
                float dist = (Time.time - startTime) * speed;
                float journey = dist / journeyLenght;
                if (eagle.position == eagleHome.position)
                {
                    canFly = false;
                }
                eagle.position = Vector3.Lerp(positionPlayerLost, eagleHome.position, journey);
            }
            else
            {
                eagle.position = Vector3.Lerp(eagle.position, player.position, 0.03f);
            }
        }
    }
    public void BackToHome()
    {
        startTime = Time.time;
        positionPlayerLost = eagle.position;
        journeyLenght = Vector3.Distance(positionPlayerLost, eagleHome.position);
    }
}