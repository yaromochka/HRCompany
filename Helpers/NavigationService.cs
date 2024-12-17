using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace SoftwareCompany.Helpers
{
    public class NavigationService : INotifyPropertyChanged
    {
        private readonly Stack<Page> _history = new Stack<Page>();
        private readonly Frame _frame;

        public NavigationService(Frame frame)
        {
            _frame = frame ?? throw new ArgumentNullException(nameof(frame));
        }

        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            private set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        // Навигация на страницу
        public void Navigate(Type pageType)
        {
            if (pageType == null)
                throw new ArgumentNullException(nameof(pageType));

            // Сохраняем текущую страницу в истории перед переходом
            if (CurrentPage != null)
                _history.Push(CurrentPage);

            // Создаем новую страницу и отображаем ее
            CurrentPage = (Page)Activator.CreateInstance(pageType);
            _frame.Navigate(CurrentPage);
        }

        // Переход назад
        public void GoBack()
        {
            if (_history.Count > 0)
            {
                CurrentPage = _history.Pop();
                _frame.Navigate(CurrentPage);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
