using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUse : MonoBehaviour
{
    public Animator weaponAnimator;
    private bool leftClick = false;
    private TreeScript selectedTree;
    public float axeDamage = 20f;
    private RaycastHit weaponRayHit;
    public static int mustHit;

    private void Start()
    {
        mustHit = 0;
    }

    private void Update()
    {
        EnvironmentInteraction();
    }

    private void EnvironmentInteraction()
    {
        Physics.Raycast(transform.position, transform.forward, out weaponRayHit, 1.8f);
        Debug.DrawRay(transform.position, transform.forward * 1.8f, Color.red);
        leftClick = Input.GetMouseButton(0);

        if (leftClick && !weaponAnimator.GetBool("chopping"))
        {
            weaponAnimator.SetBool("chopping", true);
            mustHit = 1;
        }

        if (weaponRayHit.collider != null)
        {
            if (weaponRayHit.collider.tag == "Tree" && mustHit == 1)
            {
                selectedTree = weaponRayHit.collider.GetComponentInParent<TreeScript>();
                DamageTree(selectedTree);
            }
        }
    }

    private void DamageTree(TreeScript tree)
    {
        tree.health -= axeDamage;
        tree.Dead();
    }
}
