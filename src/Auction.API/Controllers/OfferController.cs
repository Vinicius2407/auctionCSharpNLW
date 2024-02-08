using Microsoft.AspNetCore.Mvc;
using RocketseatAuction.API.Comunication.Requests;
using RocketseatAuction.API.Filter;
using RocketseatAuction.API.UseCases.Offers.CreateOffer;

namespace RocketseatAuction.API.Controllers;

[ServiceFilter(typeof(AuthenticationUserAttribute))]
public class OfferController : RocketseatAuctionBaseController
{
    [HttpPost]
    [Route("{itemId}")]
    public IActionResult CreateOffer([FromRoute]int itemId, [FromBody] RequestCreateOfferJson request, [FromServices] CreateOfferUseCase useCase)
    {
        var id = useCase.Execute(itemId, request);
        return Created(string.Empty, id);
    }
}
