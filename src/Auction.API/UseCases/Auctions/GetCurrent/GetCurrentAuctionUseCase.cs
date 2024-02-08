using Microsoft.EntityFrameworkCore;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Repositories;

namespace RocketseatAuction.API.UseCases.Auctions.GetCurrent;

public class GetCurrentAuctionUseCase
{
    public Auction? Execute()
    {
        using var repository = new RocketseatAuctionDbContext();

        return repository
                .Auctions
                .Include(x => x.Items)
                .FirstOrDefault();
    }
}
