using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public float health = 100f;
    public Rigidbody treeBody;
    public MeshCollider trunkMeshC;
    public CapsuleCollider trunkCapsuleC;
    public BoxCollider trunkBoxC;
    private Vector3 treeFallDirection;
    private bool dead = false;

    private void Start()
    {

    }

    private void Update()
    {
        health -= 1 * Time.deltaTime;
        if (!dead)
            Dead();
    }

    // verifies if the tree is dead
    public void Dead()
    {
        if (health <= 0)
        {
            Destroy(trunkMeshC);
            treeBody.isKinematic = false;
            trunkCapsuleC.enabled = true;
            trunkBoxC.enabled = true;
            treeFallDirection = GameManager.Player.transform.forward;
            treeBody.AddForce(treeFallDirection, ForceMode.Impulse);
            dead = true;
            StartCoroutine(DeadDisappear());
        }
    }

    private IEnumerator DeadDisappear()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
