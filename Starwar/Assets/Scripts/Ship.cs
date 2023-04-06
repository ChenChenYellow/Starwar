﻿using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public enum Belong
    {
        Red, Blue
    }

    public bool IsSpotted;
    // After a ship disappear from field of view, it will still be spotted for x seconds.
    // Then it goes unspotted.
    public float SpotTime = 3.0f;
    public Belong ShipBelong;
    public bool IsCargoShip;

    private float spotTimeCountDown = 0;
    private void Start()
    {
        RegisterShip(this);
    }
    public void Spotted()
    {
        IsSpotted = true;
        spotTimeCountDown = SpotTime;
    }
    public void Unspotted()
    {
        IsSpotted = false;
    }

    private void FixedUpdate()
    {
        if (IsSpotted && spotTimeCountDown <= 0)
        { Unspotted(); }
        else
        { spotTimeCountDown -= Time.fixedDeltaTime; }
    }

    public static List<Ship> BlueShips = new List<Ship>();
    public static List<Ship> RedShips = new List<Ship>();

    public static List<Ship> Ships(Belong shipBelong)
    {
        switch (shipBelong)
        {
            case Belong.Red:
                return RedShips;
            case Belong.Blue:
                return BlueShips;
            default:
                return null;
        }
    }

    public static void RegisterShip(Ship ship)
    {
        Ships(ship.ShipBelong).Add(ship);
    }
    public static void UnregisterShip(Ship ship)
    {
        Ships(ship.ShipBelong).Remove(ship);
        Debug.Log(Ships(ship.ShipBelong).Count);
    }
}
