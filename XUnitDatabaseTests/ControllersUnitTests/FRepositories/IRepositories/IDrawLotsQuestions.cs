using ExamPlatformDataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitDatabaseTests.ControllersUnitTests.FRepositories.IRepositories
{
    interface IDrawLotsQuestions
    {
        List<ClosedQuestions> SelectClosedQuestions(List<ClosedQuestions> AllQuestionsFromDB, int AmountOfSelecetedQuestions);
        List<OpenedQuestions> SelectOpenedQuestions(List<OpenedQuestions> AllQuestionsFromDB, int AmountOfSelecetedQuestions);
    }
}
