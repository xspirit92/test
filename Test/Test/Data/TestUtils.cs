using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Test.Models;

namespace Test
{
    public class TestUtils
    {
        public static Tasks ParseDate(Tasks task)
        {
            try
            {
                List<string> months = new List<string>() { "января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря" };
                List<string> days = new List<string>() { "завтра", "сегодня", "послезавтра" };


                if (task.Deadline != null) return task;

                foreach (string el in days)
                {
                    if (task.Task.IndexOf(el) > -1)
                    {
                        if (el == "завтра") { task.Deadline = DateTime.Now + new TimeSpan(1, 0, 0, 0); return task; }
                        if (el == "сегодня") { task.Deadline = DateTime.Now; return task; }
                        if (el == "послезавтра") { task.Deadline = DateTime.Now + new TimeSpan(2, 0, 0, 0); return task; }
                    }
                }

                string pattern = @"\d+";
                Regex r = new Regex(pattern);
                Match m = r.Match(task.Task);
                while (m.Success)
                {
                    if (m.Length <= 2)
                    {
                        if (m.Index + m.Length + 1 < task.Task.Length)
                        {
                            int startIdx = m.Index + m.Length + 1;
                            for (int i = 0; i < months.Count; i++)
                            {
                                if (task.Task.IndexOf(months[i], startIdx, StringComparison.CurrentCultureIgnoreCase) == startIdx)
                                {
                                    DateTime deadline = new DateTime(DateTime.Now.Year, i + 1, int.Parse(m.ToString()));
                                    task.Deadline = deadline;
                                    return task;
                                }
                            }
                        }
                    }

                    m = m.NextMatch();
                }

                return task;
            }
            catch
            {
                return task;
            }
        }
    }
}