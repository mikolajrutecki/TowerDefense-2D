using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private EnemyScript target;

    private Tower parent;

    private Element elementType;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        MoveToTarget();
	}

    public void Init(Tower parent)
    {
        this.target = parent.Target;
        this.parent = parent;
        this.elementType = parent.ElementType;
    }

    private void MoveToTarget()
    {
        if(target != null && target.IsActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * parent.ProjectileSpeed);
        }
        else if (!target.IsActive)
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            if(target.gameObject == other.gameObject)
            {
                target.TakeDamage(parent.Damage);
                GameManager.Instance.Pool.ReleaseObject(gameObject);
            }
        }
    }
}
