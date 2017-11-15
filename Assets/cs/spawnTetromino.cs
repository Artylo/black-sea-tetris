using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTetromino : MonoBehaviour {

    public GameObject[] sets;
    public Vector3 spawnOffset = new Vector3();

    public float globalTickSpeed;

    public void Spawn()
    {
        int i = Random.Range(0, sets.Length);
        Instantiate(sets[i], transform.position+spawnOffset, Quaternion.identity);
    }

	void Start ()
    {
        //sets[0] = GetComponent<GameObject>();
        Spawn();
	}

    void Update()
    {
        //Increase/Decrease Speed
        if (Input.GetKeyDown(KeyCode.Equals)) globalTickSpeed += 0.1f;
        else if (Input.GetKeyDown(KeyCode.Minus) && globalTickSpeed > 0.1) globalTickSpeed -= 0.1f;
    }
}
