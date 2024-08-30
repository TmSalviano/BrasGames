// Services/ControllerService.cs
using System.Text.RegularExpressions;
using BrasGames.Data;
using Microsoft.EntityFrameworkCore;
using BrasGames.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using BrasGames.Model.ServiceModels;

public class BasicRESTService<T> where T : new()
{
    private readonly ServiceDbContext _db;
    private readonly T modelType = new T();

    public BasicRESTService(ServiceDbContext db)
    {
        _db = db;
    }

    public async Task<IResult> GetControllers()
    {
        switch (modelType)
         {
            case BrasGames.Model.ServiceModels.Console console:
                return TypedResults.Ok(await _db.Consoles.ToListAsync());
            case Controller controller:
                return TypedResults.Ok(await _db.Controllers.ToListAsync());
            case Game game:
                return TypedResults.Ok(await _db.Games.ToListAsync());
            case Order order:
                return TypedResults.Ok(await _db.Orders.ToListAsync());
            default:
                return TypedResults.Problem("The generic type cannot be converted to one of the ServiceModels");
        };
    }

    public async Task<IResult> GetController(int id)
    {
        var searchResult = await _db.Controllers.FindAsync(id);
        if (searchResult is null)
            return TypedResults.NotFound();
        return TypedResults.Ok(searchResult);
    }

    public async Task<IResult> PostController(Controller userController)
    {
        await _db.Controllers.AddAsync(userController);
        await _db.SaveChangesAsync();
        return TypedResults.Created("/service/controller/" + userController.Id, userController);
    }

    public async Task<IResult> PutController(int id, Controller userController)
    {
        var result = await _db.Controllers.FindAsync(id);
        if (result is null)
            return TypedResults.NotFound();

        result.Type = userController.Type;
        result.Name = userController.Name;
        result.Year = userController.Year;
        result.Price = userController.Price;

        await _db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    public async Task<IResult> DeleteController(int id)
    {
        var result = await _db.Controllers.FindAsync(id);
        if (result is null)
            return TypedResults.NotFound();
        
        _db.Controllers.Remove(result);
        await _db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    public async Task<IResult> DeleteAllControllers()
    {
        var result = await _db.Controllers.ToListAsync();
        if (result.Count == 0)
            return TypedResults.NotFound();
        
        _db.Controllers.RemoveRange(result);
        await _db.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}
