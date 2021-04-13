using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public float health = 100f;
    public int daysAge = 0;
    public Rigidbody treeBody;
    public MeshCollider trunkMeshC;
    public CapsuleCollider trunkCapsuleC;
    public BoxCollider trunkBoxC;
    private Vector3 treeFallDirection;

    private void Start()
    {

    }

    private void Update()
    {

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
            StartCoroutine(DeadDisappear());
        }
    }

    private IEnumerator DeadDisappear()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void TreeDailyStatsUpdate()
    {
        daysAge++;
        Adult();
        if (health > 10f)
            health -= 10f;
    }

    private void Adult()
    {
        if (daysAge == 5)
        {
            transform.Find("Trunk").gameObject.SetActive(true);
            transform.Find("Leaves").gameObject.SetActive(true);
            transform.Find("Sapling").gameObject.SetActive(false);
        }
    }
}
