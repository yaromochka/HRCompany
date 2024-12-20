using SoftwareCompanyApp.Models;
using System.Collections.Generic;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<JobSeekerSkill> JobSeekerSkills { get; set; }
    public ICollection<VacancySkill> VacancySkills { get; set; }
}
