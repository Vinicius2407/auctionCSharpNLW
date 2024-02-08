using RocketseatAuction.API.Comunication.Requests;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Repositories;
using RocketseatAuction.API.Services;

namespace RocketseatAuction.API.UseCases.Offers.CreateOffer;

public class CreateOfferUseCase
{
    private readonly LoggedUser _loggedUser;
    public CreateOfferUseCase(LoggedUser loggedUser) => _loggedUser = loggedUser;

    public int Execute(int itemId, RequestCreateOfferJson request)
    {
        using var repository = new RocketseatAuctionDbContext();

        var user = _loggedUser.User();

        var offer = new Offer
        {
            CreatedOn = DateTime.UtcNow,
            ItemId = itemId,
            UserId = user.Id,
            Price = request.Price
        };

        repository.Offers.Add(offer);

        repository.SaveChanges();

        return offer.Id;
    }
}
