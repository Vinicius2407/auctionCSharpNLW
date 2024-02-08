using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RocketseatAuction.API.Repositories;

namespace RocketseatAuction.API.Filter;

public class AuthenticationUserAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context.HttpContext);

            using var repository = new RocketseatAuctionDbContext();

            var exist = repository.Users.Any(user => user.Email.Equals(FromBase64String(token)));

            if (!exist)
            {
                context.Result = new UnauthorizedObjectResult("E-mail not valid");
            }
        }
        catch (Exception ex)
        {
            context.Result = new UnauthorizedObjectResult(ex.Message);

        }
    }

    private string TokenOnRequest(HttpContext context)
    {
        var authentication = context.Request.Headers.Authorization.ToString();

        if(string.IsNullOrEmpty(authentication))
        {
            throw new Exception("Token is missing.");
        }

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
