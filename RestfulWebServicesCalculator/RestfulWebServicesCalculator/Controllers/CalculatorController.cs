using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestfulWebServicesCalculator.Controllers
{
    [Route("api/[controller]")]
    public class CalculatorController : Controller
    {
        // GET api/values/5/5
        [HttpGet("{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid Input.");
        }

        private int ConvertToDecimal(string secondNumber)
        {
            throw new NotImplementedException();
        }

        private bool IsNumeric(string firstNumber)
        {
            throw new NotImplementedException();
        }
    }
}
