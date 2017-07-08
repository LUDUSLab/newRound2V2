using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private Vector3 offset;
    private float y;
    private float x;
    private float limiteX1;
    private float limiteX2;
    private float limiteY;
    private Vector3 posicaoPlayer;

	void Start () {
        offset = transform.position - player.transform.position;
        limiteY = -3.4f;
        limiteX1 = -11.6f;
        limiteX2 = 114.0f;
	}
	
	void LateUpdate () {
        y = limiteY;
        
        if(player.transform.position.x > limiteX1 || player.transform.position.x < limiteX2)
        {
            x = player.transform.position.x;
        }
        if (player.transform.position.x < limiteX1)
        {
            x = limiteX1;
        }
        if(player.transform.position.x > limiteX2)
        {
            x = limiteX2;
        }
        posicaoPlayer = new Vector3(x, y, player.transform.position.z);
        transform.position = posicaoPlayer + offset;
    }
}
