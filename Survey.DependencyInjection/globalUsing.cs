﻿global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Survey.Core.AutoMapper;
global using Survey.Core.IRepositories.Generic;
global using Survey.DataAccess.Context;
global using Survey.DataAccess.Repositories.Generic;
global using Survey.DataAccess.Seed;
global using Survey.Business.Services;
global using Survey.Core.IServices;
global using Survey.Core.IUnit;
global using Survey.DataAccess.Unit;
global using Survey.DataAccess.Identity.IdentiryContext;
global using Survey.Core.IServices.Authentication;
global using Survey.Business.Services.Authentication;
global using Survey.Business.Services.Logging;
global using Survey.Core.Logging;
global using Serilog;
global using Survey.Business.Services.Caching;
global using Survey.Core.IServices.caching;
global using Microsoft.AspNetCore.Identity;
global using MailKit;
global using Microsoft.AspNetCore.Identity.UI.Services;
global using Survey.Business.Services.Email;
global using Survey.Business.Services.User;
global using Survey.Core.IServices.User;
global using Survey.Business.Services.Role;
global using Survey.Core.IServices.Role;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.RateLimiting;
global using StackExchange.Redis;
global using System.Threading.RateLimiting;
global using Microsoft.AspNetCore.Http;
global using Survey.Core.constants;
