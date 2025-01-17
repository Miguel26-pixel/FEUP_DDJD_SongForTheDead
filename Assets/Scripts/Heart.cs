using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{

    [SerializeField] private float healthPoints;

    override public void Apply(Player player)
    {
        Debug.Log("Picked a heart!");
        Debug.Log("Current health points " + player.getHealth());
        float health = player.getHealth();

        if (health == player.getMaxHealth())
        {
            return;
        }

        Debug.Log("New health " + (health + healthPoints));

        player.setHealth(health + healthPoints);
        Destroy(gameObject);
    }
}
