using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using AgroGraph.Commands;
using AgroGraph.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.Win32;
using Newtonsoft.Json;
using SkiaSharp;

namespace AgroGraph.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<SoilTesting> Testings { get; set; }
    public ICommand ReadDataCommand { get; }

    private ISeries[] _temperatureSeries;
    private ISeries[] _humiditySeries;
    private ISeries[] _conductivitySeries;
    private ISeries[] _aciditySeries;
    private ISeries[] _nitrogenSeries;
    private ISeries[] _phosphorusSeries;
    private ISeries[] _potassiumSeries;
    private ISeries[] _salinitySeries;
    private ISeries[] _totalDissolvedSolidsSeries;

    private IEnumerable<Axis> _xAxes;
    private IEnumerable<Axis> _yAxes;

    public ISeries[] TemperatureSeries
    {
        get => _temperatureSeries;
        set
        {
            _temperatureSeries = value;
            OnPropertyChanged();
        }
    }

    public ISeries[] HumiditySeries
    {
        get => _humiditySeries;
        set
        {
            _humiditySeries = value;
            OnPropertyChanged();
        }
    }

    public ISeries[] ConductivitySeries
    {
        get => _conductivitySeries;
        set
        {
            _conductivitySeries = value;
            OnPropertyChanged();
        }
    }

    public ISeries[] AciditySeries
    {
        get => _aciditySeries;
        set
        {
            _aciditySeries = value;
            OnPropertyChanged();
        }
    }

    public ISeries[] NitrogenSeries
    {
        get => _nitrogenSeries;
        set
        {
            _nitrogenSeries = value;
            OnPropertyChanged();
        }
    }

    public ISeries[] PhosphorusSeries
    {
        get => _phosphorusSeries;
        set
        {
            _phosphorusSeries = value;
            OnPropertyChanged();
        }
    }

    public ISeries[] PotassiumSeries
    {
        get => _potassiumSeries;
        set
        {
            _potassiumSeries = value;
            OnPropertyChanged();
        }
    }

    public ISeries[] SalinitySeries
    {
        get => _salinitySeries;
        set
        {
            _salinitySeries = value;
            OnPropertyChanged();
        }
    }

    public ISeries[] TotalDissolvedSolidsSeries
    {
        get => _totalDissolvedSolidsSeries;
        set
        {
            _totalDissolvedSolidsSeries = value;
            OnPropertyChanged();
        }
    }

    public IEnumerable<Axis> XAxes
    {
        get => _xAxes;
        set
        {
            _xAxes = value;
            OnPropertyChanged();
        }
    }

    public IEnumerable<Axis> YAxes
    {
        get => _yAxes;
        set
        {
            _yAxes = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel()
    {
        ReadDataCommand = new RelayCommand(ReadData);
        Testings = new ObservableCollection<SoilTesting>();
    }

    private void ReadData()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            string jsonData = File.ReadAllText(openFileDialog.FileName);

            var soilTests = JsonConvert.DeserializeObject<List<SoilTestingData>>(jsonData);

            Testings.Clear();

            if (soilTests != null)
            {
                foreach (var test in soilTests)
                {
                    Testings.Add(new SoilTesting(
                        test.Timestamp,
                        test.Humi,
                        test.Temp,
                        test.Cond,
                        test.Phph,
                        test.Nitro,
                        test.Phos,
                        test.Pota,
                        test.Soli,
                        test.Tds
                    ));
                }

                // Инициализация серий данных
                TemperatureSeries = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        DataLabelsSize = 20,
                        DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                        DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                        DataLabelsFormatter = (point) => (point.Coordinate.PrimaryValue + " \u00b0C").ToString(),
                        Values = new List<double>(Testings.Select(t => t.Temperature)),
                        Name = "Температура",
                        Fill = null
                    }
                };

                HumiditySeries = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        DataLabelsSize = 20,
                        DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                        DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                        DataLabelsFormatter = (point) => (point.Coordinate.PrimaryValue + " %").ToString(),
                        Values = new List<double>(Testings.Select(t => t.Humidity)),
                        Name = "Влажность",
                        Fill = null
                    }
                };

                ConductivitySeries = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        DataLabelsSize = 20,
                        DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                        DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                        DataLabelsFormatter = (point) => (point.Coordinate.PrimaryValue + " uS/cm").ToString(),
                        Values = new List<double>(Testings.Select(t => t.Conductivity)),
                        Name = "Электропроводность",
                        Fill = null
                    }
                };

                AciditySeries = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        DataLabelsSize = 20,
                        DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                        DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                        DataLabelsFormatter = (point) => (point.Coordinate.PrimaryValue).ToString(),
                        Values = new List<double>(Testings.Select(t => t.Acidity)),
                        Name = "Кислотность",
                        Fill = null
                    }
                };

                NitrogenSeries = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        DataLabelsSize = 20,
                        DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                        DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                        DataLabelsFormatter = (point) => (point.Coordinate.PrimaryValue + " mg/L").ToString(),
                        Values = new List<double>(Testings.Select(t => t.Nitrogen)),
                        Name = "Азот",
                        Fill = null
                    }
                };

                PhosphorusSeries = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        DataLabelsSize = 20,
                        DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                        DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                        DataLabelsFormatter = (point) => (point.Coordinate.PrimaryValue + " mg/L").ToString(),
                        Values = new List<double>(Testings.Select(t => t.Phosphorus)),
                        Name = "Фосфор",
                        Fill = null
                    }
                };

                PotassiumSeries = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        DataLabelsSize = 20,
                        DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                        DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                        DataLabelsFormatter = (point) => (point.Coordinate.PrimaryValue + " mg/L").ToString(),
                        Values = new List<double>(Testings.Select(t => t.Potassium)),
                        Name = "Калии",
                        Fill = null
                    }
                };

                SalinitySeries = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        DataLabelsSize = 20,
                        DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                        DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                        DataLabelsFormatter = (point) => (point.Coordinate.PrimaryValue + " g/L").ToString(),
                        Values = new List<double>(Testings.Select(t => t.Salinity)),
                        Name = "Соленость",
                        Fill = null
                    }
                };

                TotalDissolvedSolidsSeries = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        DataLabelsSize = 20,
                        DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                        DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                        DataLabelsFormatter = (point) => (point.Coordinate.PrimaryValue + " mg/L").ToString(),
                        Values = new List<double>(Testings.Select(t => t.TotalDissolvedSolids)),
                        Name = "Общее содержание растворенных веществ",
                        Fill = null
                    }
                };

                // Определение осей X и Y
                XAxes = new Axis[]
                {
                    new Axis { Labels = Testings.Select(t => t.Timestamp / 1000 + " сек").ToList() }
                };

                YAxes = new Axis[]
                {
                    new Axis()
                };
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}