namespace FirstResponsiveWebAppHey.Models
{
    public class Student
    {
        public string FirstName {get; set;} = string.Empty;
        public string LastName {get; set;} = string.Empty;
        public string Grade {get; set;} = string.Empty;
    }

    public class StudentViewModel
    {
        public List<Student> Students {get; set;} = new();
        public int AccessLevel {get; set;}
    }
}
