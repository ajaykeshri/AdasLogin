using System;
using System.Collections.Generic;
using System.Text;

namespace ADASMobileClient.Core.model
{
    class WorkOrderModel
    {
        public string id { get; set; }
        public string vinnumber { get; set; }
        public string workorder { get; set; }
        public string model { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string totalcalibration { get; set; }
        public string numberofcalibrationcompleted { get; set; }
        public string totalactiveworkorder { get; set; }
        public bool iscompletedcalibration { get; set; }
        public bool isactive { get; set; }
        public string createdby { get; set; }
        public string createddate { get; set; }
        public string lastupdatedby { get; set; }
        public string lastupdateddate { get; set; }
        public List<CalibrationDetailRow> calibrationDetailRows { get; set; }


    }
    public class CalibrationDetailRow
    {
        public string adasModuleName { get; set; }
        public string moduleAvailability { get; set; }
        public string targetAvailability { get; set; }
        public string status { get; set; }
    }

}
