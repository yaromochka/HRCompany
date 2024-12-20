using Microsoft.EntityFrameworkCore;
using SoftwareCompanyApp.Data;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using SoftwareCompanyApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SoftwareCompanyApp.Services
{
    public class JobSeekerService
    {
        public ObservableCollection<JobSeeker> JobSeekers { get; set; }

        private readonly ApplicationDbContext _context;

        public JobSeekerService(ApplicationDbContext context)
        {
            _context = context;
            JobSeekers = new ObservableCollection<JobSeeker>(_context.JobSeekers.Include(js => js.JobSeekerSkills).ToList());
        }

        public void RemoveJobSeeker(JobSeeker jobSeeker)
        {
            _context.JobSeekers.Remove(jobSeeker);
            _context.SaveChanges();
            JobSeekers.Remove(jobSeeker);
        }

        public void UpdateJobSeeker(JobSeeker jobSeeker)
        {
            var existingJobSeeker = _context.JobSeekers.FirstOrDefault(js => js.Id == jobSeeker.Id);
            if (existingJobSeeker != null)
            {
                existingJobSeeker.FirstName = jobSeeker.FirstName;
                existingJobSeeker.LastName = jobSeeker.LastName;
                existingJobSeeker.Email = jobSeeker.Email;
                existingJobSeeker.Phone = jobSeeker.Phone;
                existingJobSeeker.Description = jobSeeker.Description;
                existingJobSeeker.Location = jobSeeker.Location;
                existingJobSeeker.Position = jobSeeker.Position;
                existingJobSeeker.SalaryFrom = jobSeeker.SalaryFrom;
                existingJobSeeker.SalaryTo = jobSeeker.SalaryTo;
                existingJobSeeker.IsActive = jobSeeker.IsActive;

                _context.JobSeekers.Attach(existingJobSeeker);
                _context.Entry(existingJobSeeker).State = EntityState.Modified;
                _context.SaveChanges();
                _ = ReloadJobSeekers();
            }
        }

        public void AddJobSeeker(JobSeeker jobSeeker)
        {
            _context.JobSeekers.Add(jobSeeker);
            _context.SaveChanges();
            JobSeekers.Add(jobSeeker);
        }

        public JobSeeker GetJobSeekerById(int jobSeekerId)
        {
            var jobSeeker = _context.JobSeekers
                .Include(js => js.JobSeekerSkills)
                .ThenInclude(js => js.Skill)
                .FirstOrDefault(js => js.Id == jobSeekerId);

            if (jobSeeker == null)
            {
                MessageBox.Show("JobSeeker not found.");
                return null;
            }

            return jobSeeker;
        }
        

        public async Task ReloadJobSeekers()
        {
            var jobSeekers = await _context.JobSeekers.Include(js => js.JobSeekerSkills).ToListAsync();
            JobSeekers.Clear();
            foreach (var jobSeeker in jobSeekers)
            {
                JobSeekers.Add(jobSeeker);
            }
        }

        public void AddJobSeekerSkills(int jobSeekerId, ObservableCollection<Skill> selectedSkills)
        {
            var jobSeekerSkills = selectedSkills.Select(skill => new JobSeekerSkill
            {
                JobSeekerId = jobSeekerId,
                SkillId = skill.Id
            }).ToList();

            _context.JobSeekerSkills.AddRange(jobSeekerSkills);
            _context.SaveChanges();
        }

        public void UpdateJobSeekerSkills(int jobSeekerId, ObservableCollection<Skill> selectedSkills)
        {
            var existingSkills = _context.JobSeekerSkills.Where(js => js.JobSeekerId == jobSeekerId).ToList();
            _context.JobSeekerSkills.RemoveRange(existingSkills);

            var newSkills = selectedSkills.Select(skill => new JobSeekerSkill
            {
                JobSeekerId = jobSeekerId,
                SkillId = skill.Id
            }).ToList();

            _context.JobSeekerSkills.AddRange(newSkills);
            _context.SaveChanges();
        }
    }
}
