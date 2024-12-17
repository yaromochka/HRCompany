using System.Collections.Generic;

namespace SoftwareCompanyApp.Models
{
    public class JobSeeker
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // Навигационное свойство
        public ICollection<JobSeekerSkill> JobSeekerSkills { get; set; }
    }

}
