﻿using CargoApp.Modules.Contracts.Users.DTO;
using CargoApp.Modules.Users.Core.Entities;

namespace CargoApp.Modules.Users.Core.Mappers;

public static class UserMapper
{
    public static UserDto AsUserDto(this User model) => new(model.Id, model.Email, model.IsActive, model.PermissionMask);
}