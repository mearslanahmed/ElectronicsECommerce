# ElectronicsEcommerce Project Documentation

## Overview
The **ElectronicsEcommerce** project is a fully functional e-commerce web application designed to sell electronic products. The platform supports user registration, browsing of products, adding items to a shopping cart, and processing orders. The project was built using **ASP.NET Web Forms** and integrates seamlessly with a built-in SQL Server database provided by Visual Studio.

This documentation provides detailed information on the system's features, implementation, and usage.

---

## Features
### Core Functionalities
1. **User Authentication**:
   - Secure user registration and login.
   - Passwords are hashed using SHA-256 for security.
2. **Product Browsing**:
   - View a catalog of electronics with details such as name, image, price, and description.
3. **Shopping Cart**:
   - Add products to the cart.
   - Update the quantity or remove items.
   - Automatically calculate the total price of items.
4. **Checkout Process**:
   - Redirect to a success or cancellation page upon confirming the purchase.
5. **Responsive Design**:
   - Fully responsive UI using Bootstrap, ensuring a seamless experience across devices.

### Additional Features
- **Session Management**:
  - Users must be logged in to access the shopping cart and checkout features.
  - Sessions maintain user-specific cart data.
- **Error Handling**:
  - User-friendly error messages for invalid actions (e.g., attempting to checkout without logging in).

---

## Architecture
The system follows a modular structure for scalability and maintenance. Below is the high-level architecture:

### **Frontend**
- **ASP.NET Web Forms**:
  - `.aspx` files for the user interface.
  - Bootstrap 4 for responsive design.
  - FontAwesome for icons.

### **Backend**
- **ASP.NET C#**:
  - Code-behind files (`.aspx.cs`) handle business logic.
  - `DBHelper` class manages database interactions using ADO.NET.
  - `SecurityHelper` class provides password hashing functionality.

### **Database**
- Built-in SQL Server LocalDB provided by Visual Studio:
  - Tables include `Users`, `Products`, and `ShopCart`.
  - Relationships between `Users` and `ShopCart` ensure user-specific cart data.
  - Basic SQL queries are used for CRUD operations.

---

## User Roles
### Guest
- Can browse the product catalog.
- Must register/login to add items to the cart or proceed to checkout.

### Registered User
- Can add products to the shopping cart.
- Can modify quantities or remove items.
- Can complete the checkout process.

---

## Key Classes and Methods
### `DBHelper.cs`
- **Purpose**: Handles all database-related operations.
- **Key Functions**:
  - `GetProductById(int productId)`: Fetches product details by ID.
  - `AddToCart(int userId, int productId)`: Adds a product to the user's shopping cart.

### `SecurityHelper.cs`
- **Purpose**: Provides security features such as password hashing.
- **Key Function**:
  - `HashPassword(string password)`: Hashes a password using SHA-256.

---

## Implementation Details
### Shopping Cart Workflow
1. **Add to Cart**:
   - Users can add products to their shopping cart from the product details page.
   - A session variable (`Session["UserID"]`) ensures the cart is user-specific.

2. **Update or Remove Items**:
   - Users can modify quantities or remove items directly from the shopping cart page.
   - The `UpdateCartItem` and `RemoveCartItem` methods handle these actions.

3. **Checkout Process**:
   - Users are redirected to a success or cancellation page after confirming their purchase.

---

## Security Measures
- **Password Hashing**:
  - Implemented using SHA-256 in the `SecurityHelper` class to ensure secure storage of user passwords.
- **SQL Injection Prevention**:
  - All database queries use parameterized queries to safeguard against injection attacks.
- **Session Management**:
  - Ensures that only authenticated users can access sensitive features like the shopping cart and checkout.

---

## Deployment
### Prerequisites
- Visual Studio 2019 or later.
- SQL Server LocalDB (pre-installed within Visual Studio).

### Steps to Run the Application
1. Open the solution file (`.sln`) in Visual Studio.
2. Press `Ctrl+F5` to build and run the application.
3. The application will launch in a browser, and you can begin testing.

---

## Known Limitations
1. **Database Dependency**:
   - The application relies on the built-in SQL Server LocalDB. Deployment to a production environment might require migration to a standalone SQL Server instance.
2. **No Payment Integration**:
   - The checkout process does not include payment gateway integration.

---

## Contact
For any questions or support, please contact:
- **Developer**: Arslan Ahmed
- **Email**: arslanahmednaseem@gmail.com