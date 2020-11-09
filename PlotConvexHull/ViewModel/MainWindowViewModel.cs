using OxyPlot;
using PlotConvexHull.Command;
using PlotConvexHull.Services.Algorithms;
using PlotConvexHull.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;

namespace PlotConvexHull.ViewModel
{
    internal class MainWindowViewModel: BaseViewModel
    {
        #region Fields

        private readonly MelkmanAlgorithm _Melkman;

        #endregion

        #region Properties

        private string _Title = "Plot Convex Hull";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private string _AmountOfPoint;

        public string AmountOfPoint
        {
            get => _AmountOfPoint;
            set => Set(ref _AmountOfPoint, value);
        }

        private ObservableCollection<DataPoint> _HullPoints;

        public ObservableCollection<DataPoint> HullPoints
        {
            get => _HullPoints;
            set => Set(ref _HullPoints, value);
        }

        private ObservableCollection<DataPoint> _Points;

        public ObservableCollection<DataPoint> Points
        {
            get => _Points;
            set => Set(ref _Points, value);
        }

        #endregion

        #region Commands

        private ICommand _GetPlotCommand;
        public ICommand GetPlotCommand
            => _GetPlotCommand ??= new LambdaCommand(OnGetPlotCommandExecuted, CanGetPlotCommandExecute);

        private bool CanGetPlotCommandExecute() 
            => !string.IsNullOrWhiteSpace(AmountOfPoint) && AmountOfPoint.ToCharArray().All(char.IsDigit);
        private void OnGetPlotCommandExecuted()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            List<DataPoint> points = Enumerable.Range(0, int.Parse(AmountOfPoint))
                .Select(i => new DataPoint(rnd.NextDouble(), rnd.NextDouble())).ToList();

            Points = new ObservableCollection<DataPoint>(points);

            try
            {
                HullPoints = new ObservableCollection<DataPoint>(_Melkman.ConvexHull(points));
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        public MainWindowViewModel()
        {
            _Melkman = new MelkmanAlgorithm();
        }

    }
}
