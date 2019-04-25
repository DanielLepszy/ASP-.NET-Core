using ExamPlatformDataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitDatabaseTests.ControllersUnitTests.FRepositories
{
    public class FakeCourseQuestions
    {
        public Course Architecture = new Course()
        {

            CourseType = "Architecure .NET",
            ClosedQuestionsList = new List<ClosedQuestions>()
                    {
                   //new ClosedQuestions()
                   //     {
                   //         Question = "Czym jest Common Language Specification (CLS)",
                   //         ProperAnswer = "Zbiór zasad i reguł określających jak kod źródłowy zamienić na kod pośredni (IL)",
                   //         Answer_1="Określenie najpopularniejszego języka używanego na platformie .Net",
                   //         Answer_2="Zbiór zasad i reguł określających jak kod źródłowy zamienić na kod pośredni (IL)",
                   //         Answer_3="Mechanizmem zarządzania pamięcią",
                   //         Answer_4="Kompilatorem języka C#",
                   //     },
                   //     new ClosedQuestions()
                   //     {
                   //         Question = "Do jakich elementów odnoszą się reguły CLS",
                   //         ProperAnswer = "Tylko do publicznych typów i metod",
                   //         Answer_1="Do cyklu życia aplikacji",
                   //         Answer_2="Do wszystkich pól i metod w klasie",
                   //         Answer_3="Do strategii zwalniania miejsca w pamięci operacyjnej",
                   //         Answer_4="Tylko do publicznych typów i metod"
                   //     },
                   //       new ClosedQuestions()
                   //     {
                   //         Question = "Za co odpowiada Just-In-Time (JIT) Compiler",
                   //         ProperAnswer = "Kompiluje kod pośredni do kodu natywnego w trakcie działania aplikacji",
                   //         Answer_1="Kompiluje kod źródłowy do kodu pośredniego w czasie kompilacji",
                   //         Answer_2="Kontroluje bezpieczeństwo wykonywania kodu",
                   //         Answer_3="Kompiluje kod pośredni do kodu natywnego w trakcie działania aplikacji",
                   //         Answer_4="Kompiluje kod źródłowy do kodu natywnego w czasie kompilacji"
                   //     },
                   //         new ClosedQuestions()
                   //     {
                   //         Question = "AppDomain to:",
                   //         ProperAnswer = "Grupa wyizolowanych zasobów przydzielonych aplikacji, posiadająca swoją własną przestrzeń adresową",
                   //         Answer_1="Interfejs komunikacyjny pomiędzy dwiema aplikacjami na platformie .Net",
                   //         Answer_2="Mechanizm pozwalający odczytać definicję typu w trakcie działania aplikacji",
                   //         Answer_3="Grupa wyizolowanych zasobów przydzielonych aplikacji, posiadająca swoją własną przestrzeń adresową",
                   //         Answer_4="Mechanizm pozwalający uruchamiać kod natywny"
                   //     },
                              new ClosedQuestions()
                        {
                            Question = "Za jaki obszar pamięci odpowiada Garbage Collector",
                            ProperAnswer = "Sterta",
                            Answer_1="Sterta",
                            Answer_2="Żaden z wymienionych obszarów",
                            Answer_3="Za Stos i Sterte",
                            Answer_4="Stos"
                        },
                                new ClosedQuestions()
                        {

                            Question = "Ile generacji kodu rozróżnia GC",
                            ProperAnswer = "3",
                            Answer_1="2",
                            Answer_2="3",
                            Answer_3="4",
                            Answer_4="1"
                        },
                                  new ClosedQuestions()
                        {
                               Question = "Co to jest Base Class Library (BCL)",
                            ProperAnswer = "3",
                            Answer_1="2",
                            Answer_2="4",
                            Answer_3="5",
                            Answer_4="3"
                        },
                                    new ClosedQuestions()
                        {

                            Question = "Ile generacji kodu rozróżnia GC",
                            ProperAnswer = "3",
                            Answer_1="2",
                            Answer_2="3",
                            Answer_3="4",
                            Answer_4="1"
                        },
                                      new ClosedQuestions()
                        {

                            Question = "Ile generacji kodu rozróżnia GC",
                            ProperAnswer = "3",
                            Answer_1="2",
                            Answer_2="3",
                            Answer_3="4",
                            Answer_4="1"
                        },
                                        new ClosedQuestions()
                        {

                            Question = "Ile generacji kodu rozróżnia GC",
                            ProperAnswer = "3",
                            Answer_1="2",
                            Answer_2="3",
                            Answer_3="4",
                            Answer_4="1"
                        },

                    },

            OpenedQuestionsList = new List<OpenedQuestions>()
                    {
                        new OpenedQuestions()
                        {
                            Question ="Czym jest Common Intermediate Language (CIL)",
                            MaxPoints = 2,

                        },
                         new OpenedQuestions()
                        {
                            Question ="W jakich dwóch przypadkach uruchamia się GC",
                            MaxPoints = 4,

                        },
                          new OpenedQuestions()
                        {
                            Question ="Czym charakteryzują się kompilatory JIT",
                            MaxPoints = 3,

                        },
                           new OpenedQuestions()
                        {
                            Question ="Co to jest Common Language Runtime (CLR)",
                            MaxPoints = 2,

                        },

                            new OpenedQuestions()
                        {
                            Question ="Jakie obiekty klasyfikowane są przez GC jako Generacja",
                            MaxPoints = 2,
                        },

                             new OpenedQuestions()
                        {
                            Question ="Co to jest Boxing i Unboxing",
                            MaxPoints = 1,
                        }
                    }
        };
    }
}
