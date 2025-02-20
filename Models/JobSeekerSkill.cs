﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftwareCompanyApp.Models
{
    public class JobSeekerSkill
    {
        [Key, Column(Order = 0)]
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }

        [Key, Column(Order = 1)]
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
