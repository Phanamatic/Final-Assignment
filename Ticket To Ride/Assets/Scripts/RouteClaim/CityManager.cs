using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager
{
    City EnumObj;
    /*public enum CitiesEnum
    {
        Vancouver,
        Seattle,
        Portland,
        Calgary,
        SaltLakeCity,
        SanFrancisco,
        Helena,
        Winnipeg,
        LasVegas,
        LosAngeles,
        Phoenix,
        SantaFe,
        Denver,
        Duluth,
        ElPaso,
        OklahomaCity,
        Omaha,
        KansasCity,
        Dallas,
        Houston,
        LittleRock,
        SaintLouis,
        Chicago,
        NewOrleans,
        Nashville,
        Atlanta,
        Pittsburgh,
        Toronto,
        Montreal,
        SaulStMarie,
        Boston,
        NewYork,
        Washington,
        Raleigh,
        Charleston,
        Miami
    }*/

    List<City> cities = new List<City>();
    public List<City> ListOfCities()
    {
        // for(int i  = 0; i< CitiesEnum.GetNames(typeof(CitiesEnum)).Length; i++)

        cities.Add(City.Atlanta);
        cities.Add(City.Boston);
        cities.Add(City.Calgary);
        cities.Add(City.Charleston);
        cities.Add(City.Chicago);
        cities.Add(City.Dallas);
        cities.Add(City.Denver);
        cities.Add(City.Duluth);
        cities.Add(City.ElPaso);
        cities.Add(City.Helena);
        cities.Add(City.Houston);
        cities.Add(City.KansasCity);
        cities.Add(City.LasVegas);
        cities.Add(City.LittleRock);
        cities.Add(City.LosAngeles);
        cities.Add(City.Miami);
        cities.Add(City.Montreal);
        cities.Add(City.Nashville);
        cities.Add(City.NewOrleans);
        cities.Add(City.NewYork);
        cities.Add(City.OklahomaCity);
        cities.Add(City.Omaha);
        cities.Add(City.Phoenix);
        cities.Add(City.Pittsburgh);
        cities.Add(City.Portland);
        cities.Add(City.Raleigh);
        cities.Add(City.SaintLouis);
        cities.Add(City.SaltLakeCity);
        cities.Add(City.SanFrancisco);
        cities.Add(City.SantaFe);
        cities.Add(City.SaulStMarie);
        cities.Add(City.Seattle);
        cities.Add(City.Toronto);
        cities.Add(City.Vancouver);
        cities.Add(City.Washington);
        cities.Add(City.Winnipeg);
        return cities;

    }


}

