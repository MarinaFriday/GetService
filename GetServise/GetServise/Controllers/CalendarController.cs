using GetServise.Data;
using GetServise.Models;
using GetServise.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetServise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly DataContext _context;
        private CalendarService calendarService;
        public CalendarController(DataContext context)
        {
            _context = context;            
            calendarService = new CalendarService(context);
        }

        //GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar>>> Get(int year)
        {
            var getStringFromOutside = await calendarService.GetStringFromOutside(year);
            if (getStringFromOutside != null)
            {
                var listCalendar = calendarService.ParseDay(getStringFromOutside, year);
                _context.Calendars.AddRange(listCalendar);
                await _context.SaveChangesAsync();
            }
            return Ok($"Календарь с годом {year} заполнен");
        }

        //[HttpGet("GetTypeOfDay")]
        //public async Task<ActionResult<IEnumerable<Calendar>>> GetTypeOfDay(int dd, int mm, int yyyy)
        //{
        //    var day = _context.Calendars.Where(d => d.Date.Day == dd && d.Date.Month == mm && d.Date.Year == yyyy).FirstOrDefault();
        //    if(day.DayOfWeek != null) return Ok($"{day.DayOfWeek}");
        //    return Ok($"{dd}.{mm}.{yyyy} не найдено");
        //}

        [HttpGet("GetTypeOfDay")]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetTypeOfDay(string date)
        {
            string[] dayPart = date.Split('.');
            int dd = Int32.Parse(dayPart[0]);
            int mm = Int32.Parse(dayPart[1]);
            int yyyy = Int32.Parse(dayPart[2]);
            var day = _context.Calendars.Where(d => d.Date.Day == dd && d.Date.Month == mm && d.Date.Year == yyyy).FirstOrDefault();
            if (day.DayOfWeek != null) return Ok($"{day.TypeOfDay}");
            return Ok($"{dd}.{mm}.{yyyy} не найдено");
        }
    }
}
