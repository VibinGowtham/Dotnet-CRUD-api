using System;
namespace MVCapp.Models
{
	public class Student
	{
		public Student(int id,string name,int age,int mark)
		{
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Mark = mark;
		}
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public int? Mark { get; set; }
    }
}

