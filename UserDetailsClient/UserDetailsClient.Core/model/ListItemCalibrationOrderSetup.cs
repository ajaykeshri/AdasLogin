using System;
using System.Collections.Generic;
using System.Text;

namespace ADASMobileClient.Core.model
{
    public class ListItemCalibrationOrderSetup
    {
        //  Item Name.       
        public string vinnumber { get; set; }

        //  Item Image.
        public string ItemImage { get; set; }

        //  Selection Image.
        public string CheckboxImage { get; set; }

        public string ModuleAvailability { get;  set; }
        public string TargetAvailability { get;  set; }
        public string Status { get;  set; }
    }
}
