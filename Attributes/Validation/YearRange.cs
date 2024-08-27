using System.ComponentModel.DataAnnotations;

public class YearRange : RangeAttribute
{
  //Does not transform DataTime value into a string. Only uses the string to do the comparison.
  public YearRange()
    : base(typeof(DateTime), 
            DateTime.Now.Year.ToString(),
            DateTime.MaxValue.Year.ToString()) 
  { } 
}
