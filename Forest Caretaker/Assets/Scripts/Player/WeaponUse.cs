using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUse : MonoBehaviour
{
    public Animator weaponAnimator;
    private bool leftClick = false;

    private void Start()
    {

    }

    private void Update()
    {
        leftClick = Input.GetMouseButton(0);

        if (leftClick)
        {
            weaponAnimator.SetBool("chopping", true);
        }
    }
}
