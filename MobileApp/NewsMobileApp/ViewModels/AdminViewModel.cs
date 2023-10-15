using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using Sk = LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewModels;

public class AdminViewModel
{
    private readonly INewsService _newsService;
    private static DateTime startDate = DateTime.Now.AddDays(-6);
    private static DateTime endDate = DateTime.Now;
    private ObservableCollection<DateTimePoint> _data = new ObservableCollection<DateTimePoint>(Enumerable.Range(0, (int)(endDate - startDate).TotalDays + 1)
                         .Select(x => new DateTimePoint(startDate.AddDays(x), 0)));
    public object Sync => new();

    public AdminViewModel()
    {
        _newsService = Application.Current.Handler
           .MauiContext.Services.GetService<INewsService>();

        Series.Add(
         
            new LineSeries<DateTimePoint>
            {
                Values = _data,
                Stroke = new Sk.LinearGradientPaint(new[]{ new SKColor(46, 34, 172), new SKColor(203, 71, 157)}) { StrokeThickness = 10 },
                GeometryStroke = new Sk.LinearGradientPaint(new[]{ new SKColor(46, 34, 172), new SKColor(203, 71, 157) }) { StrokeThickness = 10 },
                Fill = new Sk.LinearGradientPaint( new[] { new SKColor(46, 34, 172), new SKColor(203, 71, 157) },
                new SKPoint(0.1f, 0),
                new SKPoint(1, 0.9f))
            }
         );

        GetDiagtamData();
        AddUsers(false, 20, 0);
    }

    public async void GetDiagtamData()
    {   
        var data = new List<DateTimePoint>(Enumerable.Range(0, (int)(endDate - startDate).TotalDays + 1)
                         .Select(x => new DateTimePoint(startDate.AddDays(x), Random.Shared.Next(2, 10))));
        await Task.Delay(1500);
        
        for(int i = 0; i < data.Count; i++) 
        {
            _data[i].Value = data[i].Value;
        }
    }

    public List<ISeries> Series { get; set; } = new();

    public ObservableCollection<UserViewModel> Users { get; set; } = new();

    public async void AddUsers(bool clear = false, int limit = 20, int offset = 0)
    {
        if(clear) Users.Clear();

        foreach (var user in _newsService.GetUsers())
        {
            Users.Add(user);
        }
        foreach (var user in _newsService.GetUsers())
        {
            Users.Add(user);
        }
    }
}
