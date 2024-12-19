using System.Collections.Generic;

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

        // Навигационное свойство
        public ICollection<JobSeekerSkill> JobSeekerSkills { get; set; }
        public bool IsActive { get; set; }
    }

}
