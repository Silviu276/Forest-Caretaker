using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject tree1; // tree prefabs
    public Transform trees; // stores all scene trees

    public float distance = 25f; //initial distance between trees
    public float maxTerrainHeight = 70f; // highest y point of the terrain
    private LayerMask groundMask;

    public static float dayTime = 0f;
    [SerializeField] private Transform sunTransform, moonTransform;
    public static Transform SunTransform, MoonTransform;
    private bool isDay = true;

    [SerializeField] private float axeChopDamage = 20f;
    [SerializeField] private GameObject player;
    public static float AxeChopDamage { get; set; }
    public static GameObject Player;

    // for test purposes
    public bool test;
    public float dayFactor;
    public static int days = 0;
    public static int gameMode = 0; // 0 - normal ; 1 - plant mode
    public Text gamemodeText;

    private void Awake()
    {
        SunTransform = sunTransform;
        MoonTransform = moonTransform;
        AxeChopDamage = axeChopDamage;
        Player = player;
    }

    private void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
        if (test)
            TreeInitializing();
        StartDay();
    }

    private void Update()
    {
        ChangeMouseLockState();
        TimeProgression();
        ChangeGameMode();
        if (Input.GetKeyDown(KeyCode.P))
        {
            dayFactor = 10f;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            dayFactor = 0.1f;
        }
    }

    // initializes random trees all around the map
    private void TreeInitializing()
    {
        for (float x = distance; x <= 1000f - distance; x += distance)
        {
            for (float z = distance; z <= 1000f - distance; z += distance)
            {
                RaycastHit rayHit;
                float xDifference = Random.Range(-distance / 3, distance / 3);
                float zDifference = Random.Range(-distance / 3, distance / 3);
                Physics.Raycast(new Vector3(x + xDifference, maxTerrainHeight, z + zDifference), Vector3.down, out rayHit, 80f, groundMask, QueryTriggerInteraction.Ignore);
                Instantiate(tree1, new Vector3(x + xDifference, rayHit.point.y + 4.25f, z + zDifference), Quaternion.identity, trees);
            }
        }
    }

    // progresses the time
    private void TimeProgression()
    {
        if (dayTime > 24f) // a day has passed
            dayTime -= 24f; // resets daytime to 0
        if (isDay && dayTime > 12f) // the noon has passed
        {
            isDay = false;
            StartNight();
        }
        else if (!isDay && dayTime <= 12f) // the night has passed
        {
            isDay = true;
            StartDay();
        }

        dayTime += dayFactor * Time.deltaTime; // time is passing
        float sunAngle = dayTime * 15f; // updates sun angle according to actual day time
        float moonAngle = sunAngle + 180f; // updates moon angle on the other side of sun

        // updates sun and moon rotations
        SunTransform.rotation = Quaternion.Euler(sunAngle, 0f, 0f);
        MoonTransform.rotation = Quaternion.Euler(moonAngle, 0f, 0f);
    }

    private void StartNight()
    {
        SunTransform.GetComponent<Light>().enabled = false;
        MoonTransform.GetComponent<Light>().enabled = true;
    }

    public static void StartDay()
    {
        SunTransform.GetComponent<Light>().enabled = true;
        MoonTransform.GetComponent<Light>().enabled = false;
        days++;
        TreesManager.TreesDailyUpdate();
    }

    private void ChangeMouseLockState()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void ChangeGameMode()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (gameMode == 0)
            {
                gameMode = 1;
                gamemodeText.gameObject.SetActive(true);
            }
            else if (gameMode == 1)
            {
                gameMode = 0;
                gamemodeText.gameObject.SetActive(false);
            }
        }
    }
}
