using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace yield;

public static class ExpSmoothingTask
{
	public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
    {
         DataPoint prev = null;

         foreach (var point in data)
         {
             if (prev == null)
             {
                 prev = point.WithExpSmoothedY(point.OriginalY);
                 yield return point.WithExpSmoothedY(point.OriginalY); ;
             }
             else
            { 
                 var smoothed = alpha *point.OriginalY + ((double)1-alpha)*prev.ExpSmoothedY;
                 prev = point.WithExpSmoothedY(smoothed);
                 yield return point.WithExpSmoothedY(smoothed);
             }
         }
         
    }
}
