using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExamPlatform.Models;
using System.Globalization;
using ExamPlatformDataModel;
using ExamPlatform.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ExamPlatform.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (var context = new ExamPlatformDbContext())
            {
                if (!context.Course.Any())
                {
                    Accounts admin = new Accounts()
                    {
                        Name = "Grzegorz",
                        Surname = "Glonek",
                        Email = "grzegorz@glonek.net.pl",
                        Username = "admin",
                        Password = "212A217E44A69108085566D1631F56C7", //"Egzaminowanie_123",
                        Status = "ADMIN"
                    };
                    Course Architecture = new Course()
                    {

                        CourseType = "Architecure .NET",
                        ClosedQuestionsList = new List<ClosedQuestions>()
                {
                    new ClosedQuestions()
                    {
                        Question = "Czym jest Common Language Specification (CLS) ?",
                        ProperAnswer = "Zbiór zasad i reguł określających jak kod źródłowy zamienić na kod pośredni (IL)",
                        Answer_1="Określenie najpopularniejszego języka używanego na platformie .Net",
                        Answer_2="Zbiór zasad i reguł określających jak kod źródłowy zamienić na kod pośredni (IL)",
                        Answer_3="Mechanizmem zarządzania pamięcią",
                        Answer_4="Kompilatorem języka C#",
                    },
                    new ClosedQuestions()
                    {
                        Question = "Do jakich elementów odnoszą się reguły CLS ?",
                        ProperAnswer = "Tylko do publicznych typów i metod",
                        Answer_1="Do cyklu życia aplikacji",
                        Answer_2="Do wszystkich pól i metod w klasie",
                        Answer_3="Do strategii zwalniania miejsca w pamięci operacyjnej",
                        Answer_4="Tylko do publicznych typów i metod"
                    },
                      new ClosedQuestions()
                    {
                        Question = "Za co odpowiada Just-In-Time (JIT) Compiler ?",
                        ProperAnswer = "Kompiluje kod pośredni do kodu natywnego w trakcie działania aplikacji",
                        Answer_1="Kompiluje kod źródłowy do kodu pośredniego w czasie kompilacji",
                        Answer_2="Kontroluje bezpieczeństwo wykonywania kodu",
                        Answer_3="Kompiluje kod pośredni do kodu natywnego w trakcie działania aplikacji",
                        Answer_4="Kompiluje kod źródłowy do kodu natywnego w czasie kompilacji"
                    },
                        new ClosedQuestions()
                    {
                        Question = "AppDomain to:",
                        ProperAnswer = "Grupa wyizolowanych zasobów przydzielonych aplikacji, posiadająca swoją własną przestrzeń adresową",
                        Answer_1="Interfejs komunikacyjny pomiędzy dwiema aplikacjami na platformie .Net",
                        Answer_2="Mechanizm pozwalający odczytać definicję typu w trakcie działania aplikacji",
                        Answer_3="Grupa wyizolowanych zasobów przydzielonych aplikacji, posiadająca swoją własną przestrzeń adresową",
                        Answer_4="Mechanizm pozwalający uruchamiać kod natywny"
                    },
                          new ClosedQuestions()
                    {
                        Question = "Za jaki obszar pamięci odpowiada Garbage Collector ?",
                        ProperAnswer = "Sterta",
                        Answer_1="Sterta",
                        Answer_2="Żaden z wymienionych obszarów",
                        Answer_3="Za Stos i Sterte",
                        Answer_4="Stos"
                    },
                            new ClosedQuestions()
                    {
                        Question = "Ile generacji kodu rozróżnia GC ?",
                        ProperAnswer = "3",
                        Answer_1="2",
                        Answer_2="3",
                        Answer_3="4",
                        Answer_4="1"
                    },
                              new ClosedQuestions()
                    {
                        Question = "Co to jest Base Class Library (BCL) ?",
                        ProperAnswer = "Zestaw typów bazowych i prymitywów pozwalających na budowanie innych, wyspecjalizowanych typów danych",
                        Answer_1="Mechanizm umożliwiający interpretację i wykonywanie dynamicznych typów danych",
                        Answer_2="Klasy odpowiedzialne za automatyczne zarządzanie pamięcią",
                        Answer_3="Zestaw wszystkich typów wykorzystywanych na platformie .Net",
                        Answer_4="Zestaw typów bazowych i prymitywów pozwalających na budowanie innych, wyspecjalizowanych typów danych"
                    }
                },

                        OpenedQuestionsList = new List<OpenedQuestions>()
                {
                    new OpenedQuestions()
                    {
                        Question ="Czym jest Common Intermediate Language (CIL) ?",
                        MaxPoints = 2,

                    },
                     new OpenedQuestions()
                    {
                        Question ="W jakich dwóch przypadkach uruchamia się GC ?",
                        MaxPoints = 4,

                    },

                      new OpenedQuestions()
                    {
                        Question ="Czym charakteryzują się kompilatory JIT ?",
                        MaxPoints = 3,

                    },

                       new OpenedQuestions()
                    {
                        Question ="Co to jest Common Language Runtime (CLR) ?",
                        MaxPoints = 2,

                    },

                        new OpenedQuestions()
                    {
                        Question ="Jakie obiekty klasyfikowane są przez GC jako Generacja ?",
                        MaxPoints = 2,
                    },

                         new OpenedQuestions()
                    {
                        Question ="Co to jest Boxing i Unboxing ?",
                        MaxPoints = 1,

                    }
                }

                    };
                    Course Management = new Course()
                    {

                        CourseType = "Project Management",
                        ClosedQuestionsList = new List<ClosedQuestions>()
                        {
                            new ClosedQuestions()
                            {
                                Question = "Ile procesów (wg PMI) składa się na zarządzanie projektami ?",
                                ProperAnswer = "5",
                                Answer_1="4",
                                Answer_2="5",
                                Answer_3="6",
                                Answer_4="7",
                            },
                            new ClosedQuestions()
                            {
                                Question = "W którym z procesów odbywa się określenie zakresu i charakteru projektu ?",
                                ProperAnswer = "Planowanie",
                                Answer_1="Planowanie",
                                Answer_2="Wykonanie",
                                Answer_3="Prototypowanie",
                                Answer_4="Żaden z powyższych",
                            },
                              new ClosedQuestions()
                            {
                                Question = "Na czym polega proces kontroli postępów ?",
                                ProperAnswer = "Wszystkie z powyższych",
                                Answer_1="Na sprawdzeniu gdzie jesteśmy w projekcie, a gdzie powinniśmy być",
                                Answer_2="Na przygotowaniu planu powrotu do planu realizacji projektu (o ile jest to potrzebne)",
                                Answer_3 = "Na określeniu jak szybko zgłoszone problemy są rozwiązywane",
                                Answer_4 = "Wszystkie z powyższych",
                            },
                                new ClosedQuestions()
                                {
                                    Question = "Na jakich trzech filarach/wartościach opiera się metodyka Scrum ?",
                                    ProperAnswer = "Przejrzystość, inspekcja, adaptacja",
                                    Answer_1 = "Przejrzystość, inspekcja, adaptacja",
                                    Answer_2 = "Zespoły, artefakty, zdarzenia",
                                    Answer_3 = "Backlog, inkrement, definitione of done",
                                    Answer_4 = "Żadne z powyższych",
                                },
                                  new ClosedQuestions()
                                  {
                                      Question = "Jaki element Scruma reprezentuje konkretną pracę oraz wartości pozwalające uzyskać przejrzystość projektu ? ",
                                ProperAnswer = "Artefakt",
                                      Answer_1 = "Artefakt",
                                      Answer_2 = "Zespół",
                                      Answer_3 = "Zdarzenie",
                                      Answer_4 = "Żadne z powyższych",
                                  },
                                    new ClosedQuestions()
                                    {
                                        Question = "Kto jest członkiem zespołu Scrumowego ?",
                                        ProperAnswer = "Product owner",
                                        Answer_1 = "Team leader",
                                        Answer_2 = "Product owner",
                                        Answer_3 = "Project manager",
                                        Answer_4 = "Każdy z powyższych",
                                    },
                                      new ClosedQuestions()
                                      {
                                          Question = "Za co odpowiada Scrum Master (SM) ?",
                                          ProperAnswer = "Zrozumienie i stosowanie Scruma",
                                          Answer_1 = "Dostarczenie gotowego do publikacji inkrementu",
                                          Answer_2 = "Maksymalizację wartości produktu i pracy zespołu deweloperskiego",
                                          Answer_3 = "Zrozumienie i stosowanie Scruma",
                                          Answer_4 = "Każdy z powyższych",
                                      },
                                      new ClosedQuestions()
                                      {
                                          Question = "Które ze zdarzeń Scrumowych nie może się zakończyć przed ściśle ustalonym terminem ? ",
                                          ProperAnswer = "Retrospekcja",
                                          Answer_1 = "Sprint",
                                          Answer_2 = "Daily",
                                          Answer_3 = "Retrospekcja",
                                          Answer_4 = "Każdy z powyższych",
                                      }
                        },

                        OpenedQuestionsList = new List<OpenedQuestions>()
                        {
                            new OpenedQuestions()
                            {
                                Question ="Co to jest metodyka zarządzania projektami ?",
                                MaxPoints = 2,

                            },
                             new OpenedQuestions()
                            {
                                Question ="Podaj po dwa przykłady metodyk ciężkich (klasycznych) oraz lekkich (zwinnych)",
                                MaxPoints = 4,

                            },

                              new OpenedQuestions()
                            {
                                Question ="W jaki sposób (jednym słowem) następują po sobie kolejne fazy w metodyce Waterfall",                                MaxPoints = 3,

                            },

                               new OpenedQuestions()
                            {
                                Question ="Wskaż dwie zalety (realne lub teoretyczne) metodyki Waterfall",
                                MaxPoints = 2,

                            },

                                new OpenedQuestions()
                            {
                                Question =" Co to jest timebox ?",
                                MaxPoints = 2,
                            },

                                 new OpenedQuestions()
                            {
                                Question ="Projekt to . . .  przedsięwzięcie podjęte, aby stworzyć . . . produkt, usługę lub",                                 MaxPoints = 1,

                            }
                        }

                    };
                    context.Add(Architecture);
                    context.Add(Management);
                    context.Account.Add(admin);
                    context.SaveChanges();
                }
}




DateTime loaclDate = DateTime.Now;
                CultureInfo eng = new CultureInfo("en-US");

                var dates = loaclDate.DayOfWeek.ToString() + ", " + loaclDate.Day.ToString("d2") + " " + loaclDate.ToString("MMMM", eng);

                return View("Index",dates);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
