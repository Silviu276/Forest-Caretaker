using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tree1;
    public Transform trees;
    public float distance = 25f, maxTerrainHeight = 70f;
    private LayerMask groundMask;

    private void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
        //TreeInitializing();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void TreeInitializing()
    {
        for (float x = distance; x <= 1000f - distance; x += distance)
        {
            for (float z = distance; z <= 1000f - distance; z += distance)
            {
                RaycastHit rayHit;
                float xDifference = Random.Range(-distance / 2, distance / 2);
                float zDifference = Random.Range(-distance / 2, distance / 2);
                Physics.Raycast(new Vector3(x + xDifference, maxTerrainHeight, z + zDifference), Vector3.down, out rayHit, 80f, groundMask, QueryTriggerInteraction.Ignore);
                Instantiate(tree1, new Vector3(x + xDifference, rayHit.point.y - 0.75f, z + zDifference), Quaternion.identity, trees);
            }
        }
    }
}
