using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RouteBase
{
    City start, end;
    int numOfNodes;
    bool isClaimed;
    int numOfRoutes;
    TrainCard card;
    string Routecolour;
    string RouteName;

    public RouteBase(City startCity, City endCity, string col,  int length, int n, string name)//addtraincard colour as well
    {
        Routecolour = col;
        //col = Routecolour;
        start = startCity;
        end = endCity;
        numOfNodes = length;
        numOfRoutes = n;
        RouteName = name;
    }

    //seperate constructor for double routes ??

    public int points()
    {
        switch (numOfNodes)
        {
            case 1:
                return 1;
                break;
            case 2:
                return 2;
                break;
            case 3:
                return 4;
                break;
            case 4:
                return 7;
                break;
            case 5:
                return 10;
                break;
            case 6:
                return 15;
                break;

        }

        return 0;
    }

    /*public bool isRouteClaimed { get; set; }
    public City End { get; set; }
    public City Start { get; set; }
    public int NumOfNodes { get; set; }
    public int NumOfRoutes { get; set; }
    public string colour { get; set; }
    public string NameRoute { get; set }*/


    #region Setters
    public void isRouteClaimed(bool c)
    {
        isClaimed = c;
    }


    public void setNumOfNodes(int n)
    {
        numOfNodes = n;
    }

    public void setStart(City s)
    {
       start = s;
    }

    public void setEnd(City e)
    {
        end = e;
    }
    #endregion

    #region Getters
    public bool getisClaimed()
    {
        return isClaimed;
    }


    public int getNodes()
    {
        return numOfNodes;
    }
    public int getNumRoutes()
    {
        return numOfRoutes;
    }

    public City getStart()
    {
        return start;
    }

    public City getEnd()
    {
        return end;
    }
    public string getColour()
    {
        return Routecolour;
    }
    public string getRouteName()
    {
        return RouteName;
    }

    #endregion
}
