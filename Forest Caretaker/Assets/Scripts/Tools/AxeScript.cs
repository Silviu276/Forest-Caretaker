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

        if (GameManager.gameMode == 0 && leftClick && !axeAnimator.GetBool("chopping")) // starts chopping
        {
            axeAnimator.SetBool("chopping", true);
            PlayerInteractions.PlayerMovementsToggle(false);
        }
    }

    public void DamageTree() // animator event
    {
        if (axeRayHit.collider != null) // player ray hit something
        {
            if (axeRayHit.collider.tag == "Tree") // player ray hit a tree
            {
                TreeScript selectedTree = axeRayHit.collider.GetComponentInParent<TreeScript>(); // stores the hit tree
                selectedTree.health -= axeChopDamage; // decreases its health
                selectedTree.health = Mathf.Clamp(selectedTree.health, 0, 100);
                selectedTree.Dead(); // verifies if it is dead
            }
        }
    }
}
