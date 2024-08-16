using GetServise.Data;
using GetServise.Models;

namespace GetServise.Services
{
    public class CalendarService
    {
        private readonly DataContext _context;
        public CalendarService(DataContext context)
        {
            _context=context;
        }

        public async Task<string> GetStringFromOutside(int year) {
            if (!_context.Calendars.Any(p => p.Date.Year == year))
            {
                string jsonResponse;
                using (HttpClient client = new HttpClient())
                {
                    using HttpResponseMessage response = await client.GetAsync(@$"https://isdayoff.ru/api/getdata?year={year}&pre=1");
                    jsonResponse = await response.Content.ReadAsStringAsync();
                }
                return jsonResponse;
            }
            return null;
        }

        public List<Calendar> ParseDay(string jsonResponse, int year)
        {
            var listDay = new Calendar[jsonResponse.Length];
            var newDate = new DateTime(year, 1, 1);

            for (int i = 0; i < jsonResponse.Length; i++)
            {
                switch (jsonResponse[i].ToString())
                {
                    case "0":
                        listDay[i] = new Calendar()
                        {
                            Date = newDate,
                            DayOfWeek = newDate.DayOfWeek.ToString(),
                            TypeOfDay = TypeOfDay.Work
                        };
                        break;
                    case "1":
                        listDay[i] = new Calendar()
                        {
                            Date = newDate,
                            DayOfWeek = newDate.DayOfWeek.ToString(),
                            TypeOfDay = TypeOfDay.Weekend
                        };
                        break;
                    case "2":
                        listDay[i] = new Calendar()
                        {
                            Date = newDate,
                            DayOfWeek = newDate.DayOfWeek.ToString(),
                            TypeOfDay = TypeOfDay.ShortDay
                        };
                        break;
                    default:
                        break;
                }
                newDate = newDate.AddDays(1);
            }
            return listDay.ToList();
        }
    }
}
