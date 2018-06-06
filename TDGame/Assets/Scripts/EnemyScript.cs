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
    
    public bool Alive
    {
        get { return health > 0; }
    }

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            this.health = value;
        }
    }


	private int waypointIndex = 0;
	private int amount = 0;

    public bool IsActive { get; set; }

	void Avake()
	{
	}

	// Use this for initialization
	void Start () {
        IsActive = true;
		amount = LevelManager.Goals.Length;
		points = new Transform[amount];
		for (int i = 0; i < amount; i++)
		{
			points[i] = LevelManager.Goals[i];
			//Debug.Log (points[i].position);
		}

		transform.position = points[waypointIndex].transform.position;
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
            Release();
            GameManager.Instance.Lives--;
            GameManager.Instance.RemoveEnemy(this);
			return;
		}
	}

    private void Release()
    {
        IsActive = false;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (IsActive)
        {
            Health -= damage;

            if(health <= 0)
            {
                GameManager.Instance.Currency += 2;
                IsActive = false;
                Release();
                GameManager.Instance.RemoveEnemy(this);
            }
        }
    }
}
