# Budget App

## Introduction

This budget app is designed to help you record and manage personal finance transactions. It utilizes web development technologies, including Entity Framework with SQL Server for database management and MVC for the overall structure. It also has a basic implementation of registration and login for user access.

## Requirements

- **Database Structure:** The app uses two linked tables: Transaction and Category, establishing one-to-many relationships.
- **Entity Framework:** All database operations are done through Entity Framework.
- **Search Functionality:** Users can search transactions by name.
- **Filter Functionality:** Transactions can be filtered by category and date.
- **Modals for CRUD Operations:** Insert, delete, and update operations for transactions and categories are performed within modals, without redirecting to separate pages.
- **Data Validation:** The form prevents negative values and non-numeric inputs for transaction amounts using data annotations.

## Usage

1. **Installation:** Clone the repository to your local machine.
2. **Database Setup:** Configure SQL Server connection string in `appsettings.json`.
3. **Run the Application:** Use Visual Studio or your preferred development environment to run the app.
4. **Explore Functionality:** Create, view, update, and delete transactions and categories using the provided UI elements.
5. **Customization:** Modify the code to add new features or customize existing ones according to your requirements.
