using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public RouteManager Routes { get; set; } = new RouteManager();

    public List<GameObject> GameObjCities { get; set; } = new List<GameObject>(); //populate list with gameobjects of cities

    public Board()
    {
        AddRoutesToBoard();
    }

    public void AddRoutesToBoard()
    {
        #region GreyRoutes
        Routes.AddRoute(City.Montreal, City.Boston, "grey", 2, 2, "MONTREAL-BOSTON");
        Routes.AddRoute(City.Toronto, City.Montreal, "grey", 3, 1, "TORONTO-MONTREAL");
        Routes.AddRoute(City.Toronto, City.Pittsburgh, "grey", 2, 1, "TORONTO-PITTSBURG");
        Routes.AddRoute(City.SaulStMarie, City.Toronto, "grey", 2, 1, "SAULSTMARIE-TORONTO");
        Routes.AddRoute(City.Pittsburgh, City.Washington,"grey", 2, 1, "PITTSBURG-WASHINGTON");
        Routes.AddRoute(City.Raleigh, City.Washington, "grey", 2, 2, "RALEIGH-WASHINGTON");
        Routes.AddRoute(City.Atlanta, City.Raleigh, "grey", 2, 2, "ATLANTA-RALEIGH");
        Routes.AddRoute(City.Atlanta, City.Charleston, "grey", 2, 1, "ATLANTA-CHARLESTON");
        Routes.AddRoute(City.Nashville, City.Atlanta, "grey", 1, 1, "NASHVILLE-ATLANTA");
        Routes.AddRoute(City.SaintLouis, City.Nashville, "grey", 2, 1, "SAINTLOUIS-NASHVILLE");
        Routes.AddRoute(City.KansasCity, City.OklahomaCity, "grey",  2, 2, "KANSAS-OKLAHOMA");
        Routes.AddRoute(City.OklahomaCity, City.LittleRock, "grey", 2, 1, "OKLAHOMA-LITTLEROCK");
        Routes.AddRoute(City.LittleRock, City.SaintLouis, "grey", 2, 1,"LITTLEROCK-SAINTLOUIS");
        Routes.AddRoute(City.OklahomaCity, City.Dallas, "grey", 2, 2, "OKLAHOMA-DALLAS");
        Routes.AddRoute(City.Dallas, City.LittleRock, "grey", 2, 1, "DALLAS-LITTLEROCK");
        Routes.AddRoute(City.Houston, City.Dallas, "grey", 2, 1, "HOUSTON-DALLAS");
        Routes.AddRoute(City.Houston, City.NewOrleans,"grey",  2, 1, "HOUSTON-NEWORLEANS");
        Routes.AddRoute(City.KansasCity, City.Omaha, "grey", 1, 2, "KANSAS-OMAHA");
        Routes.AddRoute(City.Omaha, City.Duluth, "grey", 2, 2, "OMAHA-DULUTH");
        Routes.AddRoute(City.Denver, City.SantaFe, "grey", 2, 1, "DENVER-SANTAFE");
        Routes.AddRoute(City.SantaFe, City.ElPaso, "grey", 2, 1, "SANTAFE-ELPASO");
        Routes.AddRoute(City.ElPaso, City.Phoenix, "grey", 3, 1, "ELPASO-PHOENIX");
        Routes.AddRoute(City.SantaFe, City.Phoenix, "grey", 3, 1, "SANTAFE-PHOENIX");
        Routes.AddRoute(City.LosAngeles, City.LasVegas, "grey", 2, 1, "LOSANGELES-LASVEGAS");
        Routes.AddRoute(City.Seattle, City.Portland, "grey", 1, 2, "SEATTLE-PORTLAND");
        Routes.AddRoute(City.Seattle, City.Vancouver,"grey", 1, 2, "SEATTLE-VANCOUVER");
        Routes.AddRoute(City.Calgary, City.Vancouver, "grey", 3, 1, "CALGARY-VANCOUVER");
        Routes.AddRoute(City.Calgary, City.Helena, "grey", 4, 1, "CALGARY-HELENA");
        Routes.AddRoute(City.Winnipeg, City.SaulStMarie,"grey", 6, 1, "WINNIPEG-SAULSTMARIE");
        Routes.AddRoute(City.Seattle, City.Calgary, "grey", 4, 1, "SEATTLE-CALGARY");
        Routes.AddRoute(City.Phoenix, City.LosAngeles,"grey",  3, 1, "PHOENIX-LOSANGELES");
        Routes.AddRoute(City.Duluth, City.SaulStMarie, "grey", 3, 1, "DULUTH-SAULSTMARIE");

        #endregion
        
        #region BlueRoutes
        Routes.AddRoute(City.Montreal, City.NewYork, "blue", 3,1, "MONTREAL-NEWYORK");
        Routes.AddRoute(City.Atlanta, City.Miami, "blue", 4, 1, "ATLANTA-MIAMI");
        Routes.AddRoute(City.Omaha, City.Chicago, "blue", 4, 1, "OMAHA-CHICAGO");
        Routes.AddRoute(City.KansasCity, City.SaintLouis, "blue", 2, 2, "KANSAS-SAINTLOUIS");
        Routes.AddRoute(City.SantaFe, City.OklahomaCity, "blue", 3,1, "SANTAF-OKLAHOMA");
        Routes.AddRoute(City.Helena, City.Winnipeg, "blue", 4, 1, "HELENA-WINNIPEG");
        Routes.AddRoute(City.SaltLakeCity, City.Portland, "blue", 6,1, "SALTLAKE-PORTLAND");

        #endregion

        #region RedRoutes
        Routes.AddRoute(City.Boston, City.NewYork, "red", 2, 2, "BOSTON-NEWYORK");
        Routes.AddRoute(City.NewOrleans, City.Miami, "red", 6, 1, "NEWORLEANS-MIAMI");
        Routes.AddRoute(City.Duluth, City.Chicago, "red", 3, 1, "DULUTH-CHICAGO");
        Routes.AddRoute(City.ElPaso, City.Dallas, "red", 4, 1, "ELPASO-DALLAS");
        Routes.AddRoute(City.Denver, City.OklahomaCity, "red", 4, 1, "DENVER-OKLAHOMA");
        Routes.AddRoute(City.Helena, City.Omaha, "red", 5,1, "HELENA-OMAHA");
        Routes.AddRoute(City.SaltLakeCity, City.Denver, "red", 3, 2, "SALTLAKE-DENVER");

        #endregion

        #region YellowRoutes
        Routes.AddRoute(City.Boston, City.NewYork, "yellow", 2, 2, "BOSTON-NEWYORK");
        Routes.AddRoute(City.Pittsburgh, City.Nashville, "yellow", 4, 1, "PITTSBURG-NASHVILLE");
        Routes.AddRoute(City.Atlanta, City.NewOrleans, "yellow", 4, 2, "ATLANTA-NEWORLEANS");
        Routes.AddRoute(City.OklahomaCity, City.ElPaso, "yellow", 5, 1, "OKLAHOMA-ELPASO");
        Routes.AddRoute(City.SaltLakeCity, City.Denver, "yellow", 3, 2, "SALTLAKE-DENVER");
        Routes.AddRoute(City.SanFrancisco, City.LosAngeles, "yellow", 3, 2, "SANFRANCISCO-LOSANGELES");//ALSO IN PURPLE ROUTES
        Routes.AddRoute(City.Seattle, City.Helena, "yellow", 6, 1, "SEATTLE-HELENA");
        #endregion

        #region WhiteRoutes
        Routes.AddRoute(City.Pittsburgh, City.NewYork, "white", 2, 2, "PITTSBURG-NEWYORK");
        Routes.AddRoute(City.Toronto, City.Chicago, "white", 4, 1, "TORONTO-CHICAGO");
        Routes.AddRoute(City.Chicago, City.SaintLouis, "white", 2, 2,"CHICAGO-SAINTLOUIS");
        Routes.AddRoute(City.LittleRock, City.Nashville, "white", 3, 1, "LITTLEROCK-NASHVILLE");
        Routes.AddRoute(City.Phoenix, City.Denver, "white", 5, 1, "PHOENIX-DENVER");
        Routes.AddRoute(City.SanFrancisco, City.SaltLakeCity, "white", 5, 2, "SANFRANCISCO-SALTLAKE");
        Routes.AddRoute(City.Winnipeg, City.Calgary, "white", 6,1, "WINNIPEG-CALGARY");

        #endregion

        #region PurpleRoutes
        Routes.AddRoute(City.Duluth, City.Toronto, "purple", 6, 1, "DULUTH-TORONTO");
        Routes.AddRoute(City.Charleston, City.Miami, "purple", 4, 1, "CHARLESTON-MIAMI");
        Routes.AddRoute(City.KansasCity, City.SaintLouis, "purple", 2, 2, "KANSAS-SAINTLOUIS");
        Routes.AddRoute(City.Denver, City.Omaha, "purple", 4, 1, "DENVER-OMAHA");
        Routes.AddRoute(City.Helena, City.SaltLakeCity, "purple", 3, 1, "HELENA-SALTLAKE");
        Routes.AddRoute(City.Portland, City.SanFrancisco, "purple", 5, 2, "PORTLAND-SANFRANCISCO");// also in green
        Routes.AddRoute(City.SanFrancisco, City.LosAngeles, "purple", 3, 2, "SANFRANCISCO-LOSANGELES");
        #endregion

        #region GreenRoutes
        Routes.AddRoute(City.Pittsburgh, City.NewYork, "green", 2, 2, "PITTSBURGH-NEWYORK");
        Routes.AddRoute(City.Pittsburgh, City.SaintLouis, "green", 5, 1, "PITTSBURGH-SAINTLOUIS");
        Routes.AddRoute(City.Chicago, City.SaintLouis, "green", 2, 2, "CHICAGO-SAINTLOUIS");
        Routes.AddRoute(City.LittleRock, City.NewOrleans, "green", 3, 1, "LITTLEROCK-NEWORLEANS");
        Routes.AddRoute(City.Houston, City.ElPaso, "green", 6, 1, "HOUSTON-ELPASO");
        Routes.AddRoute(City.Portland, City.SanFrancisco, "green", 5, 2, "PORTLAND-SANFRANCISCO");
        Routes.AddRoute(City.Denver, City.Helena, "green", 4, 1, "DENVER-HELENA");

        #endregion

        #region BlackRoutes
        Routes.AddRoute(City.Montreal, City.SaulStMarie, "black", 5, 1, "MONTREAL-SAULSTMARIE");
        Routes.AddRoute(City.NewYork, City.Washington, "black", 2, 2, "NEWYORK-WASHINGTON");
        Routes.AddRoute(City.Chicago, City.Pittsburgh, "black", 3, 2, "CHICAGO-PITTSBURGH");
        Routes.AddRoute(City.Nashville, City.Raleigh, "black", 3, 1, "NASHVILLE-RALEIGH");
        Routes.AddRoute(City.Denver, City.KansasCity, "black", 4, 2, "DENVER-KANSAS");
        Routes.AddRoute(City.Winnipeg, City.Duluth, "black", 4, 1, "WINNIPEG-DULUTH");
        Routes.AddRoute(City.LosAngeles, City.ElPaso, "black", 6, 1, "LOSANGELES-ELPASO");
        #endregion

        #region OrangeRoutes
        Routes.AddRoute(City.NewYork, City.Washington, "orange", 2, 2, "NEWYORK-WASHINGTON");
        Routes.AddRoute(City.Chicago, City.Pittsburgh, "orange", 3, 2, "CHICAGO-PITTSBURGH");
        Routes.AddRoute(City.NewOrleans, City.Atlanta, "orange", 4, 2, "NEWORLEANS-ATLANTA");
        Routes.AddRoute(City.Denver, City.KansasCity, "orange", 4, 2, "DENVER-KANSAS");
        Routes.AddRoute(City.Duluth, City.Helena, "orange", 6, 1, "DULUTH-HELENA");
        Routes.AddRoute(City.LasVegas, City.SaltLakeCity, "orange", 3, 1, "LASVEGAS-SALTLAKE");
        Routes.AddRoute(City.SanFrancisco, City.SaltLakeCity, "orange", 5, 1, "SANFRANCISCO-SALTLAKE");
        #endregion
    }

}