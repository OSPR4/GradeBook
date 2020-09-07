
using System;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;

        }
    }

    public class Book : NamedObject
    {
        private List<double> grades;
        public event GradeAddedDelegate GradeAdded;

    

        public Book(string name) : base(name)
        {
            Name = name;
            grades = new List<double>();
        }

        public void AddGrade(double grade)
        {
            if(grade <= 100 && grade >= 0)
            {
              grades.Add(grade); 
              if(GradeAdded != null)
              {
                  GradeAdded(this, new EventArgs());
              }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
            
        }

        public void AddGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }

        public Stats GetStats()
        {
            var result = new Stats();
            result.Average = 0.0;
            result.Highest = double.MinValue;
            result.Lowest = double.MaxValue;

            for(int index = 0; index < grades.Count; index++)          
            {
                result.Highest = Math.Max(grades[index], result.Highest);
                result.Lowest = Math.Min(grades[index], result.Lowest);
                result.Average += grades[index];

            }
            result.Average /= grades.Count;
           

            switch(result.Average)
            {
                case var d when d >= 90.0:
                    result.Letter = 'A';
                    break;
                case var d when d >= 80.0:
                    result.Letter = 'B';
                    break;
                case var d when d >= 70.0:
                    result.Letter = 'C';
                    break;
                case var d when d >= 60.0:
                    result.Letter = 'D';
                    break;
                default:
                    result.Letter = 'F';
                    break;
            }
             return result;
        }
    }
}