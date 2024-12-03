# LearnFlow - E-Learning Platform

LearnFlow is a modern e-learning platform built with ASP.NET Core MVC that facilitates seamless interaction between instructors and students. The platform provides comprehensive course management, student progress tracking, and assessment capabilities.

## 🚀 Features

- **Course Management**
  - Create and manage courses with multimedia content
  - Upload video lectures and supplementary materials
  - Set course pricing and descriptions
  - Manage course enrollments

- **Student Features**
  - Personal dashboard with enrolled courses
  - Real-time progress tracking
  - Interactive course content access
  - Quiz participation and results tracking

- **Assessment System**
  - Built-in quiz creation and management
  - Automatic grading system
  - Visual progress tracking
  - Detailed result analytics

- **Search & Discovery**
  - Dynamic course search functionality
  - Real-time search results
  - Course filtering and sorting options

## 🛠️ Tech Stack

- **Backend**
  - ASP.NET Core MVC
  - Entity Framework Core
  - SQL Server

- **Frontend**
  - JavaScript
  - jQuery
  - Bootstrap
  - Chart.js

## ⚙️ Prerequisites

- .NET 6.0 SDK or later
- SQL Server
- Visual Studio 2022 (recommended) or VS Code
- Node.js (for npm packages)

## 🚦 Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/LearnFlow.git
   cd LearnFlow
   ```

2. **Update Database Connection**
   - Open `appsettings.json`
   - Update the connection string to match your SQL Server instance

3. **Apply Database Migrations**
   ```bash
   dotnet ef database update
   ```

4. **Install Dependencies**
   ```bash
   dotnet restore
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

   The application will be available at `https://localhost:5001` or `http://localhost:5000`

## 🏗️ Project Structure

```
LearnFlow/
├── Controllers/         # MVC Controllers
├── Models/             # Domain Models
├── Views/              # Razor Views
├── ViewModels/         # View Models
├── Repository/         # Data Access Layer
├── Data/              # Database Context
├── wwwroot/           # Static Files
│   ├── css/
│   ├── js/
│   └── images/
└── Helpers/           # Utility Classes
```

## 🔐 Authentication

The platform uses ASP.NET Core Identity for authentication and authorization with the following roles:
- Administrator
- Instructor
- Student

## 🎯 Key Features Implementation

### Course Management
- Instructors can create courses with detailed information
- Upload and manage course content
- Track student enrollment and progress

### Student Dashboard
- Personal profile management
- Course enrollment tracking
- Progress visualization with charts
- Access to course materials and quizzes

### Assessment System
- Quiz creation and management
- Automatic grading
- Progress tracking and visualization
- Detailed result analytics

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📧 Contact

Your Name - eng.yousef.elbilkasy@gmail.com

Project Link: https://github.com/YousefElbilkasy/LearnFlow
