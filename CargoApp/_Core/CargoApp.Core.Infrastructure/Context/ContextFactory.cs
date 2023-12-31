﻿using CargoApp.Core.Abstraction.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CargoApp.Core.Infrastructure.Context;

internal class ContextFactory : IContextFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ContextFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public IContext Create()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        return httpContext is null ? Context.Empty() : new Context(httpContext);
    }
}