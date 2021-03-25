using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public float health = 100f;
    public Rigidbody treeBody;
    public MeshCollider trunkMeshC;
    public BoxCollider trunkBoxC;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void Dead()
    {
        if (health <= 0)
        {
            trunkMeshC.enabled = false;
            trunkBoxC.enabled = true;
            treeBody.isKinematic = false;
        }
    }
}
