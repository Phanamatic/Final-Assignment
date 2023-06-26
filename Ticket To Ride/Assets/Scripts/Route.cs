using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route
{
    public City StartCity { get; private set; }
    public City EndCity { get; private set; }
    public string Color { get; private set; }
    public int Length { get; private set; }
    public int DoubleRouteIndex { get; private set; }
    public string RouteName { get; private set; }

    public Route(City startCity, City endCity, string color, int length, int doubleRouteIndex, string routeName)
    {
        StartCity = startCity;
        EndCity = endCity;
        Color = color;
        Length = length;
        DoubleRouteIndex = doubleRouteIndex;
        RouteName = routeName;
    }
}
