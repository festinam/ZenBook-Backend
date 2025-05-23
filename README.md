# ZenBook – Online Booking Platform for Yoga & Pilates

ZenBook is a full-stack web application designed to streamline the booking and management of Yoga and Pilates classes. It offers a seamless experience for administrators, instructors, and clients, encompassing features like session scheduling, instructor assignments, client management.

---

## Live Demo

> **Frontend Repository:** [https://github.com/RinaHalili/ZenBook-Frontend](https://github.com/RinaHalili/ZenBook-Frontend)  
> **Backend Repository:** [https://github.com/festinam/ZenBook-Backend](https://github.com/festinam/ZenBook-Backend)

---

## Features

- **User Roles & Authentication:** Secure login with role-based access for Admins, Instructors, and Clients.
- **Instructor Management:** Add, edit, and remove instructors; assign them to specific courses.
- **Course Management:** Add, edit, and remove courses.
- **Client Management:** Add, edit, and remove clients.
- **Session Scheduling:** Add, edit, and remove sessions.
- **Payments Management** Add, edit, and remove payments.
- **Multi-Tenant Architecture:** Isolated data management for different organizations or branches.
- **API Documentation:** Comprehensive API docs available via Swagger UI.

---

## Tech Stack

### Frontend

- **React** – Building UI with components
- **Vite** – Development server and build tool
- **JavaScript (ES6+)** – Application code
- **CSS** – Styling
- **HTML** – Static markup

### Backend

- **ASP.NET Core** (.NET 7) – RESTful API development
- **Entity Framework Core** – ORM for database access
- **Microsoft SQL Server** – Relational database
- **ASP.NET Identity** – Authentication and user management
- **JWT Bearer Authentication** – Securing endpoints
- **Swashbuckle (Swagger)** – API documentation
- **Dependency Injection** – Application-wide DI

---

## Getting Started

### Prerequisites

- **Node.js & npm:** For running the frontend application.
- **.NET 7 SDK:** For building and running the backend API.
- **Microsoft SQL Server:** For database management.

### Installation

#### Backend

1. **Clone the repository:**

   ```bash
   git clone https://github.com/festinam/ZenBook-Backend.git
   cd ZenBook-Backend
   ```

2. **Configure the database connection:**

   - Update the `appsettings.json` file with your SQL Server connection string.

3. **Apply migrations and update the database:**

   ```bash
   dotnet ef database update
   ```

4. **Run the backend application:**

   ```bash
   dotnet run
   ```

5. **Access Swagger UI:**
   - Navigate to `https://localhost:<port>/swagger` in your browser.

#### Frontend

1. **Clone the repository:**

   ```bash
   git clone https://github.com/RinaHalili/ZenBook-Frontend.git
   cd ZenBook-Frontend
   ```

2. **Install dependencies:**

   ```bash
   npm install
   ```

3. **Configure environment variables:**

   - Create a `.env` file and set:
     ```
     VITE_API_URL=http://localhost:<your-backend-port>/api
     ```

4. **Run the frontend application:**

   ```bash
   npm run dev
   ```

5. **Access the application:**
   - Navigate to `http://localhost:3000` in your browser.

---

## Academic Context

This project was developed as part of the **Distributed Systems** course at the **University of Prishtina “Hasan Prishtina”**, Faculty of Electrical and Computer Engineering.

### Supervisors

- **Prof. Dr. Dhuratë Hyseni**
- **Assistant: Dalinë Vranovci**
- **Assistant: Blend Arifaj**

### Contributors

- [Festina Mjeku](https://github.com/festinam)
- [Rina Halili](https://github.com/RinaHalili)

---

## License

This project is intended for educational purposes and is not licensed for commercial use without prior permission.
