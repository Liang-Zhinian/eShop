﻿using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Basket.API.IntegrationEvents.Events
{
    // Integration Events notes: 
    // An Event is “something that has happened in the past”, therefore its name has to be   
    // An Integration Event is an event that can cause side effects to other microsrvices, Bounded-Contexts or external systems.
    public class ProductPriceChangedIntegrationEvent : IntegrationEvent
    {        
        public Guid ProductId { get; private set; }

        public decimal NewPrice { get; private set; }

        public decimal OldPrice { get; private set; }

        public ProductPriceChangedIntegrationEvent(Guid productId, decimal newPrice, decimal oldPrice)
        {
            ProductId = productId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}
