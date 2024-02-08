using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Repositories;

namespace RocketseatAuction.API.Services;

public class LoggedUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoggedUser(IHttpContextAccessor httpContext)
    {
        _httpContextAccessor = httpContext;
    }

    public User User()
    {
        using var repository = new RocketseatAuctionDbContext();

        var token = TokenOnRequest();

        return repository.Users.First(user => user.Email.Equals(FromBase64String(token)));
    }

    private string TokenOnRequest()
    {
        var authentication = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        // Conta o vetor de caractere e retorna todo o restante dps do tamanho estipulado.
        // EX: authentication[7..] *Aqui pegaria todos a partir do 7 até o fim da string.*
        // EX2: authentication[..7] *Aqui pega todos do inicio até index 7 do vetor de caracter, sem incluir o 7 caracter.*
        return authentication["Bearer ".Length..];
    }

    private string FromBase64String(string base64)
    {
        var data = Convert.FromBase64String(base64);

        return System.Text.Encoding.UTF8.GetString(data);
    }
}
