using UnityEngine;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private Location[] _locationsList;

    public void CheckAllLocationOpen(int currentHour)
    {
        foreach (Location location in _locationsList)
        {
            location.CheckOpenStatus(currentHour);
        }
    }

    public void CheckEnoughMoneyForLocations()
    {
        foreach (Location location in _locationsList)
        {
            location.CheckEnoughMoney();
        }
    }
}
