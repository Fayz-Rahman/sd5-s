using System;
using System.Collections.Generic;
using System.IO;

class Student {
    public string ID { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public string Semester { get; set; }
}

class Program {
    static List < Student > students = new List < Student > ();
    const string filePath = "students.csv";

    static void Main(string[] args) {
        LoadData();

        bool running = true;

        while (running) {
            Console.WriteLine("\nSTUDENT MANAGEMENT SYSTEM");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View Students");
            Console.WriteLine("3. Search Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Exit");
            Console.Write("Choose option: ");

            string choice = Console.ReadLine();

            switch (choice) {
            case "1":
                AddStudent();
                break;
            case "2":
                ViewStudents();
                break;
            case "3":
                SearchStudent();
                break;
            case "4":
                DeleteStudent();
                break;
            case "5":
                running = false;
                break;
            default:
                Console.WriteLine("wrong option typed");
                break;
            }
        }
    }

    static void AddStudent() {
        Student newStudent = new Student();

        Console.Write("Enter ID: ");
        newStudent.ID = Console.ReadLine();

        if (newStudent.ID == "") {
            Console.WriteLine("id is empty");
            return;
        }

        Console.Write("Enter Name: ");
        newStudent.Name = Console.ReadLine();

        Console.Write("Enter Department: ");
        newStudent.Department = Console.ReadLine();

        Console.Write("Enter Semester: ");
        newStudent.Semester = Console.ReadLine();

        students.Add(newStudent);
        Console.WriteLine("Student added successfully!");

        SaveData();
    }

    static void ViewStudents() {
        if (students.Count == 0) {
            Console.WriteLine("list is empty");
            return;
        }

        Console.WriteLine("\nStudent List");
        foreach(Student s in students) {
            Console.WriteLine($"ID: {s.ID}");
            Console.WriteLine($"Name: {s.Name}");
            Console.WriteLine($"Department: {s.Department}");
            Console.WriteLine($"Semester: {s.Semester}");
            Console.WriteLine("-------------------");
        }
    }

    static void SearchStudent() {
        Console.Write("Enter ID to search: ");
        string searchId = Console.ReadLine();

        if (searchId == "") {
            Console.WriteLine("search id is empty");
            return;
        }

        bool found = false;

        foreach(Student s in students) {
            if (s.ID == searchId) {
                Console.WriteLine("\nStudent Found:");
                Console.WriteLine($"ID: {s.ID}");
                Console.WriteLine($"Name: {s.Name}");
                Console.WriteLine($"Department: {s.Department}");
                Console.WriteLine($"Semester: {s.Semester}");
                found = true;
                break;
            }
        }

        if (!found) {
            Console.WriteLine("couldn't find student");
        }
    }

    static void DeleteStudent() {
        Console.Write("Enter ID to delete: ");
        string deleteId = Console.ReadLine();

        if (deleteId == "") {
            Console.WriteLine("delete id is empty");
            return;
        }

        Student studentToDelete = null;

        foreach(Student s in students) {
            if (s.ID == deleteId) {
                studentToDelete = s;
                break;
            }
        }

        if (studentToDelete != null) {
            students.Remove(studentToDelete);
            Console.WriteLine("Student deleted successfully!");
            SaveData();
        } else {
            Console.WriteLine("student to delete not found");
        }
    }

    static void SaveData() {
        if (students != null) {
            using(StreamWriter sw = new StreamWriter(filePath)) {
                foreach(Student s in students) {
                    sw.WriteLine($"{s.ID},{s.Name},{s.Department},{s.Semester}");
                }
            }
            Console.WriteLine("(Data saved to file)");
        } else {
            Console.WriteLine("list is null");
        }
    }

    static void LoadData() {
        if (File.Exists(filePath)) {
            string[] lines = File.ReadAllLines(filePath);

            foreach(string line in lines) {
                string[] parts = line.Split(',');

                if (parts.Length == 4) {
                    Student loadedStudent = new Student {
                        ID = parts[0],
                        Name = parts[1],
                        Department = parts[2],
                        Semester = parts[3]
                    };
                    students.Add(loadedStudent);
                } else {
                    Console.WriteLine("check csv");
                }
            }
        } else {
            Console.WriteLine("file doesn't exist yet");
        }
    }
}
