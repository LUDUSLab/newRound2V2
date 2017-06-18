using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tatu_IA : MonoBehaviour {
    private Rigidbody2D enemy;
    private Transform enemyPosition;
    public Transform Player;
    public Transform tatuHome;
    private bool viradoParaDireita;
    // Use this for initialization
    void Start () {
        enemy = GetComponent<Rigidbody2D>();
        enemyPosition = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
	}
    
}
