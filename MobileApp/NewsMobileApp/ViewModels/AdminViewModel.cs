using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using Sk = LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using NewsMobileApp.TempServices;
using NewsMobileApp.Models.ODataModels;

namespace NewsMobileApp.ViewModels;

public class AdminViewModel
{
    private readonly IRequestsService _requestService;
    private static readonly DateTime _startDate = DateTime.Now.AddDays(-6);
    private static readonly DateTime _endDate = DateTime.Now;
    private readonly ObservableCollection<DateTimePoint> _data = new ObservableCollection<DateTimePoint>(Enumerable.Range(0, 
        (int)(_endDate - _startDate).TotalDays + 1).Select(x => new DateTimePoint(_startDate.AddDays(x), 0)));
    public ObservableCollection<UserViewModel> Users { get; set; } = new();
    public ObservableCollection<ISeries> Series { get; set; } = new();

    public AdminViewModel()
    {
        _requestService = Application.Current.Handler
            .MauiContext.Services.GetService<IRequestsService>();

        Series.Add(
            new LineSeries<DateTimePoint>
            {
                Values = _data,
                Stroke = new Sk.LinearGradientPaint(new[]{ new SKColor(46, 34, 172), new SKColor(203, 71, 157)}) { 
                    StrokeThickness = 10 },
                GeometryStroke = new Sk.LinearGradientPaint(new[]{ new SKColor(46, 34, 172), new SKColor(203, 71, 157) }) { 
                    StrokeThickness = 10 },
                Fill = new Sk.LinearGradientPaint( new[] { new SKColor(46, 34, 172), new SKColor(203, 71, 157) },
                new SKPoint(0.1f, 0), new SKPoint(1, 0.9f))
            }
         );
    }

    public async Task GetDiagtamData()
    {   
        List<DiagramData> data = (await _requestService.GetDiagramAsync(_startDate, _endDate)).OrderBy(x => x.PublishTime).ToList();
        Parallel.For(0, _data.Count, i => {
            _data[i].Value += data.Where(d => d.PublishTime.Date.Equals(_data[i].DateTime.Date)).Sum(d => d.Total);
        });
    }



    public async Task AddUsers(bool clear = false, int limit = 20, int offset = 0)
    {
        if (clear) Users.Clear();

        foreach (var user in await _requestService.GetUsersAsync(offset, limit))
        {
            Users.Add(user);
        }
    }
    public async Task SearchUser(string text)
    {
        UserViewModel found = await _requestService.GetUserByIdAsync(text);

        if (found is not null)
        {
            Users.Add(found);
        }
    }

}
