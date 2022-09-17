using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEventsAPI.Core.CustomAttributes
{
    internal class CustomDateRangeAttribute : RangeAttribute
    {
        internal CustomDateRangeAttribute() : base(typeof(DateTime), DateTime.Now.ToString(), DateTime.MaxValue.ToString())
        {
        }
    }
}
