using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareCompanyApp.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Навигационное свойство
        public ICollection<JobSeekerSkill> JobSeekerSkills { get; set; }
    }

}
