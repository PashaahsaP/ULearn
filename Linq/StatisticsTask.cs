using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public class StatisticsTask
{
    public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
    {
        if (visits.Count == 0) return 0;
        return visits
            .GroupBy(q => q.UserId)
            .Select(x => x.OrderBy(x => x.DateTime)
                .Bigrams())
            .Select(x => GetTimes(slideType, x, time => time >= 1 && time <= 120))
            .SelectMany(x => x)
            .DefaultIfEmpty()
            .Median();
    }

    private static List<double> GetTimes(SlideType slideType, IEnumerable<(VisitRecord First, VisitRecord Second)> x,
        Func<double, bool> predicate)
    {
        var timeList = new List<double>();

        foreach (var pairs in x)
        {
            var time = (pairs.Second.DateTime - pairs.First.DateTime).TotalMinutes;
            if (pairs.First.SlideType == slideType && predicate(time))
                timeList.Add(time);
        }

        return timeList;
    }
}