using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessDomain.Enums
{
    public enum Gender
    {
        [Description("Male")]
        Male = 0,

        [Description("Female")]
        Female,
    }
}
