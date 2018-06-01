using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

	[SerializeField]
	Transform[] points;

	[SerializeField]
	private float speed = 5f;

	[SerializeField]
	private float health = 100f;

	private int waypointIndex = 0;
	private int amount = 0;

	void Avake()
	{
	}

	// Use this for initialization
	void Start () {
		amount = LevelManager.Goals.Length;
		points = new Transform[amount];
		for (int i = 0; i < amount; i++)
		{
			points[i] = LevelManager.Goals[i];
			//Debug.Log (points[i].position);
		}

		transform.position = points [waypointIndex].transform.position;
	}

	
	// Update is called once per frame
	void Update () {
		Move();
	}

	void Move()
	{
		transform.position = Vector2.MoveTowards (transform.position,
			points [waypointIndex].transform.position,
			speed * Time.deltaTime);
		if(transform.position == points[waypointIndex].transform.position)
		{
			waypointIndex++;
		}

		if(waypointIndex == points.Length)
		{
			Destroy(gameObject);
			return;
		}
	}
}
