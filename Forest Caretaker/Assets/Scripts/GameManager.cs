using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tree1; // tree prefabs
    public Transform trees; // stores all scene trees

    public float distance = 25f; //initial distance between trees
    public float maxTerrainHeight = 70f; // highest y point of the terrain
    private LayerMask groundMask;

    private float dayTime = 0f;
    public Transform sunTransform, moonTransform;
    private bool isDay = true;

    [SerializeField] private float axeChopDamage = 20f;
    [SerializeField] private GameObject player;
    public static float AxeChopDamage { get; set; }
    public static GameObject Player;

    // for test purposes
    public bool test;
    public float dayFactor;
    public static int days = 0;

    private void Awake()
    {
        AxeChopDamage = axeChopDamage;
        Player = player;
    }

    private void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
        if (test)
        {
            TreeInitializing();
        }
        StartDay();
    }

    private void Update()
    {
        ChangeMouseLockState();
        TimeProgression();
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
        sunTransform.rotation = Quaternion.Euler(sunAngle, 0f, 0f);
        moonTransform.rotation = Quaternion.Euler(moonAngle, 0f, 0f);
    }

    private void StartNight()
    {
        sunTransform.GetComponent<Light>().enabled = false;
        moonTransform.GetComponent<Light>().enabled = true;
    }

    private void StartDay()
    {
        sunTransform.GetComponent<Light>().enabled = true;
        moonTransform.GetComponent<Light>().enabled = false;
        days++;
        Debug.Log(days);
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
}
