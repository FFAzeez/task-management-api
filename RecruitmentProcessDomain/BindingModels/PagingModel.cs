using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessDomain.BindingModels
{
    public class PagingModel
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string SortColumn { get; set; } = "CreatedDate";
        public string SortOrder { get; set; } = "desc";
    }
}
