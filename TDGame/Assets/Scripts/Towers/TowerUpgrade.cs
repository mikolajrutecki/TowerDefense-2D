using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade {

    public int Price { get; private set; }

    public int Damage { get; private set; }

    public TowerUpgrade(int price, int damage)
    {
        this.Price = price;
        this.Damage = damage;
    }
}
