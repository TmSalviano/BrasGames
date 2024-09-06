// Services/ControllerService.cs
using System.Text.RegularExpressions;
using BrasGames.Data;
using Microsoft.EntityFrameworkCore;
using BrasGames.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using BrasGames.Model.ServiceModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using BrasGames.Model.DTO.ServiceDTO;

//There is a possibility this could be a static class. Don't forget about static constructors.

public class BasicRESTService<T> where T : class
{
    private readonly ServiceDbContext _db;
    private readonly Type _tType = typeof(T);
    private readonly Type[] _serviceModelTypes = [typeof(Controller), typeof(Game), typeof(BrasGames.Model.ServiceModels.Console)];

    public BasicRESTService(ServiceDbContext db)
    {
        _db = db;
    }

    public async Task<IResult> GetAll() {
        if (_tType == _serviceModelTypes[0])
        {
            var models = await _db.Controllers.ToListAsync();
            var result = models.Select(model => new ControllerDTO(model)).ToList();
            return TypedResults.Ok(result);
        }
        if (_tType == _serviceModelTypes[1])
            return TypedResults.Ok(await _db.Games.ToListAsync());
        if (_tType == _serviceModelTypes[2])
            return TypedResults.Ok(await _db.Consoles.ToListAsync());
        
        return TypedResults.Problem("typeof generic class parameter is not equal to typeof any service model");
    }

    public async Task<IResult> GetId(int id)
    {

        if (_tType == _serviceModelTypes[0]) {
            var searchResult = await _db.Controllers.FindAsync(id);
            if (searchResult is null)
                return TypedResults.NotFound();
            return TypedResults.Ok(searchResult);
        }
        if (_tType == _serviceModelTypes[1]) {
            var searchResult = await _db.Games.FindAsync(id);
            if (searchResult is null)
                return TypedResults.NotFound();
            return TypedResults.Ok(searchResult);
        }
        if (_tType == _serviceModelTypes[2]) {
            var searchResult = await _db.Consoles.FindAsync(id);
            if (searchResult is null)
                return TypedResults.NotFound();
            return TypedResults.Ok(searchResult);
        }
        
        return TypedResults.Problem("typeof generic class parameter is not equal to typeof any service model");
    }

    public async Task<IResult> PostModel(T userModel)
    {
        if (userModel is Controller controller) {
            await _db.Controllers.AddAsync(controller);
            await _db.SaveChangesAsync();
            return TypedResults.Created("/service/controller/" + controller.Id, controller);
        }

        if (userModel is Game game) {
            await _db.Games.AddAsync(game);
            await _db.SaveChangesAsync();
            return TypedResults.Created("/service/game/" + game.Id, game);
        }

        if (userModel is BrasGames.Model.ServiceModels.Console console) {
            await _db.Consoles.AddAsync(console);
            await _db.SaveChangesAsync();
            return TypedResults.Created("/service/console/" + console.Id, console);
        }

        return TypedResults.Problem("typeof generic userModel is not equal to typeof any service model");
    }

    public async Task<IResult> PutModel(int id, T userModel)
    {
        if (userModel is Controller controller) {
            var result = await _db.Controllers.FindAsync(id);
            if (result is null)
                return TypedResults.NotFound();

            result.Type = controller.Type;
            result.Name = controller.Name;
            result.Year = controller.Year;
            result.Price = controller.Price;

            await _db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        if (userModel is Game game) {
           var result = await _db.Games.FindAsync(id);
            if (result is null)
                return TypedResults.NotFound();

            result.Genre = game.Genre;
            result.Name = game.Name;
            result.AgeRestriction = game.AgeRestriction;
            result.Price = game.Price;

            await _db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        if (userModel is BrasGames.Model.ServiceModels.Console console) {
            var result = await _db.Consoles.FindAsync(id);
            if (result is null)
                return TypedResults.NotFound();

            result.Type = console.Type;
            result.Name = console.Name; 
            result.Price = console.Price;
            result.ReleaseYear = console.ReleaseYear;

            await _db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        return TypedResults.Problem("typeof generic userModel is not equal to typeof any service model");
    }

    public async Task<IResult> DeleteModel(int id)
    {

        if (_tType == _serviceModelTypes[0]) {
            var result = await _db.Controllers.FindAsync(id);
            if (result is null)
                return TypedResults.NotFound();
            
            _db.Controllers.Remove(result);
            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        if (_tType == _serviceModelTypes[1]) {
            var result = await _db.Games.FindAsync(id);
            if (result is null)
                return TypedResults.NotFound();
            
            _db.Games.Remove(result);
            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        if (_tType == _serviceModelTypes[2]) {
            var result = await _db.Consoles.FindAsync(id);
            if (result is null)
                return TypedResults.NotFound();
            
            _db.Consoles.Remove(result);
            await _db.SaveChangesAsync();
            return TypedResults.NoContent(); 
        }

        return TypedResults.Problem("typeof generic class parameter is not equal to typeof any service model");
    }

    public async Task<IResult> DeleteAllCModels()
    {

        if (_tType == _serviceModelTypes[0]) {
            var result = await _db.Controllers.ToListAsync();
            if (result is null)
                return TypedResults.NotFound();
            
            foreach (var controller in result) {
                _db.Controllers.Remove(controller);
            }
            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }
        if (_tType == _serviceModelTypes[1]) {
            var result = await _db.Games.ToListAsync();
            if (result is null)
                return TypedResults.NotFound();
            
            foreach (var game in result) {
                _db.Games.Remove(game);
            }
            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }
        if (_tType == _serviceModelTypes[2]) {
            var result = await _db.Consoles.ToListAsync();
            if (result is null)
                return TypedResults.NotFound();
            
            foreach (var console in result) {
                _db.Consoles.Remove(console);
            }
            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }
        
        return TypedResults.Problem("typeof generic class parameter is not equal to typeof any service model");

    }
}




