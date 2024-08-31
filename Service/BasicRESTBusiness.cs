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

}