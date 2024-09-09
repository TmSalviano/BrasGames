using BrasGames.Data;
using BrasGames.Model.BusinessModels;
using BrasGames.Model.DTO.BusinessDTO;
using Microsoft.EntityFrameworkCore;

public class BasicRESTBusiness<T> where T : class {
    private readonly BusinessDbContext _db;
    private readonly Type[] _businessModelTypes = [ typeof(EmployeeDTO), typeof(DayStatsDTO)];

    public BasicRESTBusiness(BusinessDbContext db) {
        _db = db;
    }

    public async Task<IResult> GetAll() {
        //Employee
        if ( typeof(T) == _businessModelTypes[0] )  {
            var models = await _db.Employees.ToListAsync();
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
            }).ToList();
            return TypedResults.Ok(result);
        }
        //DayStats
        if ( typeof(T) == _businessModelTypes[1] )  {
            var models = await _db.Agenda.ToListAsync();
            var result = models.Select(model => new DayStatsDTO {
                Id = model.Id,    
                Day = model.Day,
                TotalConsumers = model.TotalConsumers,
                TotalProfit = model.TotalProfit,
                TotalCost = model.TotalCost,
            }).ToList();
            return TypedResults.Ok(result);
        }

        return TypedResults.Problem("Type T doesn't correspond to any of the Business Models.");
    }

    public async Task<IResult> GetId(int id) {
        //Employee
        if ( typeof(T) == _businessModelTypes[0] ) {
            var result = await _db.Employees.FindAsync(id);
            if (result == null) {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(new EmployeeDTO {
                Id = result.Id,
                Name = result.Name,
                Password = result.Password,
                Email = result.Email,
                Age = result.Age,
                YearsWorked = result.YearsWorked,
                Sex = result.Sex,
                IsFired = result.isFired,
                EndOfContract = result.EndOfContract,
                Salary = result.Salary,
            });
        }
        //DayStats
        if ( typeof(T) == _businessModelTypes[1] ) {
            var result = await _db.Agenda.FindAsync(id);
            if (result == null) {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(new DayStatsDTO {
                Id = result.Id,    
                Day = result.Day,
                TotalConsumers = result.TotalConsumers,
                TotalProfit = result.TotalProfit,
                TotalCost = result.TotalCost,
            });
        }
        return TypedResults.Problem("Type T doesn't correspond to any of the Business Models.");
    }

    public async Task<IResult> PostModel(T dtoModel) {
         //Employee
        if ( dtoModel is EmployeeDTO employee ) {
            await _db.Employees.AddAsync(
                new Employee {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Email = employee.Email,
                    EndOfContract = employee.EndOfContract,
                    isFired = employee.IsFired,
                    Password = employee.Password,
                    Salary = employee.Salary,
                    Sex = employee.Sex,
                    YearsWorked = employee.YearsWorked,
                }
            );
            await _db.SaveChangesAsync();
            return TypedResults.Created("/business/employee/" + employee.Id, employee);
        }
        //DayStats
        if ( dtoModel is DayStatsDTO dayStats ) {
            await _db.Agenda.AddAsync(new DayStats() {
                Id = dayStats.Id,
                Day = dayStats.Day,
                TotalConsumers = dayStats.TotalConsumers,
                TotalCost = dayStats.TotalCost,
                TotalProfit = dayStats.TotalProfit,
            });
            await _db.SaveChangesAsync();
            return TypedResults.Created("/business/agenda/" + dayStats.Id, dayStats);
        }
        return TypedResults.Problem("userModel Type T doesn't correspond to any of the Business Models.");
    }

    public async Task<IResult> PutModel(int id, T dtoModel) {
        if (dtoModel is EmployeeDTO employee) {
            var result = await _db.Employees.FindAsync(id);
            if (result == null)
                return TypedResults.NotFound();

            result.Name = employee.Name;
            result.Password = employee.Password;
            result.Email = employee.Email;
            result.Age = employee.Age;
            result.YearsWorked = employee.YearsWorked;
            result.Sex = employee.Sex;
            result.isFired = employee.IsFired;
            result.EndOfContract = employee.EndOfContract;
            result.Salary = employee.Salary;

            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        if (dtoModel is DayStatsDTO dayStats ) {
            var result = await _db.Agenda.FindAsync(id);
            if (result == null)
                return TypedResults.NotFound();

            result.Day = dayStats.Day;
            result.TotalConsumers = dayStats.TotalConsumers;
            result.TotalProfit = dayStats.TotalProfit;
            result.TotalCost = dayStats.TotalCost;

            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        return TypedResults.Problem("userModel Type T doesn't correspond to any of the Business Models.");
    }

    //DTO's are useless
    public async Task<IResult> DeleteModel(int id) {
        //Employees
        if (typeof(T) == _businessModelTypes[0]) {
            var result = await _db.Employees.FindAsync(id);
            if (result == null)
                return TypedResults.NotFound();
            
            _db.Remove(result);
            await _db.SaveChangesAsync();

            return TypedResults.NoContent();
        }
        //DayStats
        if (typeof(T) == _businessModelTypes[1]) {
            var result = await _db.Agenda.FindAsync(id);
            if (result == null)
                return TypedResults.NotFound();
            
            _db.Remove(result);
            await _db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        return TypedResults.Problem("Type T doesn't correspond to any of the Business Models.");
    }

    //DTO's are useless
    public async Task<IResult> DeleteAllModels() {
        //Employees
        if (typeof(T) == _businessModelTypes[0]) {
            var result = await _db.Employees.ToListAsync();
            if (result == null)
                return TypedResults.NotFound();
            
            foreach (var model in result) {
                _db.Remove(model);
            }

            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }
        //DayStats
        if (typeof(T) == _businessModelTypes[1]) {
            var result = await _db.Agenda.ToListAsync();
            if (result == null)
                return TypedResults.NotFound();
            
            foreach (var model in result) {
                _db.Remove(model);
            }

            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        return TypedResults.Problem("Type T doesn't correspond to any of the Business Models.");
    }
}