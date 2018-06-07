using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTower : Tower {

    private void Start()
    {
        ElementType = Element.RED;

        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(2,25),
            new TowerUpgrade(5, 30),
            new TowerUpgrade(10,40)
        };
    }
}
