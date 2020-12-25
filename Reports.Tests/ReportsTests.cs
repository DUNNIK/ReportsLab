using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ReportsLab.BusinessLogicLayer.EmployeeSystem;
using ReportsLab.BusinessLogicLayer.TaskManagementSystem;

namespace Reports.Tests
{
    [TestFixture]
    public class Tests
    {
        private TeamLead _teamLead;
        private Director _director;
        private Employee _employee1;
        private Employee _employee2;
        private string _physicsTaskId;
        [SetUp]
        public void Setup()
        {
            _teamLead = new TeamLead("Nikita", new List<ISubordinate>());
            _director = new Director("Nail", _teamLead, new List<ISubordinate>());
            _employee1 = new Employee("Nan", _director);
            _employee2 = new Employee("Non", _director);
            _director.TransferEmployeeToAnotherDirector(_employee2, _teamLead);

            _physicsTaskId = _director.CreateTask("Make Physics", "Shit");
            
        }

        [Test]
        public void CreateNewTaskAndOpen_1TaskOpen_CorrectString()
        {
            var id = _teamLead.CreateTask("Task", "Task for create test");
            _teamLead.OpenTask(id);
            Assert.That(_director.GetTask(id).ToString(), Is.EqualTo("Task task\nState: Open\nTask for create test\n"));
        }

        [Test]
        public void Task_ResolveTask_CorrectStatus()
        {
            _director.OpenTask(_physicsTaskId);
            _director.ActiveTask(_physicsTaskId);
            _director.ResolveTask(_physicsTaskId);
            Assert.That(_director.GetTask(_physicsTaskId).Status(), Is.EqualTo(Task.State.Resolved));
        }

        [Test]
        public void Report_EmployeeCreateReport_CorrectFile()
        {
            var id = _teamLead.CreateTask("Task", "Task for create test");
            _director.UpdateTaskEmployee(id, _employee1);
            _employee1.OpenTask(id);
            _employee1.ActiveTask(id);
            _employee1.CreateCommit(id, "Almost done");
            _employee1.ResolveTask(id);
            _employee1.CreateDayReport("Test.txt");
            string fileData1;
            using (var sr = new StreamReader(@"D:\ООП\Test.txt"))
            {
                fileData1 = sr.ReadToEnd();
            }
            Assert.That(fileData1, Is.EqualTo("Task task\nState: Resolved\nTask for create test\n"));
        }
            
        [Test]
        public void CreateReport_TeamLeadAndDirectorCreateReports_CorrectPhysicalFiles()
        {
            _director.OpenTask(_physicsTaskId);
            _director.ActiveTask(_physicsTaskId);
            _director.ResolveTask(_physicsTaskId);
            _director.CreateDayReport("DirectorDayReport.txt");
            _teamLead.CreateSprintReport("OneTaskReportFromTeamLead.txt");
            string fileData1;
            using (var sr = new StreamReader(@"D:\ООП\OneTaskReportFromTeamLead.txt"))
            {
                fileData1 = sr.ReadToEnd();
            }
            string fileData;
            using (var sr = new StreamReader(@"D:\ООП\DirectorDayReport.txt"))
            {
                fileData = sr.ReadToEnd();
            }
            Assert.That(fileData1, Is.EqualTo("Make Physics task\nState: Resolved\nShit\n"));
            Assert.That(fileData, Is.EqualTo("Make Physics task\nState: Resolved\nShit\n"));
        }

        [Test]
        public void TasksByLastChangeTime_Wait2Seconds_1Task()
        {
            _director.ActiveTask(_physicsTaskId);
            var twoSecondsAgo = DateTime.Now;
            
            Thread.Sleep(2000);
            
            Assert.That(_director.TasksByChangeCreateTime(twoSecondsAgo).Count, Is.EqualTo(1));
            Assert.That(_director.TasksByChangeCreateTime(DateTime.Now).Count, Is.EqualTo(0));
        }

        [Test]
        public void UpdateTasks_DirectorAddEmployee1ToTask_Employee1Task()
        {
            _director.UpdateTaskEmployee(_physicsTaskId, _employee1);
            Assert.That(_employee1.MyTasks().Last(), Is.EqualTo(_employee1.GetTask(_physicsTaskId)));
        }
    }
}