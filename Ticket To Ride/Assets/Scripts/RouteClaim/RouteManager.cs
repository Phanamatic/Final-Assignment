using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RouteManager
{
    public List<RouteBase> Routes = new List<RouteBase>();
    public List<RouteBase> GeneralList = new List<RouteBase>();

    private CityManager cityobj;
    public void AddRoute(City s, City e, string c, int l, int n, string name)
    {
        //Routes.Add(new RouteBase(s, e, c, l, n, name));
        GeneralList.Add(new RouteBase(s, e, c, l, n, name));
    }

    public void RemoveRoute(City s, City e, string c, int l, int n, string name)
    {
        //Routes.Remove(new RouteBase(s, e, c,  l, n, name));
        GeneralList.Add(new RouteBase(s, e, c, l, n, name));
    }

    public bool ClaimRoute(RouteBase route, Player player)//add a player parameter as well and a traincard colour
    {
        Debug.Log("firstTime Looping....");
        bool firstCheck = false;
        bool secondCheck = false;
        bool thirdCheck = false;
        int x = route.getNodes();//---loop x times to remove x number of train cards
        string y = player.TrainCards[0].cardColorString;//--- the pos is equal to the index of groupbycol counter
        bool[] canRemove = new bool[player.TrainCards.Count];


        #region CheckInGeneralList
        for (int i = 0; i < GeneralList.Count; i++)
        {
            if (route == GeneralList[i])
            {
                route.isRouteClaimed(true);
                return false;
            }

        }
        #endregion//checks if route has already been claimed

        #region Check Nodes To TrainCard by Colour

        int[] GroupByColCounter = new int[8];

        int index = 0;
        //for loop to increment counters for each colour 
        for (int i = 0; i < player.TrainCards.Count; i++)
        {
            if (player.TrainCards[i].cardColorString == "green")
            {
                GroupByColCounter[index]++;
            }
            if (player.TrainCards[i].cardColorString == "black")
            {
                GroupByColCounter[index + 1]++;
            }
            if (player.TrainCards[i].cardColorString == "blue")
            {
                GroupByColCounter[index + 2]++;
            }
            if (player.TrainCards[i].cardColorString == "orange")
            {
                GroupByColCounter[index + 3]++;
            }
            if (player.TrainCards[i].cardColorString == "purple")
            {
                GroupByColCounter[index + 4]++;
            }
            if (player.TrainCards[i].cardColorString == "white")
            {
                GroupByColCounter[index + 5]++;
            }
            if (player.TrainCards[i].cardColorString == "red")
            {
                GroupByColCounter[index + 6]++;
            }
            if (player.TrainCards[i].cardColorString == "yellow")
            {
                GroupByColCounter[index + 7]++;
            }
            if (player.TrainCards[i].IsLocomotive == true)
            {
                GroupByColCounter[index]++;
                GroupByColCounter[index + 1]++;
                GroupByColCounter[index + 2]++;
                GroupByColCounter[index + 3]++;
                GroupByColCounter[index + 4]++;
                GroupByColCounter[index + 5]++;
                GroupByColCounter[index + 6]++;
                GroupByColCounter[index + 7]++;
            }


        }


        bool[] NumCardsToNode = new bool[8];
        Debug.Log("number of nodes: " + route.getNodes());
        for (int i = 0; i < GroupByColCounter.Length; i++)
        {
            Debug.Log("number of cards of colour: " + GroupByColCounter[i]);
            if (route.getNodes() > GroupByColCounter[i])
            {

                NumCardsToNode[i] = false;
            }
            else if (route.getNodes() <= GroupByColCounter[i])
            {
                NumCardsToNode[i] = true;
            }

        }

        for (int j = 0; j < GroupByColCounter.Length; j++)
        {
            if (NumCardsToNode[j] == true)
            {
                Debug.Log("firstcheck is TRUE");

                firstCheck = true;
            }
        }

        #endregion

        #region CheckColour


        for (int j = 0; j < player.TrainCards.Count; j++)
        {
            if (player.TrainCards[j].cardColorString == route.getColour())
            {
                Debug.Log("secondcheck is TRUE");
                secondCheck = true;
            }
            else if (route.getColour() == "grey")
            {
                Debug.Log("secondcheck is TRUE");
                secondCheck = true;
            }
            else
            {
                Debug.Log("secondcheck is FALSE");
            }
        }


        #endregion

        #region CheckSingleDoubleRoute


        if (route.getNumRoutes() == 1)
        {
            Debug.Log("thirdcheck is TRUE");
            thirdCheck = true;

        }

        if (route.getNumRoutes() == 2)
        {
            if (player.ConnectedCities.Count == 0)
            {
                Debug.Log("thirdcheck is TRUE");
                thirdCheck = true;
            }

            for (int i = 0; i < player.ConnectedCities.Count; i++)
            {
                for (int j = 0; j < player.ConnectedCities.Count; j++)
                {

                    Debug.Log("city at i in GL: " + GeneralList[i].getStart());
                    Debug.Log("city at i in GL: " + GeneralList[i].getEnd());
                    Debug.Log("ROUTE START: " + route.getStart());

                    if (GeneralList[i].getStart() == route.getStart() || GeneralList[i].getEnd() == route.getStart())
                    {
                        Debug.Log("city at j in GL: " + GeneralList[j].getStart());
                        Debug.Log("city at j in GL: " + GeneralList[j].getEnd());
                        Debug.Log("ROUTE END: " + route.getEnd());

                        if (GeneralList[j].getEnd() == route.getEnd() || GeneralList[j].getStart() == route.getEnd())
                        {
                            //because these conditions are found to be true, it means one player has already claimed a double route 
                            Debug.Log("third check is false");
                            thirdCheck = false;

                        }//end if j
                        else
                        {
                            thirdCheck = true;

                        }



                    }//end if i

                    thirdCheck = true;

                }//end for j
            }//end for i

        }

        #endregion

        if (firstCheck == true && secondCheck == true && thirdCheck == true)
        {

            Debug.Log("All checks are TRUE");

            //remove x amount of cards 
            for (int i = 0; i < x; i++)//number of times card is removed is equal to num of nodes in route
            {
                for (int k = 0; k < player.TrainCards.Count; k++)
                {
                    if (player.TrainCards[k].cardColorString == route.getColour())//more than one colour
                    {
                        player.TrainCards.Remove(player.TrainCards[k]);//remove the train card at that position of the colour counter 
                                                                       //this will happen x times which is equal to the number of nodes being claimed
                    }
                }


            }

            route.isRouteClaimed(true);
            return true;
        }

        return false;

    }


    public List<RouteBase> ConnectedRoutes(Player p, RouteBase route)
    {
        List<RouteBase> connectedRoutes = new List<RouteBase>();
        int counterOfCitySet = 0;
        for (int i = 0; i < p.RoutesClaimed.Count; i++)
        {
            if (p.RoutesClaimed[i].getStart() == route.getStart()
                || p.RoutesClaimed[i].getStart() == route.getEnd()
                || p.RoutesClaimed[i].getEnd() == route.getStart()
                || p.RoutesClaimed[i].getEnd() == route.getStart())//check all combinations where the start/end city of the
                                                                   //route we cant to claim is equal to start/end city of player's claimed routes
            {
                List<City> connectCities = new List<City>();//add to a new list the cities which are connected (start and end of both)
                connectCities.Add(route.getStart());
                connectCities.Add(route.getEnd());
                connectCities.Add(p.RoutesClaimed[i].getStart());
                connectCities.Add(p.RoutesClaimed[i].getEnd());

                City tempShared = new City();
                City tempStart = new City();
                City tempEnd = new City();
                int templength = 0;

                for (int j = 1; j < p.RoutesClaimed.Count; j++)
                {
                    Debug.Log("city at i: " + connectCities[i] + " city at j: " + connectCities[j]);

                    if (connectCities[i] == connectCities[j])
                    {
                        tempShared = connectCities[i];
                        connectCities.Remove(connectCities[i]);//take out a repeated city so that if two routes are connected only 3 cities are added not 4

                        while (tempEnd != tempShared)
                        {
                            tempEnd = connectCities[j];
                        }

                        counterOfCitySet++;
                    }
                    else
                    {
                        tempStart = connectCities[i];
                        tempEnd = connectCities[j];
                    }
                }
                templength = route.getNodes() + p.RoutesClaimed[i].getNodes();
                RouteBase newRoute = new RouteBase(tempStart, tempEnd, " ", templength, 1, tempStart.ToString() + "-" + tempEnd.ToString());
                connectedRoutes.Add(newRoute);
                Debug.Log("name of new route: " + newRoute.getRouteName());
                int length = 0;
                length = length + newRoute.getNodes();
                Debug.Log("sum of routes at i:" + length);
            }

        }

        return connectedRoutes;
    }


    public bool DestinationTicketAcheived(Player p, List<RouteBase> routelist)
    {
        for(int i =0; i<routelist.Count; i++)
        {
            if((p.DestinationCards[i].startCity == routelist[i].getEnd().ToString() && p.DestinationCards[i].endCity == routelist[i].getEnd().ToString())||
                (p.DestinationCards[i].startCity == routelist[i].getStart().ToString() && p.DestinationCards[i].endCity == routelist[i].getStart().ToString()))//if the startcity and end city of the player's destination card is found in their connected routes list...
            {
                Debug.Log(p.DestinationCards[i].name + " destination ticket completed!");
                return true;
            }
        }

        return false;
    }

  
    public void longestRouteCalculated(List<RouteBase> connected)
    {
        List<List<RouteBase>> connectList = new List<List<RouteBase>>();
        //connected is the list after the checkconnectroutes method is called

        int sum = 0;
        int tempsum = 0;

        for (int i = 0; i < connectList.Count; i++)
        {
            connectList.Add(connected);

            for (int j = 0; j < connected.Count; j++)
            {
                RouteBase temproute = connected[j];
                sum = sum + temproute.getNodes();
            }

        }


    }


}
