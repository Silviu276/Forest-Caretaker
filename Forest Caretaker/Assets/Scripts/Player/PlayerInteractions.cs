using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    private RaycastHit playerRayHit;
    private TreeScript selectedTree;
    public GameObject healthBar;
    public Text dayNumber;
    public Image sleepDark;
    private GameObject[] tools = new GameObject[5]; // 0 - hands ; 1 - axe ; 2 - water can ; 3 - shears ; 4 - saplings

    // start
    private void Start()
    {
        ToolsInit();
        ChangeTool(0);
    }

    // update
    private void Update()
    {
        EnvironmentInteraction();
        ToolsChange();
    }

    // executes all player interactions with the environment
    private void EnvironmentInteraction()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out playerRayHit, 1.8f); // raycasts in front of player

        if (playerRayHit.collider != null) // ray hit something
        {
            switch (playerRayHit.collider.tag)
            {
                case "Tree":
                    selectedTree = playerRayHit.collider.gameObject.GetComponentInParent<TreeScript>(); // stores the hit tree
                    healthBar.SetActive(true); // shows its health
                    healthBar.transform.Find("HealthText").GetComponent<Text>().text = selectedTree.health.ToString();
                    healthBar.transform.Find("Health").GetComponent<Image>().fillAmount = selectedTree.health / 100f;
                    break;
            }

            switch (playerRayHit.collider.name)
            {
                case "Door":
                    OpenDoor(playerRayHit.collider.gameObject);
                    break;
                case "Bed":
                    Sleep();
                    break;
                case "Fireplace":
                    break;
            }
        }
        else
            healthBar.SetActive(false);
    }

    // COTTAGE INTERACTIONS
    private void OpenDoor(GameObject door)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int openDoor; // sets direction to open
            if (transform.position.z - door.transform.position.z > 0f)
                openDoor = 2;
            else openDoor = 1;
            door.GetComponent<Animator>().SetInteger("isOpened", openDoor);
        }
    }

    private void Sleep()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.dayTime >= 14f)
        {
            // shows day canvas
            GameManager.dayTime = 0f;
            dayNumber.text = $"Day {GameManager.days}";
            sleepDark.gameObject.SetActive(true);
            dayNumber.gameObject.SetActive(true);
            sleepDark.canvasRenderer.SetAlpha(0f);
            dayNumber.canvasRenderer.SetAlpha(0f);
            sleepDark.CrossFadeAlpha(1f, 1f, true);
            dayNumber.CrossFadeAlpha(1f, 1f, true);
            StartCoroutine(EndSleep());

            // deactivates movement and interactions
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private IEnumerator EndSleep()
    {
        yield return new WaitForSeconds(1f);
        Vector3 awakePos = new Vector3(528f, 11.94f, 523f); // respawn position
        transform.position = new Vector3(0f, 500f, 0f);
        yield return new WaitForSeconds(3f);
        transform.position = awakePos; // respawns player
        sleepDark.CrossFadeAlpha(0f, 1f, true);
        dayNumber.CrossFadeAlpha(0f, 1f, true);
        yield return new WaitForSeconds(0.9f);
        GetComponent<PlayerMovement>().enabled = true; // activates movement again
    }

    // TOOLS INTERACTIONS
    // changes the equipped tool at input
    private void ChangeTool(int toolEquipped)
    {
        for (int i = 0; i < 5; i++)
            if (i == toolEquipped)
                tools[i].SetActive(true);
            else
                tools[i].SetActive(false);
    }

    // verifies tool change input
    private void ToolsChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeTool(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeTool(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeTool(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeTool(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeTool(4);
    }

    private void ToolsInit()
    {
        tools[0] = Camera.main.transform.Find("Hands").gameObject;
        tools[1] = Camera.main.transform.Find("Axe").gameObject;
        tools[2] = Camera.main.transform.Find("WaterCan").gameObject;
        tools[3] = Camera.main.transform.Find("Shears").gameObject;
        tools[4] = Camera.main.transform.Find("Saplings").gameObject;
    }

    public static void PlayerMovementsToggle(bool newStatus)
    {
        Camera.main.GetComponentInParent<PlayerMovement>().enabled = newStatus;
        Camera.main.GetComponent<PlayerLook>().enabled = newStatus;
    }
}
