using BrasGames.Data;
using BrasGames.Model.BusinessModels;
using Microsoft.EntityFrameworkCore;

public class BasicRESTBusiness<T> where T : class {
    private readonly BusinessDbContext _db;
    private readonly Type[] _businessModelTypes = [ typeof(Employee), typeof(DayStats)];

    public BasicRESTBusiness(BusinessDbContext db) {
        _db = db;
    }

    public async Task<IResult> GetAll() {
        //Employee
        if ( typeof(T) == _businessModelTypes[0] ) 
            return TypedResults.Ok(await _db.Employees.ToListAsync());
        //DayStats
        if ( typeof(T) == _businessModelTypes[1] ) 
            return TypedResults.Ok(await _db.Agenda.ToListAsync());

        return TypedResults.Problem("Type T doesn't correspond to any of the Business Models.");
    }

    public async Task<IResult> GetId(int id) {
        //Employee
        if ( typeof(T) == _businessModelTypes[0] ) {
            var result = await _db.Employees.FindAsync(id);
            if (result == null) {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(result);
        }
        //DayStats
        if ( typeof(T) == _businessModelTypes[1] ) {
            var result = await _db.Agenda.FindAsync(id);
            if (result == null) {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(result);
        }
        return TypedResults.Problem("Type T doesn't correspond to any of the Business Models.");
    }

    public async Task<IResult> PostModel(T userModel) {
         //Employee
        if ( userModel is Employee employee ) {
            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();

            return TypedResults.Created("/business/employee/" + employee.Id, employee);
        }
        //DayStats
        if ( userModel is DayStats dayStats ) {
            await _db.Agenda.AddAsync(dayStats);
            await _db.SaveChangesAsync();

            return TypedResults.Created("/business/agenda/" + dayStats.Id, dayStats);
        }
        return TypedResults.Problem("userModel Type T doesn't correspond to any of the Business Models.");
    }

    public async Task<IResult> PutModel(int id, T userModel) {
        if (userModel is Employee employee) {
            var result = await _db.Employees.FindAsync(id);
            if (result == null)
                return TypedResults.NotFound();

            result.Id = employee.Id;
            result.Name = employee.Name;
            result.Password = employee.Password;
            result.Email = employee.Email;
            result.Age = employee.Age;
            result.YearsWorked = employee.YearsWorked;
            result.Sex = employee.Sex;
            result.isFired = employee.isFired;
            result.EndOfContract = employee.EndOfContract;
            result.Salary = employee.Salary;

            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        if (userModel is DayStats dayStats ) {
            var result = await _db.Agenda.FindAsync(id);
            if (result == null)
                return TypedResults.NotFound();

            result.Id = dayStats.Id;
            result.Day = dayStats.Day;
            result.TotalConsumers = dayStats.TotalConsumers;
            result.TotalProfit = dayStats.TotalProfit;
            result.TotalCost = dayStats.TotalCost;

            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        return TypedResults.Problem("UserModel Type T doesn't correspond to any of the Business Models.");
    }

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