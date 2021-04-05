using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeUse : MonoBehaviour
{
    private float axeChopDamage;

    private void Start()
    {
        axeChopDamage = GameManager.AxeChopDamage;
    }

    public void DamageTree()
    {
        if (WeaponUse.weaponRayHit.collider != null) // player ray hit something
        {
            if (WeaponUse.weaponRayHit.collider.tag == "Tree") // player ray hit a tree
            {
                TreeScript selectedTree = WeaponUse.weaponRayHit.collider.GetComponentInParent<TreeScript>(); // stores the hit tree
                selectedTree.health -= axeChopDamage; // decreases its health
                selectedTree.Dead(); // verifies if it is dead
            }
        }
    }
}
