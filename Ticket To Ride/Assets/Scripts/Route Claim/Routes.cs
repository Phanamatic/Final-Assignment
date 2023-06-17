using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routes : MonoBehaviour
{
    [SerializeField] private GameObject Route;
    [SerializeField] public string RouteName;
    [SerializeField] public Color colour;
    [SerializeField] public Vector2 startCity;
    [SerializeField] public Vector2 endCity;
    [SerializeField] public int numNodes;

    public GameObject routeChild;
    public GameObject routeGrandChild;

    private void Start()
    {
        Route = this.gameObject;
        routeChild = new GameObject();

        if (Route.GetComponent<Transform>().childCount == 2)
        {
            routeChild.transform.SetParent(Route.transform);//Route.transform or just Route?

        }



        //FillRoute();

    }

    /*
    void FillRoute()//assign child transforms to route 
    {
        blockList.Clear();//clear the list first
        blocks = Route.GetComponentsInChildren<Transform>();//get all transform components in the children 
        
        foreach (Transform child in blocks)
        {

            if (child != Route.transform)
            {
                blockList.Add(child);
            }
        }


    }*/



}
