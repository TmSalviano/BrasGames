using BrasGames.Data;
using BrasGames.Model.DTO.BusinessDTO;
using BrasGames.Model.DTO.ServiceDTO;
using BrasGames.Model.ServiceModels;
using Microsoft.EntityFrameworkCore;

public class ComplexRESTService {
    private readonly ServiceDbContext _serviceDb;
    private readonly BusinessDbContext _businessDb;

    public ComplexRESTService(ServiceDbContext serviceDb, BusinessDbContext businessDb) {
        _serviceDb = serviceDb;
        _businessDb = businessDb;
    }

    //Service Models Complex Query Methods
    // Console
    public async Task<IResult> GetFilteredConsoles( 
        string? nameSearch = null, string? typeSearch = null, 
        DateTime? releaseYearSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {

        var collection = _serviceDb.Consoles.AsQueryable();
        
        if (nameSearch != null) {
            collection = collection.Where(model => model.Name.Contains(nameSearch));
        }
        if (typeSearch != null) {
            collection = collection.Where(model => model.Type.Contains(typeSearch));
        }
        if (releaseYearSearch != null) {
            collection = collection.Where(model => model.ReleaseYear == releaseYearSearch);
        }
        if (priceLowerBound != 0 || priceUpperBound < float.MaxValue) {
            collection = collection.Where(model => model.Price >= priceLowerBound && model.Price <= priceUpperBound);
        }

        var models = await collection.ToListAsync();
        var result = models.Select(model => new ConsoleDTO {
            Id = model.Id,
            Name = model.Name,
            Type = model.Type,
            ReleaseYear = model.ReleaseYear,
            Price = model.Price,
        }).ToList();

        return TypedResults.Ok(result);
    }

    public async Task<IResult> DeleteFilteredConsoles(
        string? nameSearch = null, string? typeSearch = null, 
        DateTime? releaseYearSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
        
        var collection = _serviceDb.Consoles.AsQueryable();

        if (nameSearch != null) {
            collection = collection.Where(model => model.Name.Contains(nameSearch));
        }
        if (typeSearch != null) {
            collection = collection.Where(model => model.Type.Contains(typeSearch));
        }
        if (releaseYearSearch != null) {
            collection = collection.Where(model => model.ReleaseYear == releaseYearSearch);
        }
        if (priceLowerBound != 0 || priceUpperBound < float.MaxValue) {
            collection = collection.Where(model => model.Price >= priceLowerBound && model.Price <= priceUpperBound);
        }

        var items = await collection.ToListAsync();
        if (items.Any()) {
            foreach (var item in items) {
                _serviceDb.Remove(item);
            }
        }

        await _serviceDb.SaveChangesAsync();
        return TypedResults.NoContent();
    }

        // Controller
    public async Task<IResult> GetFilteredControllers(
        string? nameSearch = null, string? typeSearch = null, 
        int? releaseYearSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
        
        var collection = _serviceDb.Controllers.AsQueryable();
        
        if (nameSearch != null) {
            collection = collection.Where(model => model.Name.Contains(nameSearch));
        }
        if (typeSearch != null) {
            collection = collection.Where(model => model.Type.Contains(typeSearch));
        }
        if (releaseYearSearch != null) {
            collection = collection.Where(model => model.Year == releaseYearSearch);
        }
        if (priceLowerBound != 0 || priceUpperBound < float.MaxValue) {
            collection = collection.Where(model => model.Price >= priceLowerBound && model.Price <= priceUpperBound);
        }

        var models = await collection.ToListAsync();
        var result = models.Select(model => new ControllerDTO() {
            Price = model.Price,
            Type =  model.Type,
            Name = model.Name,
            Id = model.Id,
            Year = model.Year,
        });
        return TypedResults.Ok(result);
    }

    public async Task<IResult> DeleteFilteredControllers(
        string? nameSearch = null, string? typeSearch = null, 
        int? releaseYearSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
        
        var collection = _serviceDb.Controllers.AsQueryable();
        
        if (nameSearch != null) {
            collection = collection.Where(model => model.Name.Contains(nameSearch));
        }
        if (typeSearch != null) {
            collection = collection.Where(model => model.Type.Contains(typeSearch));
        }
        if (releaseYearSearch != null) {
            collection = collection.Where(model => model.Year == releaseYearSearch);
        }
        if (priceLowerBound != 0 || priceUpperBound < float.MaxValue) {
            collection = collection.Where(model => model.Price >= priceLowerBound && model.Price <= priceUpperBound);
        }

        var items = await collection.ToListAsync();
        if (items.Any()) {
            foreach (var item in items) {
                _serviceDb.Remove(item);
            }
        }
        await _serviceDb.SaveChangesAsync();
        return TypedResults.NoContent();
    }


        // Game
    public async Task<IResult> GetFilteredGames(
        string? nameSearch = null, string? ageRestrictionSearch = null, 
        string? genreSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
        
        var collection = _serviceDb.Games.AsQueryable();

        if (nameSearch != null) {
            collection = collection.Where(model => model.Name.Contains(nameSearch));
        }
        if (ageRestrictionSearch != null) {
            collection = collection.Where(model => model.AgeRestriction.Contains(ageRestrictionSearch));
        }
        if (genreSearch != null) {
            collection = collection.Where(model => model.Genre == genreSearch);
        }
        if (priceLowerBound != 0 || priceUpperBound < float.MaxValue) {
            collection = collection.Where(model => model.Price >= priceLowerBound && model.Price <= priceUpperBound);
        }

        var models = await collection.ToListAsync();
        var result = models.Select(model => new GameDTO {
            Id = model.Id,
            Name = model.Name,
            AgeRestriction = model.AgeRestriction,
            Genre = model.Genre,
            Price = model.Price,
        });
        return TypedResults.Ok(result);
    }

    public async Task<IResult> DeleteFilteredGames(
        string? nameSearch = null, string? ageRestrictionSearch = null, 
        string? genreSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
        
        var collection = _serviceDb.Games.AsQueryable();
        if (nameSearch != null) {
            collection = collection.Where(model => model.Name.Contains(nameSearch));
        }
        if (ageRestrictionSearch != null) {
            collection = collection.Where(model => model.AgeRestriction.Contains(ageRestrictionSearch));
        }
        if (genreSearch != null) {
            collection = collection.Where(model => model.Genre == genreSearch);
        }
        if (priceLowerBound != 0 || priceUpperBound < float.MaxValue) {
            collection = collection.Where(model => model.Price >= priceLowerBound && model.Price <= priceUpperBound);
        }

        var items = await collection.ToListAsync();
        if (items.Any()) {
            foreach (var item in items) {
                _serviceDb.Remove(item);
            }
        }
        await _serviceDb.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    //Business Models Complex Query Methods
        //DayStats
    public async Task<IResult> GetFilteredAgenda( 
        DateTime? date = null,
        int totalConsumerLowerBound = 0, int totalConsumerUpperBound= int.MaxValue, 
        int totalProfitLowerBound = 0, int totalProfitUpperBound = int.MaxValue,
        int totalCostLowerBound = 0, int totalCostUpperBound = int.MaxValue) {

        var collection = _businessDb.Agenda.AsQueryable();

        if (date.HasValue) {
            collection = collection.Where(model => model.Day == date);        
        }
        if (totalConsumerLowerBound != 0 || totalConsumerUpperBound != int.MaxValue) {
            collection = collection.Where(model  => 
                (model.TotalConsumers >= totalConsumerLowerBound) && 
                (model.TotalConsumers <= totalConsumerUpperBound));
        }
        if (totalProfitLowerBound != 0 || totalProfitUpperBound != int.MaxValue) {
            collection = collection.Where(model  => 
                (model.TotalProfit >= totalProfitLowerBound) && 
                (model.TotalProfit <= totalProfitUpperBound));
        }
        if (totalCostLowerBound != 0 || totalCostUpperBound != int.MaxValue) {
            collection = collection.Where(model  => 
                (model.TotalCost >= totalCostLowerBound) && 
                (model.TotalCost <= totalCostUpperBound));
        }

        var models = await collection.ToListAsync();
        var result = models.Select(model => new DayStatsDTO {
            Id = model.Id,    
            Day = model.Day,
            TotalConsumers = model.TotalConsumers,
            TotalProfit = model.TotalProfit,
            TotalCost = model.TotalCost,
        });
        return TypedResults.Ok(result);
    }
        
    public async Task<IResult> DeleteFilteredAgenda( 
        DateTime? date = null,
        int totalConsumerLowerBound = 0, int totalConsumerUpperBound= int.MaxValue, 
        int totalProfitLowerBound = 0, int totalProfitUpperBound = int.MaxValue,
        int totalCostLowerBound = 0, int totalCostUpperBound = int.MaxValue) {

        var collection = _businessDb.Agenda.AsQueryable();

        if (date.HasValue) {
            collection = collection.Where(model => model.Day == date);        
        }
        if (totalConsumerLowerBound != 0 || totalConsumerUpperBound != int.MaxValue) {
            collection = collection.Where(model  => 
                (model.TotalConsumers >= totalConsumerLowerBound) && 
                (model.TotalConsumers <= totalConsumerUpperBound));
        }
        if (totalProfitLowerBound != 0 || totalProfitUpperBound != int.MaxValue) {
            collection = collection.Where(model  => 
                (model.TotalProfit >= totalProfitLowerBound) && 
                (model.TotalProfit <= totalProfitUpperBound));
        }
        if (totalCostLowerBound != 0 || totalCostUpperBound != int.MaxValue) {
            collection = collection.Where(model  => 
                (model.TotalCost >= totalCostLowerBound) && 
                (model.TotalCost <= totalCostUpperBound));
        }

        var items = await collection.ToListAsync();
        if (items.Any()) {
            foreach (var item in items) {
                _businessDb.Remove(item);
            }
        }
        await _businessDb.SaveChangesAsync();

        return TypedResults.NoContent();
    }
        
        //Employee - No Sentitive Information
    public async Task<IResult> GetFilteredEmployees( 
        string? nameSearch = null,  
        int ageLowerBound = 0, int ageUpperBound = int.MaxValue,
        int yearsWorkedLowerBound = 0, int yearsWorkedUpperBound = int.MaxValue,
        float salaryLowerBound = 0, float salaryUpperBound = float.MaxValue,
        bool? isFiredSearch = null
    ) {
        var collection = _businessDb.Employees.AsQueryable();

        if (nameSearch != null) {
            collection = collection.Where(model => model.Name.Contains(nameSearch));
        }
        if (ageLowerBound != 0 || ageUpperBound != int.MaxValue) {
            collection = collection.Where(model => 
                model.Age >= ageLowerBound && model.Age <= ageUpperBound
            );
        }
        if (yearsWorkedLowerBound != 0 ||yearsWorkedUpperBound != int.MaxValue) {
            collection = collection.Where(model =>
                model.YearsWorked >= yearsWorkedLowerBound && model.YearsWorked <= yearsWorkedUpperBound 
                );
        }
        if (salaryLowerBound != 0 || salaryUpperBound!= float.MaxValue) {
            collection = collection.Where(model =>
                model.Salary >= salaryLowerBound && model.Salary <= salaryUpperBound
            );
        }
        if (isFiredSearch != null) {
            collection = collection.Where(model => model.isFired == isFiredSearch);
            }

        var models = await collection.ToListAsync();
        var result = models.Select(model => new EmployeeDTO {
            Id = model.Id,
            Name = model.Name,
            Password = model.Password,
            Email = model.Email,
            Age = model.Age,
            YearsWorked = model.YearsWorked,
            Sex = model.Sex,
            IsFired = model.isFired,
            EndOfContract = model.EndOfContract,
            Salary = model.Salary,
        });
        return TypedResults.Ok(result);
    }

    public async Task<IResult> DeleteFilteredEmployees( 
        string? nameSearch = null,  
        int ageLowerBound = 0, int ageUpperBound = int.MaxValue,
        int yearsWorkedLowerBound = 0, int yearsWorkedUpperBound = int.MaxValue,
        float salaryLowerBound = 0, float salaryUpperBound = float.MaxValue,
        bool? isFiredSearch = null
    ) {
        var collection = _businessDb.Employees.AsQueryable();

        if (nameSearch != null) {
            collection = collection.Where(model => model.Name.Contains(nameSearch));
        }
        if (ageLowerBound != 0 || ageUpperBound != int.MaxValue) {
            collection = collection.Where(model => 
                model.Age >= ageLowerBound && model.Age <= ageUpperBound
            );
        }
        if (yearsWorkedLowerBound != 0 || yearsWorkedUpperBound != int.MaxValue) {
            collection = collection.Where(model =>
                model.YearsWorked >= yearsWorkedLowerBound && model.YearsWorked <= yearsWorkedUpperBound 
            );
        }
        if (salaryLowerBound != 0 ||  salaryUpperBound!= float.MaxValue) {
                collection = collection.Where(model =>
                model.Salary >= salaryLowerBound && model.Salary <= salaryUpperBound
            );
        }
        
        if (isFiredSearch != null)  {
            collection = collection.Where(model => model.isFired == isFiredSearch);
        } 

        var items = await collection.ToListAsync();
        if (items.Any()) {
            foreach (var item in items) {
                _businessDb.Remove(item);
            }
        }
        await _businessDb.SaveChangesAsync();
        return TypedResults.NoContent();
    }

}