using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechsoft.CodeExtractor.Models
{
    public class ExtractResult
    {
        public int EventHandlersCount { get; set; }
        public int ValidationsCount { get; set; }
        public int CalculatedValuesCount { get; set; }
        public int NumberingCount { get; set; }
        public int TriggerCount { get; set; }
        public int StateActionCount { get; set; }
        public int ConditionCount { get; set; }
        public int ApplicationsCount { get; set; }
    }
}
