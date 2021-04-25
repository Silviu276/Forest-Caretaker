using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingsScript : MonoBehaviour
{
    private bool leftClick;
    private RaycastHit saplingsRayHit;
    public Transform trees;
    public GameObject babyTree;
    private Animator saplingsAnimator;
    private bool isAnyTreeTooClose;
    public GameObject saplingsChecker;

    // start
    private void Start()
    {
        saplingsAnimator = GetComponent<Animator>();
    }

    // update
    private void Update()
    {
        EnvironmentInteraction();
        PlantAvailability();
    }

    private void EnvironmentInteraction()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out saplingsRayHit, 4f); // raycasts in front of player for planting trees
        leftClick = Input.GetMouseButton(0);

        /* // saplings action
        if (GameManager.gameMode == 1 && leftClick && !saplingsAnimator.GetBool("planting") && saplingsRayHit.collider != null)
        {
            saplingsAnimator.SetBool("planting", true);
            PlayerInteractions.PlayerMovementsToggle(false);
            Collider[] hitObjects = Physics.OverlapSphere(new Vector3(saplingsRayHit.point.x, saplingsRayHit.point.y - 4.25f, saplingsRayHit.point.z),
                15f);
            isAnyTreeTooClose = false;

            foreach (Collider hitObject in hitObjects)
                if (hitObject.tag == "Tree")
                {
                    isAnyTreeTooClose = true;
                    break;
                }
        } */

        if (GameManager.gameMode == 1 && saplingsRayHit.collider != null)
        {
            saplingsChecker.SetActive(true);
            PlantAvailability();
            if (leftClick && !saplingsAnimator.GetBool("planting") && !isAnyTreeTooClose)
            {
                saplingsAnimator.SetBool("planting", true);
                PlayerInteractions.PlayerMovementsToggle(false);
            }
        }
        else
        {
            saplingsChecker.SetActive(false);
        }
    }

    public void PlantTree() // animator event
    {
        if (saplingsRayHit.collider.tag == "Ground" && !isAnyTreeTooClose)
        {
            GameObject newTree = Instantiate(babyTree, new Vector3(saplingsRayHit.point.x, saplingsRayHit.point.y + 4.25f, saplingsRayHit.point.z),
                Quaternion.identity, trees);
            TreesManager.trees.Add(newTree.GetComponent<TreeScript>());
        }
    }

    private void PlantAvailability()
    {
        saplingsChecker.transform.position = new Vector3(saplingsRayHit.point.x, saplingsRayHit.point.y - 0.25f, saplingsRayHit.point.z);
        saplingsChecker.transform.rotation = Quaternion.identity;
        Collider[] hitColliders = Physics.OverlapSphere(new Vector3(saplingsRayHit.point.x, saplingsRayHit.point.y - 4.25f, saplingsRayHit.point.z),
            15f);
        isAnyTreeTooClose = false;

        foreach (Collider hitObject in hitColliders)
            if (hitObject.tag == "Tree")
            {
                isAnyTreeTooClose = true;
                break;
            }

        if (isAnyTreeTooClose)
            saplingsChecker.GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f, 0.5f);
        else
            saplingsChecker.GetComponent<MeshRenderer>().material.color = new Color(0f, 1f, 0f, 0.5f);
    }
}
