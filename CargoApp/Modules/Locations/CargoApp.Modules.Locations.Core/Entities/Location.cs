﻿using CargoApp.Core.ShareCore.Entites;

namespace CargoApp.Modules.Locations.Core.Entities;

public class Location : BaseEntity
{
    public double Lat { get; private set; }
    public double Lon { get; private set; }
    public string Name { get; private set; }
    public string DisplayName { get; private set; }
    public long OsmId { get; private set;}
    public Guid AddressId { get; private set; }
    public Address? Address { get; private set; }

    public Location()
    {
    }

    public Location(Guid id, DateTime createAt, double lat, double lon, string name, string displayName, long osmId, Address address) : base(id, createAt)
    {
        Lat = lat;
        Lon = lon;
        Name = name;
        DisplayName = displayName;
        OsmId = osmId;
        Address = address;
    }
    public Location(double lat, double lon, string name, string displayName, long osmId, Address address)
    {
        Lat = lat;
        Lon = lon;
        Name = name;
        DisplayName = displayName;
        OsmId = osmId;
        Address = address;
    }

    public Location(Guid id, DateTime createAt, double lat, double lon, string name, string displayName, long osmId, Guid addressId) : base(id, createAt)
    {
        Lat = lat;
        Lon = lon;
        Name = name;
        DisplayName = displayName;
        OsmId = osmId;
        AddressId = addressId;
    }

    public Location(double lat, double lon, string name, string displayName, long osmId, Guid addressId, Address? address)
    {
        Lat = lat;
        Lon = lon;
        Name = name;
        DisplayName = displayName;
        OsmId = osmId;
        AddressId = addressId;
        Address = address;
    }
}