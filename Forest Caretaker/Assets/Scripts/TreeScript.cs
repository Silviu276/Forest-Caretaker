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
    private int healthChange = 0;
    public bool watered = false, sheared = false;

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
        HealthChange();
        daysAge++;
        Adult();
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

    private void HealthChange()
    {
        // positive
        healthChange = 0;
        if (watered)
            healthChange += (20 / Mathf.Clamp(daysAge, 1, 5));
        if (sheared)
            healthChange += (20 / Mathf.Clamp(daysAge, 1, 5));

        // negative
        if (!watered && !sheared)
            healthChange -= (30 / Mathf.Clamp(daysAge, 1, 5));

        // aplies health change
        health += healthChange;
        health = Mathf.Clamp(health, 10, 100);

        watered = sheared = false;
    }
}
