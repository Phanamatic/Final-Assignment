using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteClaim : MonoBehaviour
{
    //Notes:
    /*When a player clicks on route to add it, that gameObj should be added into a temporary array.

   Make a general claimed routes array/list to refer to ??

   Check 0: DONE
   - check if route is in the other player's claimed route array/List OR general claimed routes array
   - If not, proceed to next checks
   - If so, return false

   Check 1: DONE
   - train card colour matches route color -- each route must be assigned a colour (an int?string?char?) 
   - switch case for the colours???
   - if yes, go to next checks 
   - if not, go to check 1.1 

       -> Check 1.1: DONE
           - check if the route colour is grey...if yes, skip to claim route. else do not claim route
           - if yes, proceed to check 2
           - if no, return false

   Check 2:HALF-DONE...
   - call from PlayerScript the arrays of the sets...
   - check that the number of blocks in route gameObj == number of cards -- do a count of each grandchildren blocks of each child subroute in the array/list
   - if yes, go to check 3
   - if no, return false

   Check 3:DONE
   - check double / single route....
   - a double route is a route with TWO block lists -- 2 children in the route gameObj

       -> If route is double....
           - choose one route to claim -- add only one of the routes into the claimed route array/list


       -> If route is single...
           - claim route and add route to player's claimed routeList/array AND add to general Route Claim array/List 

    */

    Routes route;//route gameOj

    //player object
    Player player;

    // private GameObject subRoute;

   // public Transform[] nodes;

    //list of block children in route gameObj 
    //public List<Transform> nodesList = new List<Transform>();

    //route list general
    public List<Routes> GeneralRouteList = new List<Routes>();

    Vector2 start;
    Vector2 end;

    //train card object
    TrainCard train_card;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //we can change this getKeyDown
        if(Input.GetKeyDown(0))//check if claimed button is clicked every frame/constantly so that on each click throughout the game, claimRoute is possible
        {
            ClaimRoute();
        }

    }


    public void ClaimRoute()
    {
        //check pos of first block and pos of last block
        //check that the distance between the two blocks is equal to the distance between the start city and end city
        //if true, there is a route
        //add route game object to the player's route array
        //check if route game object is already in player's array 
     

        checkColour();//check colour

        if (checkColour() == true)
        {
            checkNodes();//check num of nodes to cards

            if(checkNodes() == true)
            {
               
                for(int i =0; i< player.RoutesClaimed.Count; i++)
                {
                    if(route != GeneralRouteList[i])//check it is not already in generalRouteList 
                    {
                        player.RoutesClaimed.Add(route);
                        GeneralRouteList.Add(route);
                    }
                }
                
            }
            
        }

    }

    public bool checkColour()
    {
        if (route.GetComponent<Color>() == train_card.cardColor || route.GetComponent<Color>() == Color.grey)
        {
            return true;
        }
        return false;
    }

    public bool checkNodes()
    {
        
        if(checkDoubleSingle() == 1)
        {
            if (route.GetComponent<Transform>().childCount == player.TrainCards.Count)//NB:it must count children of children
            {
                return true;
            }

        }
        else if(checkDoubleSingle() == 2)
        {
            if(route.routeChild.GetComponent<Transform>().childCount == player.TrainCards.Count)
            {
                return true;
            }

        }

        return false;
    }

    public int checkDoubleSingle()
    {
        bool isRoute;


        if (route.GetComponent<Transform>().childCount == 2)
        {
            return 2;//double route because there are only 2 child objects with grandchild objects as the nodes
        }
        else if(route.GetComponent<Transform>().childCount > 2)
        {
            return 1;//single route because the children objects are the nodes
        }

        return 0;
    }

    public int LongestRoute()//use pathfinding-----make a new script entirely??
    {
        return 0;
    }

}
