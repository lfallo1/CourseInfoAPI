using System;

namespace CourseInfoAPI.Helpers
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTimeOffset.Year;
            if (currentDate.DayOfYear < dateTimeOffset.DayOfYear)
            {
                age--;
            }
            return age;
        }
    }
}
