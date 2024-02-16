using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Contracts.Cargoes;
using Result;

namespace CargoApp.Modules.Cargoes.Core.CargoAggregate;

public class Cargo
{
    public Guid Id { get; init; }
    public Location From { get; private set; }
    public Location To { get; private set;}
    public Company Sender { get; private set;}
    public Company Receiver { get; private set;}
    public Driver? Driver { get; private set;}
    public DateTime ExpectedDeliveryTime{ get; private set;}
    public DateTime? DeliveryDate{ get; private set;}
    public bool IsDelivered{ get; private set;}
    public bool IsLocked{ get; private set;}
    public bool IsCanceled { get; private set;}
    private readonly DateTime _createAt;

    public Cargo()
    {
    }

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
        From = from;
        To = to;
        Sender = sender;
        Receiver = receiver;
        ExpectedDeliveryTime = expectedDeliveryTime;
        DeliveryDate = deliveryDate;
        _createAt = createAt;
        Driver = driver;
        IsCanceled = false;
        IsDelivered = false;
    }

    internal static Result<Cargo> Create(
        Location? from,
        Location? to,
        Company? sender,
        Company? receiver,
        DateTime expectedDeliveryTime,
        IClock clock)
    {
        if (clock.Now() > expectedDeliveryTime)
        {
            return "Can't create cargo with expected delivery time in past";
        }

        if (from is null)
        {
            return "Source can not be null";
        }

        if (to is null)
        {
            return "Destination can not be null";
        }
        
        if (sender is null)
        {
            return "Sender can not be null";
        }
        
        if (receiver is null)
        {
            return "Receiver can not be null";
        }
        
        return new Cargo(from, to, sender, receiver, expectedDeliveryTime, null, clock.Now(), null)
        {
            Id = Guid.NewGuid()
        };
    }

    public Result.Result Update(
        Location? from,
        Location? to,
        Company? receiver,
        DateTime? expectedDeliveryTime,
        IClock clock
    )
    {
        if (clock.Now() > ExpectedDeliveryTime)
        {
            return Result.Result.Fail("Can't update cargo after expected delivery time");
        }

        From = from ?? From;
        To = to ?? To;
        Receiver = receiver ?? Receiver;
        ExpectedDeliveryTime = expectedDeliveryTime ?? ExpectedDeliveryTime;

        return Result.Result.Success();
    }

    public void Cancel()
    {
        IsCanceled = true;
    }

    public void Lock()
    {
        IsLocked = true;
    }

    public void Deliver(DateTime deliveryDate)
    {
        IsDelivered = true;
        DeliveryDate = deliveryDate;
    }

    public CargoDto CreateDto()
    {
        return  new CargoDto(
            From.CreateDto(),
            To.CreateDto(),
            Sender.CreateDto(),
            Driver?.CreateDto(),
            _createAt,
            ExpectedDeliveryTime,
            IsDelivered,
            IsCanceled,
            IsLocked,
            Id
        );
    }
}