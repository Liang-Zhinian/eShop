﻿global using Autofac.Extensions.DependencyInjection;
global using Autofac;
global using Azure.Core;
global using Azure.Identity;
global using HealthChecks.UI.Client;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore;
global using Azure.Messaging.ServiceBus;
global using Eva.eShop.BuildingBlocks.EventBus.Abstractions;
global using Eva.eShop.BuildingBlocks.EventBus.Events;
global using Eva.eShop.BuildingBlocks.EventBus;
global using Eva.eShop.BuildingBlocks.EventBusRabbitMQ;
global using Eva.eShop.BuildingBlocks.EventBusServiceBus;
global using Eva.eShop.Payment.API.IntegrationEvents.Events;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Eva.eShop.Payment.API.IntegrationEvents.EventHandling;
global using Eva.eShop.Payment.API;
global using RabbitMQ.Client;
global using Serilog.Context;
global using Serilog;
global using System.Threading.Tasks;
global using System;
global using System.IO;
global using Microsoft.AspNetCore.Hosting;