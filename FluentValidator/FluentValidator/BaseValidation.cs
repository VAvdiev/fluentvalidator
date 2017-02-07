using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentValidator
{
    public abstract class  BaseValidation
    {
        readonly IList<IValidatorResult> _validatorResults = new List<IValidatorResult>();

        public virtual BaseValidation Validate()
        {
            return this;
        }

        public IEnumerable<IValidatorResult> Violations()
        {
            return _validatorResults.Where(x => !x.IsValid).ToList();
        } 

        public virtual void Reset()
        {
            _validatorResults.Clear();
        }

        public int ViolationsCount()
        {
            return  _validatorResults.Where(x => !x.IsValid).Count();
        }
    }


    public class CreateEmployeeRequest
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string JobTitle { get; set; }
        public int LocationID { get; set; }
    }
    class SampleValidation : BaseValidation
    {
        public bool ValidateP(CreateEmployeeRequest request)
        {
            
        }
    }
}