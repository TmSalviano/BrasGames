// Services/ControllerService.cs
using System.Text.RegularExpressions;
using BrasGames.Data;
using Microsoft.EntityFrameworkCore;
using BrasGames.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using BrasGames.Model.ServiceModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using BrasGames.Model.DTO.ServiceDTO;
using System.Runtime.InteropServices;

//There is a possibility this could be a static class. Don't forget about static constructors.

public class BasicRESTService<T> where T : class
{
    private readonly ServiceDbContext _db;
    private readonly Type _tType = typeof(T);
    private readonly Type[] _serviceModelTypes = [typeof(ControllerDTO), typeof(GameDTO), typeof(ConsoleDTO)];

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
        if (_tType == _serviceModelTypes[1]) {
            var models = await _db.Games.ToListAsync();
            var result = models.Select(model => new GameDTO(model)).ToList();
            return TypedResults.Ok(result);
        }
        if (_tType == _serviceModelTypes[2]) {
            var models = await _db.Consoles.ToListAsync();
            var result = models.Select(model => new ConsoleDTO(model)).ToList();
            return TypedResults.Ok(result);        
        }
        
        return TypedResults.Problem("typeof generic class parameter is not equal to typeof any service model");
    }

    public async Task<IResult> GetId(int id)
    {

        if (_tType == _serviceModelTypes[0]) {
            var searchResult = await _db.Controllers.FindAsync(id);
            if (searchResult is null)
                return TypedResults.NotFound();
            return TypedResults.Ok(new ControllerDTO(searchResult));
        }
        if (_tType == _serviceModelTypes[1]) {
            var searchResult = await _db.Games.FindAsync(id);
            if (searchResult is null)
                return TypedResults.NotFound();
            return TypedResults.Ok(new GameDTO(searchResult));
        }
        if (_tType == _serviceModelTypes[2]) {
            var searchResult = await _db.Consoles.FindAsync(id);
            if (searchResult is null)
                return TypedResults.NotFound();
            return TypedResults.Ok(new ConsoleDTO(searchResult));   
        }
        
        return TypedResults.Problem("typeof generic class parameter is not equal to typeof any service model");
    }

    public async Task<IResult> PostModel(T dtoModel)
    {
        if (dtoModel is ControllerDTO controller) {
            await _db.Controllers.AddAsync(
                new Controller() {
                    Id = controller.Id,
                    Name = controller.Name,
                    Type = controller.Type,
                    Price = controller.Price,
                    Year= controller.Year,
                }
            );
            await _db.SaveChangesAsync();
            return TypedResults.Created("/service/controller/" + controller.Id, controller);
        }

        if (dtoModel is GameDTO game) {
            await _db.Games.AddAsync(new Game() {
                Id = game.Id,
                AgeRestriction = game.AgeRestriction,
                Genre = game.Genre,
                Name = game.Name,
                Price = game.Price,
            });
            await _db.SaveChangesAsync();
            return TypedResults.Created("/service/game/" + game.Id, game);
        }

        if (dtoModel is ConsoleDTO console) {
            await _db.Consoles.AddAsync(
                new BrasGames.Model.ServiceModels.Console() {
                    Id = console.Id,
                    Name = console.Name,
                    Type = console.Type,
                    ReleaseYear = console.ReleaseYear,
                    Price = console.Price,
                }
            );
            await _db.SaveChangesAsync();
            return TypedResults.Created("/service/console/" + console.Id, console);
        }

        return TypedResults.Problem("typeof generic userModel is not equal to typeof any service model");
    }

    public async Task<IResult> PutModel(int id, T dtoModel)
    {
        if (dtoModel is ControllerDTO controllerDTO) {
            var result = await _db.Controllers.FindAsync(id);
            if (result is null)
                return TypedResults.NotFound();

            result.Type = controllerDTO.Type;
            result.Name = controllerDTO.Name;
            result.Year = controllerDTO.Year;
            result.Price = controllerDTO.Price;

            await _db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        if (dtoModel is GameDTO gameDTO) {
           var result = await _db.Games.FindAsync(id);
            if (result is null)
                return TypedResults.NotFound();

            result.Genre = gameDTO.Genre;
            result.Name = gameDTO.Name;
            result.AgeRestriction = gameDTO.AgeRestriction;
            result.Price = gameDTO.Price;

            await _db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        if (dtoModel is ConsoleDTO consoleDTO) {
            var result = await _db.Consoles.FindAsync(id);
            if (result is null)
                return TypedResults.NotFound();

            result.Type = consoleDTO.Type;
            result.Name = consoleDTO.Name; 
            result.Price = consoleDTO.Price;
            result.ReleaseYear = consoleDTO.ReleaseYear;

            await _db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        return TypedResults.Problem("typeof generic userModel is not equal to typeof any service model");
    }

    //DTO's in this method is useless
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

    //DTO's here are useless
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




