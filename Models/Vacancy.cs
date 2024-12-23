using System.Collections.Generic;
using System.Linq;

namespace SoftwareCompanyApp.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Requirments { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }
        public int EmploymentTypeId { get; set; }
        // Добавляем коллекцию скиллов
        public ICollection<VacancySkill> VacancySkills { get; set; } = new List<VacancySkill>();
    }
}
