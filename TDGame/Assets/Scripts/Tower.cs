using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	private SpriteRenderer mySpriteRenderer;

    private EnemyScript target;

    public EnemyScript Target
    {
        get
        {
            return target;
        }
    }

    private Queue<EnemyScript> enemies = new Queue<EnemyScript>();

    private Transform targetTransform;

    private bool canAttack = true;

    [SerializeField]
    private int damage;

    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            this.damage = value;
        }
    }

    private float attackTimer;


    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    private string projectileType;

    [SerializeField]
    private float projectileSpeed;

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }


	// Use this for initialization
	void Awake () {

		mySpriteRenderer = GetComponent<SpriteRenderer>();
	}



    // Update is called once per frame
    void Update () {
        Attack();
	}


	public void Select()
	{
		mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
	}

    private void Attack()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if(attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }

        if(target == null && enemies.Count > 0)
        {
            target = enemies.Dequeue();
        }
        if(target != null && target.IsActive)
        {
            if (canAttack)
            {
                Shoot();

                canAttack = false;
            }
        }
        else if(enemies.Count > 0)
        {
            target = enemies.Dequeue();
        }
        if(target != null && !target.Alive)
        {
            target = null;
        }
    }

    private void Shoot()
    {
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();

        projectile.transform.position = transform.position;

        projectile.Init(this);

    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemies.Enqueue(other.GetComponent<EnemyScript>());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            target = null;
        }
    }

}
