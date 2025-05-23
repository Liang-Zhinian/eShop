﻿namespace Eva.eShop.Mobile.Shopping.HttpAggregator.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly ICatalogService _catalog;
    private readonly IBasketService _basket;

    public BasketController(ICatalogService catalogService, IBasketService basketService)
    {
        _catalog = catalogService;
        _basket = basketService;
    }

    [HttpPost]
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BasketData), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BasketData>> UpdateAllBasketAsync([FromBody] UpdateBasketRequest data)
    {
        if (data.Items == null || !data.Items.Any())
        {
            return BadRequest("Need to pass at least one basket line");
        }

        // Retrieve the current basket
        var basket = await _basket.GetByIdAsync(data.BuyerId) ?? new BasketData(data.BuyerId);
        var catalogItems = await _catalog.GetCatalogItemsAsync(data.Items.Select(x => x.ProductId));
        // group by product id to avoid duplicates
        var itemsCalculated = data
                .Items
                .GroupBy(x => x.ProductId, x => x, (k, i) => new { productId = k, items = i })
                .Select(groupedItem =>
                {
                    var item = groupedItem.items.First();
                    item.Quantity = groupedItem.items.Sum(i => i.Quantity);
                    return item;
                });

        foreach (var bitem in itemsCalculated)
        {
            var catalogItem = catalogItems.SingleOrDefault(ci => ci.Id == bitem.ProductId);
            if (catalogItem == null)
            {
                return BadRequest($"Basket refers to a non-existing catalog item ({bitem.ProductId})");
            }

            var itemInBasket = basket.Items.FirstOrDefault(x => x.ProductId == bitem.ProductId);
            if (itemInBasket == null)
            {
                basket.Items.Add(new BasketDataItem()
                {
                    Id = bitem.Id,
                    ProductId = catalogItem.Id,
                    ProductName = catalogItem.Name,
                    PictureUrl = catalogItem.PictureUri,
                    UnitPrice = catalogItem.Price,
                    Quantity = bitem.Quantity
                });
            }
            else
            {
                itemInBasket.Quantity = bitem.Quantity;
            }
        }

        await _basket.UpdateAsync(basket);

        return basket;
    }

    [HttpPut]
    [Route("items")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BasketData), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BasketData>> UpdateQuantitiesAsync([FromBody] UpdateBasketItemsRequest data)
    {
        if (!data.Updates.Any())
        {
            return BadRequest("No updates sent");
        }

        // Retrieve the current basket
        var currentBasket = await _basket.GetByIdAsync(data.BasketId);
        if (currentBasket == null)
        {
            return BadRequest($"Basket with id {data.BasketId} not found.");
        }

        // Update with new quantities
        foreach (var update in data.Updates)
        {
            var basketItem = currentBasket.Items.SingleOrDefault(bitem => bitem.Id == update.BasketItemId);

            if (basketItem == null)
            {
                return BadRequest($"Basket item with id {update.BasketItemId} not found");
            }

            basketItem.Quantity = update.NewQty;
        }

        // Save the updated basket
        await _basket.UpdateAsync(currentBasket);

        return currentBasket;
    }

    [HttpPost]
    [Route("items")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> AddBasketItemAsync([FromBody] AddBasketItemRequest data)
    {
        if (data == null || data.Quantity == 0)
        {
            return BadRequest("Invalid payload");
        }

        // Step 1: Get the item from catalog
        var item = await _catalog.GetCatalogItemAsync(data.CatalogItemId);

        //item.PictureUri = 

        // Step 2: Get current basket status
        var currentBasket = (await _basket.GetByIdAsync(data.BasketId)) ?? new BasketData(data.BasketId);
        // Step 3: Merge current status with new product
        currentBasket.Items.Add(new BasketDataItem()
        {
            UnitPrice = item.Price,
            PictureUrl = item.PictureUri,
            ProductId = item.Id,
            ProductName = item.Name,
            Quantity = data.Quantity,
            Id = Guid.NewGuid().ToString()
        });

        // Step 4: Update basket
        await _basket.UpdateAsync(currentBasket);

        return Ok();
    }
}
