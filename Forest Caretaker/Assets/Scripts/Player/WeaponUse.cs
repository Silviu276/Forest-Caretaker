using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUse : MonoBehaviour
{
    public Animator weaponAnimator;
    public static bool leftClick = false;
    public static RaycastHit weaponRayHit;
    private TreeScript selectedTree;

    public Text healthText; // from tree stats canvas
    public Image healthAmount;
    public GameObject healthBar;

    public Image sleepDark; // canvas elements when sleeping
    public Text dayNumber;

    private void Start()
    {
        
    }

    private void Update()
    {
        EnvironmentInteraction();
    }

    private void EnvironmentInteraction()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out weaponRayHit, 1.8f); // raycasts in front of player
        leftClick = Input.GetMouseButton(0);

        if (leftClick && !weaponAnimator.GetBool("chopping")) // starts chopping
        {
            weaponAnimator.SetBool("chopping", true);
        }

        if (weaponRayHit.collider != null) // ray hit something
        {
            switch(weaponRayHit.collider.tag)
            {
                case "Tree":
                    selectedTree = weaponRayHit.collider.gameObject.GetComponentInParent<TreeScript>(); // stores the hit tree
                    healthBar.SetActive(true); // shows its health
                    healthText.text = selectedTree.health.ToString();
                    healthAmount.fillAmount = selectedTree.health / 100f;
                    break;
            }

            switch(weaponRayHit.collider.name)
            {
                case "Door":
                    OpenDoor(weaponRayHit.collider.gameObject);
                    break;
                case "Bed":
                    Sleep();
                    break;
                case "Fireplace":
                    Debug.Log("Fireplace");
                    break;
            }
        }
        else
        {
            healthBar.SetActive(false); 
        }
    }

    private void OpenDoor(GameObject door)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int openDoor;
            if (transform.position.z - door.transform.position.z > 0f)
                openDoor = 2;
            else openDoor = 1;
            door.GetComponent<Animator>().SetInteger("isOpened", openDoor);
        }
    }

    private void Sleep()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            dayNumber.text = $"Day {GameManager.days}";
            sleepDark.gameObject.SetActive(true);
            dayNumber.gameObject.SetActive(true);
            sleepDark.canvasRenderer.SetAlpha(0f);
            dayNumber.canvasRenderer.SetAlpha(0f);
            sleepDark.CrossFadeAlpha(1f, 1f, true);
            dayNumber.CrossFadeAlpha(1f, 1f, true);
            StartCoroutine(EndSleep());
        }
    }

    private IEnumerator EndSleep()
    {
        yield return new WaitForSeconds(3f);
        sleepDark.CrossFadeAlpha(0f, 1f, true);
        dayNumber.CrossFadeAlpha(0f, 1f, true);
    }
}
