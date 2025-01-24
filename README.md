# Business Copilot

**Business Copilot** is a management system designed for freelancers and small business owners, enabling efficient management of contracts, payments, cash flow, stakeholders, and subaccounts. Built with a .NET C# backend and a Next.js frontend, the system focuses on providing flexibility, scalability, and ease of use.

Our primary goal is to create a reliable, intuitive platform that requires minimal user effort to get started. By reducing the time needed for data entry, we aim to help users focus on their core activities while maintaining a comprehensive and effective management tool.

---

## Next Steps

- Enhance the database structure.
- [ x ] Create database migrations.
- Strengthen cryptography with salt.
- Implement JWT for authentication (access and refresh tokens).
- Add caching to authentication middleware to reduce database access for every request.
- Apply CI/CD using Jenkins to autodeploy in the VPS
- Integrate the existing WhatsApp chatbot (developed in Node.js) with the API for streamlined CRUD operations.

---

## Features (Version 1)

- **Multi-User System**: Manage clients, contracts, and cash flows independently.
- **User Roles**: Includes roles such as admin, user, and client.
- **Financial Tracking**: Monitor payments, invoices, cash flow, income, and expenses.
- **Stakeholder Management**: Easily access critical information about clients, partners, and providers.
- **Subaccounts**: Allow clients to view progress, invoices, and other related data.
- **Custom Progress Tracking**: Manage project progress, stakeholders, and document uploads.
- **File Uploads**: Upload files such as PDFs and images for contracts and profiles.

---

## Tech Stack

- **Backend**: ASP.NET Core Web API (MVC architecture).
- **Frontend**: Next.js (available in a separate repository).
- **Database**: SQL with Entity Framework Core.
- **Authentication**: JWT with role-based authorization.
- **File Storage**: Local file system, with plans for cloud storage support in future versions.

---

### API Structure

- **Controllers**: Define REST API endpoints for managing users, contracts, payments, and other entities.
- **DTOs (Data Transfer Objects)**: Standardize data flow between the API and the client.
- **Filters & Middleware**: Manage tasks such as error logging, authorization, and custom filtering.

### App Structure

- **Entities**: Core business models (e.g., `User`, `Contract`, `CashFlow`).
- **Services**: Contain business logic, such as user registration and cash flow tracking.
- **Use Cases**: Represent user interactions and task workflows.

### Repository Structure

- **Repositories**: Handle data persistence, including database queries and CRUD operations.
- **Data**: Contains the database context and migration scripts.

---

## Getting Started

### Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0).
- SQLite (for local development) or PostgreSQL (for production).

### Setup

1. Clone the repository:

   ```bash
   git clone <repository link>
   cd Business Copilot
   ```

2. Restore NuGet packages and build the project:

   ```bash
   dotnet build
   ```

3. Run the API:

   ```bash
   dotnet run --project API
   ```

4. (Optional) Apply database migrations:
   ```bash
   dotnet ef database update --project REPO
   ```

### Testing

Unit tests are located in the `Tests` directory. To run the tests:

```bash
dotnet test
```

---

## Roadmap

- **Version 1**: Default entities and basic functionalities.
- **Version 2**: Support for custom entities and detailed subaccount management.
- **Version 3**: Real-time analytics, notifications, and AI-driven insights.

---

## Contributing

Contributions are welcome! Please fork the repository and create a pull request for review.

---

## License

This project is licensed under the MIT License.

---

## Contact

For inquiries or support, reach out to:  
**Ricardo Sipione** - [ricardo@sipionetech.com](mailto:ricardo@sipionetech.com)

---

This version improves readability, corrects grammar, and presents a more professional tone. Let me know if you'd like further refinements!
