using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.DriverAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using CargoApp.Modules.Contracts.Cargoes;
using Result;

namespace CargoApp.Modules.Cargoes.Core.CargoAggregate;

public class Cargo
{
    public Guid Id { get; init; }
    private Location _from;
    private Location _to;
    private Company _sender;
    private Company _receiver;
    private Driver? _driver;
    private DateTime _expectedDeliveryTime;
    private DateTime? _deliveryDate;
    private bool _isDelivered;
    private bool _isCanceled;
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
        _from = from;
        _to = to;
        _sender = sender;
        _receiver = receiver;
        _expectedDeliveryTime = expectedDeliveryTime;
        _deliveryDate = deliveryDate;
        _createAt = createAt;
        _driver = driver;
        _isCanceled = false;
        _isDelivered = false;
    }

    public static Result<Cargo> Create(
        Location from,
        Location to,
        Company sender,
        Company receiver,
        DateTime expectedDeliveryTime,
        IClock clock)
    {
        if (clock.Now() > expectedDeliveryTime)
        {
            return "Can't create cargo with expected delivery time in past";
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
        if (clock.Now() > _expectedDeliveryTime)
        {
            return Result.Result.Fail("Can't update cargo after expected delivery time");
        }

        _from = from ?? _from;
        _to = to ?? _to;
        _receiver = receiver ?? _receiver;
        _expectedDeliveryTime = expectedDeliveryTime ?? _expectedDeliveryTime;

        return Result.Result.Success();
    }

    public void Cancel()
    {
        _isCanceled = true;
    }

    public void Deliver(DateTime deliveryDate)
    {
        _isDelivered = true;
        _deliveryDate = deliveryDate;
    }

    public CargoDto CreateDto()
    {
        return  new CargoDto(
            _from.CreateDto(),
            _to.CreateDto(),
            _sender.CreateDto(),
            _driver?.CreateDto(),
            _createAt,
            _expectedDeliveryTime,
            _isDelivered,
            _isCanceled,
            Id
        );
    }
}