using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using Sk = LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace NewsMobileApp.ViewModels;

public class AdminViewModel
{
    private static ObservableCollection<DateTimePoint> GetDiagtamData()
    {
        DateTime startDate = DateTime.Now.AddDays(-6);
        DateTime endDate = DateTime.Now;
        return new ObservableCollection<DateTimePoint>(Enumerable.Range(0, (int)(endDate - startDate).TotalDays + 1)
                         .Select(x => new DateTimePoint(startDate.AddDays(x), Random.Shared.Next(2, 10))));
    }

    public ISeries[] Series { get; set; } = new ISeries[]
        {
            new LineSeries<DateTimePoint>
            {
                Values = GetDiagtamData(),
                Stroke = new Sk.LinearGradientPaint(new[]{ new SKColor(46, 34, 172), new SKColor(203, 71, 157)}) { StrokeThickness = 10 },
                GeometryStroke = new Sk.LinearGradientPaint(new[]{ new SKColor(46, 34, 172), new SKColor(203, 71, 157) }) { StrokeThickness = 10 },
                Fill = new Sk.LinearGradientPaint( new[] { new SKColor(46, 34, 172), new SKColor(203, 71, 157) },
                new SKPoint(0.1f, 0),
                new SKPoint(1, 0.9f))

                
            }
        };
}
