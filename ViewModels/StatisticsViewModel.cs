using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Extensions.DependencyInjection;
using SoftwareCompanyApp.Data.SoftwareCompanyApp.Data;
using SoftwareCompanyApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace SoftwareCompanyApp.ViewModels
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        // Данные для графиков
        private SeriesCollection salaryDistribution;
        public SeriesCollection SalaryDistribution
        {
            get => salaryDistribution;
            set
            {
                salaryDistribution = value;
                OnPropertyChanged();
            }
        }

        private List<string> salaryRanges;
        public List<string> SalaryRanges
        {
            get => salaryRanges;
            set
            {
                salaryRanges = value;
                OnPropertyChanged();
            }
        }

        private SeriesCollection skillsDistribution;
        public SeriesCollection SkillsDistribution
        {
            get => skillsDistribution;
            set
            {
                skillsDistribution = value;
                OnPropertyChanged();
            }
        }

        private List<string> skillNames;
        public List<string> SkillNames
        {
            get => skillNames;
            set
            {
                skillNames = value;
                OnPropertyChanged();
            }
        }

        public StatisticsViewModel()
        {
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    // Получаем контекст базы данных из ServiceProvider
                    var dbContext = App.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    if (dbContext == null)
                    {
                        throw new InvalidOperationException("Не удалось получить экземпляр ApplicationDbContext из ServiceProvider.");
                    }

                    var salaryData = PrepareSalaryDistribution(dbContext);
                    var skillsData = PrepareSkillsDistribution(dbContext);

                    // Переключение на UI-поток для обновления данных
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ApplySalaryDistribution(salaryData);
                        ApplySkillsDistribution(skillsData);
                    });
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private (List<string> ranges, List<int> counts) PrepareSalaryDistribution(ApplicationDbContext dbContext)
        {
            var vacancies = dbContext.Vacancies.ToList();

            // Подсчёт диапазонов зарплат
            var salaryGroups = vacancies
                .Select(v =>
                {
                    int salaryFrom = int.TryParse(v.SalaryFrom, out int sf) ? sf : 0;
                    int salaryTo = int.TryParse(v.SalaryTo, out int st) ? st : 0;
                    return (salaryFrom + salaryTo) / 2; // Средняя зарплата
                })
                .GroupBy(salary => salary / 10000) // Группировка по диапазонам
                .Select(group => new
                {
                    Range = $"{group.Key * 10000} - {(group.Key + 1) * 10000}",
                    Count = group.Count()
                })
                .ToList();

            return (salaryGroups.Select(g => g.Range).ToList(), salaryGroups.Select(g => g.Count).ToList());
        }

        private (List<string> skillNames, List<int> counts) PrepareSkillsDistribution(ApplicationDbContext dbContext)
        {
            var skills = dbContext.Skills.ToList();
            var vacancySkills = dbContext.VacancySkills.ToList();
            var jobSeekerSkills = dbContext.JobSeekerSkills.ToList();

            // Объединение данных по навыкам
            var skillCounts = skills
                .Select(skill =>
                {
                    int vacancyCount = vacancySkills.Count(vs => vs.SkillId == skill.Id);
                    int jobSeekerCount = jobSeekerSkills.Count(js => js.SkillId == skill.Id);
                    return new
                    {
                        SkillName = skill.Name,
                        Count = vacancyCount + jobSeekerCount
                    };
                })
                .OrderByDescending(skill => skill.Count)
                .Take(10) // Топ-10 навыков
                .ToList();

            return (skillCounts.Select(s => s.SkillName).ToList(), skillCounts.Select(s => s.Count).ToList());
        }

        private void ApplySalaryDistribution((List<string> ranges, List<int> counts) data)
        {
            SalaryRanges = data.ranges;
            SalaryDistribution = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Количество",
                    Values = new ChartValues<int>(data.counts)
                }
            };
        }

        private void ApplySkillsDistribution((List<string> skillNames, List<int> counts) data)
        {
            SkillNames = data.skillNames;
            SkillsDistribution = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Навыки",
                    Values = new ChartValues<int>(data.counts)
                }
            };
        }

        // Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
