using System.Collections.Generic;
using System.Linq;

namespace SoftwareCompanyApp.Models
{
    public class JobSeeker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }

        // Добавляем коллекцию скиллов
        public ICollection<JobSeekerSkill> JobSeekerSkills { get; set; }
        public ICollection<Skill> Skills
        {
            get
            {
                return JobSeekerSkills?.Select(js => js.Skill).ToList();
            }
        }
    }

}
