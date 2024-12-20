using System.Collections.Generic;

namespace SoftwareCompanyApp.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Requirments { get; set; }
        public string SalaryFrom { get; set; }
        public string SalaryTo { get; set; }
        public int EmploymentTypeId { get; set; }
        public List<VacancySkill> VacancySkills { get; set; }
    }
}
