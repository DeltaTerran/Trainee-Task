using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainee_Task.Data;
using Trainee_Task.Models;

namespace Trainee_Task.Controllers
{
    public class PeopleController : Controller
    {
        private readonly DataBaseContext _context;
        public PeopleController(DataBaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _context.People.ToListAsync();
            return View(people);
        }


        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var list = new List<PersonModel>();

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    string line;
                    bool isFirstLine = true;

                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (isFirstLine) // пропускаем заголовок
                        {
                            isFirstLine = false;
                            continue;
                        }

                        var columns = line.Split(';'); // или ','

                        if (columns.Length < 3) continue; // пропуск некорректных строк

                        list.Add(new PersonModel
                        {
                            Name = columns[1],
                            Birthday = StringToDate(columns[2]),
                            Maried = columns[3].ToLower() == "true".ToLower() ? true : false,
                            Phone = columns[4],
                            Salary = decimal.Parse(columns[5])
                        });
                    }
                }

                // Покажем данные пользователю перед сохранением
                TempData["CsvData"] = System.Text.Json.JsonSerializer.Serialize(list);
                return RedirectToAction("Preview");
            }

            return View();
        }


        [HttpGet]
        public IActionResult Preview()
        {
            if (TempData["CsvData"] is string json)
            {
                TempData.Keep("CsvData"); // ← сохраняем данные еще на один запрос
                var list = System.Text.Json.JsonSerializer.Deserialize<List<PersonModel>>(json);
                return View(list);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SaveToDb()
        {
            if (TempData["CsvData"] is string json)
            {
                var list = System.Text.Json.JsonSerializer.Deserialize<List<PersonModel>>(json);

                _context.People.AddRange(list);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }



        private DateTime StringToDate(string str)
        {
            int[] DateNumbers = new int[3];
            DateNumbers = str.Split(".").Select(int.Parse).ToArray();
            return new DateTime(DateNumbers[2], DateNumbers[1], DateNumbers[0]);
        }
    }
}
