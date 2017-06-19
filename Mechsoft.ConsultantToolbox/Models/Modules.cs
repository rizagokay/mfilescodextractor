using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechsoft.ConsultantToolbox.Models
{
    public class CodeExtractorModule : BaseModule
    {
        private string _name;

        public CodeExtractorModule()
        {
            _name = "Code Extractor";
        }

        public override string Name
        {
            get
            {
                return _name;
            }
        }
    }

    public class ValueListExtractorModule : BaseModule
    {
        private string _name;

        public ValueListExtractorModule()
        {
            _name = "Value List Extractor";
        }

        public override string Name
        {
            get
            {
                return _name;
            }
        }
    }
}
