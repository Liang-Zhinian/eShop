﻿using Eva.eShop.Services.Catalog.API.IntegrationEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eva.eShop.Services.Catalog.API;
using Eva.eShop.Services.Catalog.API.Controllers;
using Eva.eShop.Services.Catalog.API.Infrastructure;
using Eva.eShop.Services.Catalog.API.Model;
using Eva.eShop.Services.Catalog.API.ViewModel;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Catalog.Application;

public class CatalogControllerTest
{
    private readonly DbContextOptions<CatalogContext> _dbOptions;

    public CatalogControllerTest()
    {
        _dbOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "in-memory")
            .Options;

        using var dbContext = new CatalogContext(_dbOptions);
        dbContext.AddRange(GetFakeCatalog());
        dbContext.SaveChanges();
    }

    [Fact]
    public async Task Get_catalog_items_success()
    {
        //Arrange
        var brandFilterApplied = 1;
        var typesFilterApplied = 2;
        var pageSize = 4;
        var pageIndex = 1;

        var expectedItemsInPage = 2;
        var expectedTotalItems = 6;

        var catalogContext = new CatalogContext(_dbOptions);
        var catalogSettings = new TestCatalogSettings();

        var integrationServicesMock = new Mock<ICatalogIntegrationEventService>();

        //Act
        var orderController = new CatalogController(catalogContext, catalogSettings, integrationServicesMock.Object);
        var actionResult = await orderController.ItemsByTypeIdAndBrandIdAsync(typesFilterApplied, brandFilterApplied, pageSize, pageIndex);

        //Assert
        Assert.IsType<ActionResult<PaginatedItemsViewModel<CatalogItem>>>(actionResult);
        var page = Assert.IsAssignableFrom<PaginatedItemsViewModel<CatalogItem>>(actionResult.Value);
        Assert.Equal(expectedTotalItems, page.Count);
        Assert.Equal(pageIndex, page.PageIndex);
        Assert.Equal(pageSize, page.PageSize);
        Assert.Equal(expectedItemsInPage, page.Data.Count());
    }

    private List<CatalogItem> GetFakeCatalog()
    {
        return new List<CatalogItem>()
        {
            new()
            {
                Id = 1,
                Name = "fakeItemA",
                CatalogTypeId = 2,
                CatalogBrandId = 1,
                PictureFileName = "fakeItemA.png"
            },
            new()
            {
                Id = 2,
                Name = "fakeItemB",
                CatalogTypeId = 2,
                CatalogBrandId = 1,
                PictureFileName = "fakeItemB.png"
            },
            new()
            {
                Id = 3,
                Name = "fakeItemC",
                CatalogTypeId = 2,
                CatalogBrandId = 1,
                PictureFileName = "fakeItemC.png"
            },
            new()
            {
                Id = 4,
                Name = "fakeItemD",
                CatalogTypeId = 2,
                CatalogBrandId = 1,
                PictureFileName = "fakeItemD.png"
            },
            new()
            {
                Id = 5,
                Name = "fakeItemE",
                CatalogTypeId = 2,
                CatalogBrandId = 1,
                PictureFileName = "fakeItemE.png"
            },
            new()
            {
                Id = 6,
                Name = "fakeItemF",
                CatalogTypeId = 2,
                CatalogBrandId = 1,
                PictureFileName = "fakeItemF.png"
            }
        };
    }
}

public class TestCatalogSettings : IOptionsSnapshot<CatalogSettings>
{
    public CatalogSettings Value => new()
    {
        PicBaseUrl = "http://image-server.com/",
        AzureStorageEnabled = true
    };

    public CatalogSettings Get(string name) => Value;
}
