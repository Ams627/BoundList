using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace BoundList
{
    public class PathWithSelectionAndAnimation : INotifyPropertyChanged
    {
        private string _value;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Deleted { get; set; } = false;
        int Moving { get; set; } = -1;
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        private bool _selected { get; set; }

        public bool Focused { get; set; } = false;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        void OnPropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
    public class ViewModel
    {
        public ObservableCollection<PathWithSelectionAndAnimation> Paths { get; private set; } = new ObservableCollection<PathWithSelectionAndAnimation>();
        public ViewModel()
        {
            Paths.Add(new PathWithSelectionAndAnimation { Value = "Hello" });
            Paths.Add(new PathWithSelectionAndAnimation { Value = "Mister" });
            Paths.Add(new PathWithSelectionAndAnimation { Value = "Fatman" });

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(3000);
            timer.Tick += (s, e) =>
            {
                foreach (var p in Paths)
                {
                    p.Selected = true;
                    //System.Diagnostics.Debug.WriteLine($"{p.Value} - {p.Selected}");
                }
                System.Diagnostics.Debug.WriteLine("-------------");
            };
            timer.Start();
        }
    }
}
