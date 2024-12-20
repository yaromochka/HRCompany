using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareCompanyApp.Models
{
    public class VacancySkill
    {
        public int Id { get; set; }

        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }

}
