﻿using CargoApp.Core.Infrastructure.CQRS.Request;
using CargoApp.Modules.Locations.Application.DTO;


namespace CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;

public record AddLocationCommand(LocationDto Location) : IRequest;