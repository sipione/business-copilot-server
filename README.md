Here's an improved version of the README file based on your current folder structure:

# Business Copilot
Business Copilot is a management system tailored to freelancers and small business owners to efficiently manage contracts, payments, cash flow, stakeholders, and subaccounts. Built with a .NET C# backend, the system is designed to offer flexibility, scalability, and extensibility, with a frontend implementation in Next js. The idea is create a trustful system, simple to use and understand the tools to people who don't have too much time to insert data, so the first goal is get a structure complete with less effort as possible and the minimum of requirement as possible to work.

## Next Steps
- Improve the database
- Create the migrations
- Improve the criptography with salt
- Use jwt to authentication access and refresh tokens
- use cache to the authentication middleware and avoid database access on every requisition
- integrate the whatsApp chatbot (already doine in Nodejs) with the api to be able to CRUD easily the data

## Features (present and future in version 1)
- **Multi-User System**: Users can manage clients, contracts, and cash flows independently.
- **User Roles**: Includes various roles such as admin, user, and client.
- **Financial Tracking**: Easily track payments, invoices, cash flow, income, and expenses.
- **Stakeholders Pooll**: Be able to reach easily all the important information about the clients, partners, providers, etc. 
- **Subaccounts**: Allow clients to view progress, invoices, and other related data.
- **Custom Progress Tracking**: Track project progress, manage stakeholders, and upload documents.
- **File Uploads**: Upload files such as PDFs and images for contracts or profiles.
  
## Tech Stack
- **Backend**: ASP.NET Core Web API (MVC architecture)
- **Frontend**: Next js (available in other repository)
- **Database**: SQL with Entity Framework Core
- **Authentication**: JWT with role-based authorization
- **File Storage**: Local filesystem (expandable to cloud storage in future versions)

### API Structure
- **Controllers**: Define REST API endpoints for managing users, contracts, payments, and other entities.
- **DTOs**: Objects that shape the data between the API and the client.
- **Filters & Middlewares**: Handle common tasks like error logging, authorization, and custom filtering.

### APP Structure
- **Entities**: Core business models (e.g., User, Contract, CashFlow).
- **Services**: Contain business logic such as user registration, cash flow tracking.
- **Use Cases**: Describe scenarios of how users interact with the system to accomplish tasks.

### REPO Structure
- **Repositories**: Handle data persistence (e.g., database queries, CRUD operations).
- **Data**: Contains database context and migration scripts.

## Getting Started

### Prerequisites
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQLite (for local development) or PostgreSQL (for production)

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
Unit tests are located in the `Tests` directory. Run them using:

```bash
dotnet test
```

## Roadmap
- [ ] Version 1: Default entities and basic functionalities.
- [ ] Version 2: Custom entities, detailed subaccount management.
- [ ] Version 3: Real-time analytics, notifications and insights with AI.

## Contributing
Contributions are welcome! Please fork the repository and create a pull request.

## License
This project is licensed under the MIT License.

## Contact
For any inquiries, reach out to:

- Ricardo Sipione - ricardo@sipionetech.com