using System;

namespace GitCommands
{
    public class DateTimeHelper
    {
        public static string TimeToString(DateTime time, bool useRelativeDate)
        {
            if (time == DateTime.MinValue || time == DateTime.MaxValue)
                return "";

            if (!useRelativeDate)
                return String.Format("{0} {1}", time.ToShortDateString(), time.ToLongTimeString());


            var span = DateTime.Now - time;
            
            #region Relative time output note
            /*
            To summarise, the output always rounds down the relative time. eg. 2.9 days = "2 days ago"
            The following table describes the output in detail:
            
            displayed  |                 |  time unit for
             output    |  time interval  |  time interval
           ------------+-----------------+-------------------
            0 seconds     [0.0, 1.0)        seconds
            1 second      [1.0, 2.0)        seconds
            n seconds     [2.0, 60.0)       seconds

            1 minute      [1.0, 2.0)        minutes
            n minutes     [2.0, 60.0)       minutes

            1 hour        [1.0, 2.0)        hours
            n hours       [2.0, 24.0)       hours

            1 day         [1.0, 2.0)        days
            n days        [2.0, 30.0)       days

            1 month       [30.0, 60.0)      days
            n months      [60.0, 365.0)     days

            1 year        [365.0, 730.0)    days
            n years       [730.0, inf)      days
            
             */
            #endregion
            if (span.TotalMinutes < 1.0)
            {
                if (span.Seconds == 1)
                    return String.Format(Strings.Get1SecondAgoText(), "1");
                return String.Format(Strings.GetNSecondsAgoText(), span.Seconds);
            }

            if (span.TotalHours < 1.0)
            {
                if (span.Minutes == 1)
                    return String.Format(Strings.Get1MinuteAgoText(), "1");
                return String.Format(Strings.GetNMinutesAgoText(), span.Minutes);
            }

            if (span.TotalHours < 24.0)
            {
                if (span.Hours == 1)
                    return String.Format(Strings.Get1HourAgoText(), "1");
                else
                    return String.Format(Strings.GetNHoursAgoText(), span.Hours);
            }

            if (span.TotalDays < 30.0)
            {
                if (span.Days == 1)
                    return String.Format(Strings.Get1DayAgoText(), "1");
                else
                    return String.Format(Strings.GetNDaysAgoText(), span.Days);
            }

            if (span.TotalDays < 365.0)
            {
                if (span.Days < 60)
                    return String.Format(Strings.Get1MonthAgoText(), "1");
                else    // 30.417 = 365 days / 12 months - note that the if statement only bothers with 30 days for "1 month ago" because span.Days is int.
                    return String.Format(Strings.GetNMonthsAgoText(), (int)(span.TotalDays / 30.417));  // round down
            }

            if (span.TotalDays < 730.0)  // less than 2.0 years = "1 year"
                return String.Format(Strings.Get1YearAgoText(), "1");
            else
                return String.Format(Strings.GetNYearsAgoText(), (int)(span.TotalDays / 365.0));        // round down
        }
    }
}