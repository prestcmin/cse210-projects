using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

public class Job
    {
        public string _company;
        public string _jobTitle;
        public int _startYear; 
        public int _endYear;

        public void DisplayJobInformation()
        {
            Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
        }
    }
