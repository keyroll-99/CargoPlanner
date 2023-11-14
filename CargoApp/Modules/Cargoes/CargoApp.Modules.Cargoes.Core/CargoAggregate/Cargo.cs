﻿using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.DriverAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;

namespace CargoApp.Modules.Cargoes.Core.CargoAggregate;

public class Cargo
{
    public Guid Id { get; init; }
    private Location _from;
    private Location _to;
    private Company _sender;
    private Company _receiver;
    private DateTime _expectedDeliveryTime;
    private DateTime? _deliveryDate;
    private readonly DateTime _createAt;
    private DriverAggregate.Driver? _driver;


    private Cargo(
        Location from,
        Location to,
        Company sender,
        Company receiver,
        DateTime expectedDeliveryTime,
        DateTime? deliveryDate,
        DateTime createAt,
        Driver? driver)
    {
        _from = from;
        _to = to;
        _sender = sender;
        _receiver = receiver;
        _expectedDeliveryTime = expectedDeliveryTime;
        _deliveryDate = deliveryDate;
        _createAt = createAt;
        _driver = driver;
    }

    public static Cargo Create(
        Location from,
        Location to,
        Company sender, 
        Company receiver, 
        DateTime expectedDeliveryTime,
        DateTime createAt)
    {
        return new Cargo(from, to, sender, receiver, expectedDeliveryTime, null, createAt, null)
        {
            Id = Guid.NewGuid()
        };
    }
}