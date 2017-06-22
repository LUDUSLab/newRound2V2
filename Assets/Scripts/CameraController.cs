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
    private float limiteY1;
    private float limiteY2;
    private Vector3 posicaoPlayer;
    
    void Start () {
        offset = transform.position - player.transform.position;
        limiteY1 = -1.8f;
        limiteY2 = -3.6f;
        limiteX1 = -11.56298f;
        limiteX2 = 48.59263f;
	}
	
	void LateUpdate () {
        if (player.transform.position.y >= limiteY1)
        {
            y = limiteY1;
        }
        if (player.transform.position.y <= limiteY2)
        {
            y = limiteY2;
        }
        if (player.transform.position.y < limiteY1 && player.transform.position.y > limiteY2)
        {
            y = player.transform.position.y;
        }
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
