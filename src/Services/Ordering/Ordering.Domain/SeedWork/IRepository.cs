﻿namespace Eva.eShop.Services.Ordering.Domain.Seedwork;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
