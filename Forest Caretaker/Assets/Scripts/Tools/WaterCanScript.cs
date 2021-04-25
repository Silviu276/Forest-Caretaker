using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCanScript : MonoBehaviour
{
    private bool leftClick;
    private RaycastHit waterCanRayHit;
    private Animator waterCanAnimator;

    // start
    private void Start()
    {
        waterCanAnimator = GetComponent<Animator>();
    }

    // update
    private void Update()
    {
        EnvironmentInteraction();
    }

    private void EnvironmentInteraction()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out waterCanRayHit, 1.8f);
        leftClick = Input.GetMouseButton(0);

        if (waterCanRayHit.collider != null) // water can ray hits something
            // water can action
            if (waterCanRayHit.collider.tag == "Tree" && GameManager.gameMode == 0 && leftClick && !waterCanAnimator.GetBool("pouring"))
            {
                waterCanAnimator.SetBool("pouring", true);
                PlayerInteractions.PlayerMovementsToggle(false);
            }
    }

    public void WaterTree() // animator event
    {
        TreeScript selectedTree = waterCanRayHit.collider.GetComponentInParent<TreeScript>();
        selectedTree.watered = true;
    }
}
