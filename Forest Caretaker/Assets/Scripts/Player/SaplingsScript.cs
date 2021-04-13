using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingsScript : MonoBehaviour
{
    private bool leftClick;
    private RaycastHit saplingsRayHit;
    public Transform trees;
    public GameObject babyTree;

    // start
    private void Start()
    {
        
    }

    // update
    private void Update()
    {
        EnvironmentInteraction();
    }

    private void EnvironmentInteraction()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out saplingsRayHit, 4f); // raycasts in front of player for planting trees
        leftClick = Input.GetMouseButton(0);

        if (saplingsRayHit.collider != null && GameManager.gameMode == 1 && leftClick)
        {
            Collider[] hitObjects = Physics.OverlapSphere(new Vector3(saplingsRayHit.point.x, saplingsRayHit.point.y - 4.25f, saplingsRayHit.point.z),
                15f);
            bool isAnyTreeTooClose = false;

            foreach (Collider hitObject in hitObjects)
                if (hitObject.tag == "Tree")
                {
                    isAnyTreeTooClose = true;
                    break;
                }

            if (saplingsRayHit.collider.tag == "Ground" && !isAnyTreeTooClose)
                PlantTree(saplingsRayHit.point.x, saplingsRayHit.point.z);
        }
    }

    private void PlantTree(float x, float z)
    {
        GameObject newTree = Instantiate(babyTree, new Vector3(x, saplingsRayHit.point.y + 4.25f, z), Quaternion.identity, trees);
        TreesManager.trees.Add(newTree.GetComponent<TreeScript>());
    }
}
