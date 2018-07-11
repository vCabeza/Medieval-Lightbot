using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {
    public int altura = 1;
    public bool isEnemyTile;
    public GameObject enemy;

    Vector3 spawnPos;
    Vector3 scale;

    // Use this for initialization
    void Start () {
        spawnPos = transform.position;
        spawnPos.y = 1 + altura;

        scale = transform.localScale;
        scale.y = altura;
        transform.localScale = scale;

        if(isEnemyTile) {
            GameObject myRoadInstance =
            Instantiate(enemy,
            spawnPos,
            Quaternion.identity) as GameObject;
        }
    }
}
