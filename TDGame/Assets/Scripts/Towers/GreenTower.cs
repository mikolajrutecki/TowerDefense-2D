using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTower : Tower {

    public void Start()
    {
        ElementType = Element.GREEN;

        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(3,18),
            new TowerUpgrade(10,21),
            new TowerUpgrade(20,25)
        };
    }
}
