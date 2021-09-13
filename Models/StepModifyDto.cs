using System.Collections.Generic;
using cookBook.Models.Validators;

namespace cookBook.Models
{
    public class StepModifyDto
    {
        [Steps(250)] 
        public IEnumerable<string> Steps;
    }
}