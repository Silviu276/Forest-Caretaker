using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesManager : MonoBehaviour
{
    public static List<TreeScript> trees = new List<TreeScript>();

    private void Awake()
    {
        foreach (Transform tree in transform)
        {
            trees.Add(tree.GetComponent<TreeScript>());
        }
    }

    private void Start()
    {

    }

    public static void TreesDailyUpdate()
    {
        foreach (TreeScript tree in trees)
        {
            tree.TreeDailyStatsUpdate();
        }
    }
}
