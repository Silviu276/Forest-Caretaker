using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour
{
    private float axeChopDamage;
    private RaycastHit axeRayHit;
    private bool leftClick;
    private Animator axeAnimator;

    // start
    private void Start()
    {
        axeChopDamage = GameManager.AxeChopDamage;
        axeAnimator = GetComponent<Animator>();
    }

    // update
    private void Update()
    {
        EnvironmentInteraction();
    }

    private void EnvironmentInteraction()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out axeRayHit, 1.8f); // raycasts in front of player
        leftClick = Input.GetMouseButton(0);

        if (leftClick && !axeAnimator.GetBool("chopping") && GameManager.gameMode == 0) // starts chopping
            axeAnimator.SetBool("chopping", true);
    }

    public void DamageTree()
    {
        if (axeRayHit.collider != null) // player ray hit something
        {
            if (axeRayHit.collider.tag == "Tree") // player ray hit a tree
            {
                TreeScript selectedTree = axeRayHit.collider.GetComponentInParent<TreeScript>(); // stores the hit tree
                selectedTree.health -= axeChopDamage; // decreases its health
                selectedTree.Dead(); // verifies if it is dead
            }
        }
    }
}
