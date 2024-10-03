insert into Users
  (Email, PasswordHash, Role, FullName, DateJoined)
values
  ('instructor1@example.com', 'hashedPassword1', 1, 'Instructor One', GETDATE()),
  ('instructor2@example.com', 'hashedPassword2', 1, 'Instructor Two', GETDATE()),
  ('student1@example.com', 'hashedPassword3', 0, 'Student One', GETDATE()),
  ('student2@example.com', 'hashedPassword4', 0, 'Student Two', GETDATE()),
  ('student3@example.com', 'hashedPassword5', 0, 'Student Three', GETDATE()),
  ('student4@example.com', 'hashedPassword6', 0, 'Student Four', GETDATE()),
  ('student5@example.com', 'hashedPassword7', 0, 'Student Five', GETDATE()),
  ('student6@example.com', 'hashedPassword8', 0, 'Student Six', GETDATE()),
  ('student7@example.com', 'hashedPassword9', 0, 'Student Seven', GETDATE()),
  ('student8@example.com', 'hashedPassword10', 0, 'Student Eight', GETDATE());

insert into Courses
  (InstructorId, Title, Description, Price, CreationDate)
values
  (1, 'C# Basics', 'Introduction to C# programming.', 49.99, GETDATE()),
  (1, 'Entity Framework Core', 'Learn Entity Framework Core from scratch.', 59.99, GETDATE()),
  (2, 'ASP.NET MVC', 'Learn ASP.NET MVC.', 69.99, GETDATE()),
  (2, 'JavaScript for Beginners', 'A beginner’s guide to JavaScript.', 39.99, GETDATE()),
  (1, 'Advanced C#', 'Deep dive into C# features.', 79.99, GETDATE()),
  (2, 'SQL Server Essentials', 'Learn SQL Server from basics to advanced.', 89.99, GETDATE()),
  (1, 'WPF Development', 'Create rich Windows desktop applications.', 59.99, GETDATE()),
  (2, 'React Basics', 'A beginner’s guide to React.', 49.99, GETDATE()),
  (1, 'HTML and CSS', 'Learn the fundamentals of web design.', 29.99, GETDATE()),
  (2, 'Python Programming', 'Master the basics of Python.', 69.99, GETDATE());

insert into Enrollments
  (StudentId, CourseId, EnrollmentDate, Progress)
values
  (3, 1, GETDATE(), 0.1),
  (3, 2, GETDATE(), 0.2),
  (4, 3, GETDATE(), 0.0),
  (4, 4, GETDATE(), 0.5),
  (5, 5, GETDATE(), 0.7),
  (5, 6, GETDATE(), 0.9),
  (6, 7, GETDATE(), 0.0),
  (6, 8, GETDATE(), 0.1),
  (7, 9, GETDATE(), 0.3),
  (8, 10, GETDATE(), 0.0);

insert into Lectures
  (CourseId, Title, Content, [Order])
values
  (1, 'Intro to C#', 'Introduction to the C# language.', 1),
  (1, 'Data Types', 'Understanding data types in C#.', 2),
  (1, 'Control Flow', 'Control flow statements in C#.', 3),
  (2, 'Intro to EF Core', 'Getting started with Entity Framework Core.', 1),
  (2, 'DbContext', 'Understanding DbContext.', 2),
  (3, 'Intro to ASP.NET MVC', 'Overview of ASP.NET MVC.', 1),
  (4, 'Intro to JavaScript', 'Getting started with JavaScript.', 1),
  (5, 'Advanced C# Features', 'Deep dive into C# advanced features.', 1),
  (6, 'SQL Basics', 'Understanding SQL basics.', 1),
  (7, 'WPF Controls', 'Learning WPF controls.', 1);


insert into Payments
  (StudentId, CourseId, Amount, PaymentDate)
values
  (3, 1, 49.99, GETDATE()),
  (3, 2, 59.99, GETDATE()),
  (4, 3, 69.99, GETDATE()),
  (4, 4, 39.99, GETDATE()),
  (5, 5, 79.99, GETDATE()),
  (5, 6, 89.99, GETDATE()),
  (6, 7, 59.99, GETDATE()),
  (6, 8, 49.99, GETDATE()),
  (7, 9, 29.99, GETDATE()),
  (8, 10, 69.99, GETDATE());

insert into Quizs
  (CourseId, Title, MaxScore)
values
  (1, 'C# Basics Quiz', 100),
  (2, 'Entity Framework Core Quiz', 100),
  (3, 'ASP.NET MVC Quiz', 100),
  (4, 'JavaScript Quiz', 100),
  (5, 'Advanced C# Quiz', 100),
  (6, 'SQL Server Quiz', 100),
  (7, 'WPF Quiz', 100),
  (8, 'React Quiz', 100),
  (9, 'HTML and CSS Quiz', 100),
  (10, 'Python Quiz', 100);

insert into Reviews
  (CourseId, StudentId, Rating, ReviewText)
values
  (1, 3, 5, 'Great introduction to C#!'),
  (2, 3, 4, 'Very helpful EF Core course.'),
  (3, 4, 4, 'ASP.NET MVC explained well.'),
  (4, 4, 5, 'Perfect for beginners.'),
  (5, 5, 4, 'Learned a lot from this course.'),
  (6, 5, 5, 'In-depth SQL knowledge.'),
  (7, 6, 3, 'A bit basic for my needs.'),
  (8, 6, 4, 'Good course on React basics.'),
  (9, 7, 5, 'Clear and concise web design tutorial.'),
  (10, 8, 5, 'Python is easy to learn after this.');

insert into Questions
  (QuizId, QuestionText)
values
  (1, 'What is C#?'),
  (1, 'What is a data type?'),
  (2, 'What is Entity Framework?'),
  (2, 'How do you create a DbContext?'),
  (3, 'What is ASP.NET MVC?'),
  (4, 'What is JavaScript?'),
  (5, 'Explain C# advanced features.'),
  (6, 'What is SQL Server?'),
  (7, 'What is a WPF Control?'),
  (8, 'What is React?');

insert into AnswerOptions
  (OptionText, IsCorrect, QuestionId)
values
  ('A programming language', 1, 1),
  ('A markup language', 0, 1),
  ('An IDE', 0, 1),
  ('A datatype is a variable type', 1, 2),
  ('A datatype is an IDE', 0, 2),
  ('ORM framework for .NET', 1, 3),
  ('A database tool', 0, 3),
  ('Creating a DbContext is essential', 1, 4),
  ('DbContext is not required', 0, 4),
  ('A web development framework', 1, 5);

insert into QuizResults
  (StudentId, QuizId, Score, CompletionDate)
values
  (3, 1, 90, GETDATE()),
  (3, 2, 85, GETDATE()),
  (4, 3, 95, GETDATE()),
  (4, 4, 80, GETDATE()),
  (5, 5, 88, GETDATE()),
  (5, 6, 92, GETDATE()),
  (6, 7, 70, GETDATE()),
  (6, 8, 75, GETDATE()),
  (7, 9, 95, GETDATE()),
  (8, 10, 90, GETDATE());
