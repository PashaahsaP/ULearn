using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace linq_slideviews;

public class ParsingTask
{
    private static string[] typeSlides = new[] { "theory", "exercise", "quiz" };
    /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
    /// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
    /// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>

    public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
    {
        return lines
            .Skip(1)
            .Select(x => x.Split(';'))
            .Select(x => ParseSlide(x))
            .Where(x => x != null)
            .ToDictionary(x => x.Item1, y => new SlideRecord(y.Item1, y.Item2, y.Item3));
    }
    
    /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
    /// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
    /// Такой словарь можно получить методом ParseSlideRecords</param>
    /// <returns>Список информации о посещениях</returns>
    /// <exception cref="FormatException">Если среди строк есть некорректные</exception>
    public static IEnumerable<VisitRecord> ParseVisitRecords(
        IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
    {
        return lines
            .Skip(1)
            .Select(x => ParseVisit(x, slides))
            .Where(x=>x != null);

    }

    #region HelperMethods
    static VisitRecord ParseVisit(string visit, IDictionary<int, SlideRecord> slides)
    {
        var newX = visit.Split(';');
        try
        {
            //if (newX[0] != "" && newX[1] != "" && newX[2] != "" && newX[3] != "")
                return new VisitRecord(int.Parse(newX[0]), int.Parse(newX[1]),
                    DateTime.ParseExact($"{newX[2]} {newX[3]}",
                        "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None),
                        slides.FirstOrDefault(x=>x.Key== int.Parse(newX[1])).Value.SlideType);
        }
        catch (Exception e)
        {
            throw new FormatException($"Wrong line [{visit}]");
        }

    }
    static Tuple<int, SlideType, string> ParseSlide(string[] slides)
    {
        if (int.TryParse(slides[0], out int id) && slides.Length == 3
                                                && typeSlides.Any(x => x == slides[1]))
        {
            var type = slides[1] switch
            {
                "theory" => SlideType.Theory,
                "exercise" => SlideType.Exercise,
                "quiz" => SlideType.Quiz,
            };
            return Tuple.Create(id, type, slides[2]);
        }
        return null;
    }
    #endregion

}