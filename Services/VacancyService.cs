using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using SoftwareCompanyApp.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace SoftwareCompanyApp.Services
{
    public class VacancyService
    {
        public ObservableCollection<Vacancy> Vacancies { get; }

        private readonly ApplicationDbContext _context;

        public VacancyService(ApplicationDbContext context)
        {
            _context = context;
            Vacancies = new ObservableCollection<Vacancy>(_context.Vacancies.ToList());
        }

        public void AddVacancy(Vacancy vacancy)
        {
            _context.Vacancies.Add(vacancy);
            _context.SaveChanges();
            Vacancies.Add(vacancy); // Добавляем в ObservableCollection
        }

        public void RemoveVacancy(Vacancy vacancy)
        {
            _context.Vacancies.Remove(vacancy);
            _context.SaveChanges();
            Vacancies.Remove(vacancy);
        }
    }
}
