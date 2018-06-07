using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    RED, GREEN, NONE
}

public abstract class Tower : MonoBehaviour {

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

    public TowerUpgrade[] Upgrades { get; protected set; }

    private int level;

    public int Level { get; protected set; }

    public TowerUpgrade NextUpgrade
    {
        get
        {
            if(Upgrades.Length > Level - 1)
            {
                return Upgrades[Level - 1];
            }
            return null;
        }
    }

    [SerializeField]
    private int damage;

    public Element ElementType { get; protected set; }

    public int Price { get; set; }

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
        Level = 1;
	}



    // Update is called once per frame
    void Update () {
        Attack();
        GameManager.Instance.UpdateUpgradeTip();
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

    public virtual void Upgrade()
    {
        GameManager.Instance.Currency -= NextUpgrade.Price;
        Price += NextUpgrade.Price;
        this.damage = NextUpgrade.Damage;
        Level++;
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
