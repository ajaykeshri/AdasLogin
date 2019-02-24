using System;
using System.Collections.Generic;
using System.Text;

namespace ADASMobileClient.Core.model
{
    class VehicleVinResponse
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public string SearchCriteria { get; set; }
        public IList<Result> Results { get; set; }
        

    }
    public class Result
    {
        public string Value { get; set; }
        public string ValueId { get; set; }
        public string Variable { get; set; }
        public int VariableId { get; set; }

        public bool HasComment => !string.IsNullOrEmpty(Value);
    }
}
