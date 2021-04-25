using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShearsScript : MonoBehaviour
{
    private RaycastHit shearsRayHit;
    private bool leftClick;
    private Animator shearsAnimator;

    // start
    private void Start()
    {
        shearsAnimator = GetComponent<Animator>();
    }

    // update
    private void Update()
    {
        EnvironmentInteraction();
    }

    private void EnvironmentInteraction()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out shearsRayHit, 1.8f);
        leftClick = Input.GetMouseButtonDown(0);

        if (shearsRayHit.collider != null) // shear ray hits something
            // shears action
            if (shearsRayHit.collider.tag == "Tree" && GameManager.gameMode == 0 && leftClick && !shearsAnimator.GetBool("cutting"))
            {
                shearsAnimator.SetBool("cutting", true);
                PlayerInteractions.PlayerMovementsToggle(false);
            }
    }

    public void ShearTree() // animator event
    {
        TreeScript selectedTree = shearsRayHit.collider.GetComponentInParent<TreeScript>();
        selectedTree.sheared = true;
    }
}
