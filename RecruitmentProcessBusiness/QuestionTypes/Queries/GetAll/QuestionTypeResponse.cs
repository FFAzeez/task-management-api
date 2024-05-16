using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentProcessBusiness.QuestionTypes.Queries.GetAll
{
    public class QuestionTypeResponse
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Type { get; set; }
    }
}
