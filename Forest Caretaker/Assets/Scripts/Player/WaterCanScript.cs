using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCanScript : MonoBehaviour
{
    private bool leftClick;
    private RaycastHit waterCanRayHit;

    // start
    private void Start()
    {
        
    }

    // update
    private void Update()
    {
        
    }

    private void EnvironmentInteraction()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out waterCanRayHit, 1.8f);
        leftClick = Input.GetMouseButton(0);

        if (leftClick && GameManager.gameMode == 0)
        {

        }
    }
}
