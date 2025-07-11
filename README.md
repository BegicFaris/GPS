# GPS (Gradski Prevozni Sistem)

A full-stack web application for managing and using a city bus system, developed with .NET Core Web API (backend) and Angular (frontend). Users can view bus lines, stations, rides, and purchase tickets.

---

## 📑 Table of Contents
- [Project Overview](#project-overview)
- [Technologies](#technologies)
- [Prerequisites](#prerequisites)
- [Backend – Running the API Server](#backend--running-the-api-server)
- [Frontend – Running the Angular Client](#frontend--running-the-angular-client)
- [How to Set Up Secret Keys](#-how-to-set-up-secret-keys)
- [Test Login Credentials](#-test-login-credentials)
- [Notes](#notes)
- [Project Background](#project-background)
- [Authors](#authors)

---

## Project Overview
This application was developed as part of the mandatory curriculum for the course Software Development I (Razvoj Softvera I).

---

## ⚙️ Technologies
- .NET 8+ Web API
- Angular 15+
- SQL Server
- Entity Framework Core

---

## 📦 Prerequisites
Make sure the following tools are installed:
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [Angular CLI 15+](https://angular.io/cli)
- SQL Server

---

## 🔧 Backend – Running the API Server
1. **Restore dependencies**
   ```bash
   cd Backend
   cd API
   dotnet restore
   ```
2. **Set up the database**
   ```bash
   dotnet ef database update --context ApplicationDbContext
   ```
3. **Seed data**
   - Automatically generated in `ApplicationDbContext.OnModelCreating()`.
4. **Start the server**
   - Run via Visual Studio or:
   ```bash
   dotnet run
   ```

---

## 💻 Frontend – Running the Angular Client
1. **Install dependencies**
   ```bash
   cd Frontend
   cd Client
   npm install
   ```
2. **Start the app**
   ```bash
   npm start
   ```

---

## 🔑 How to Set Up Secret Keys
Before running the app, you need to create your own configuration files with real secret keys. Here’s how to get each required key:

### 1. Backend (`appsettings.json`)
Copy `Backend/API/appsettings.example.json` to `Backend/API/appsettings.json` and fill in the following:
- **TokenKey**: Generate a long, random string (at least 32 characters). Use an online password generator or run `openssl rand -base64 32` in a terminal.
- **ReCaptcha SecretKey**: Go to [Google reCAPTCHA Admin Console](https://www.google.com/recaptcha/admin), register your site, and get the secret key. Replace `"YOUR_RECAPTCHA_SECRET_KEY"`.
- **Email Configuration**: Use SMTP credentials from your email provider (e.g., Gmail, Outlook, or a transactional email service like SendGrid). Fill in `SmtpServer`, `SmtpPort`, `SmtpUsername`, and `SmtpPassword`.
- **Stripe SecretKey**: Go to [Stripe Dashboard](https://dashboard.stripe.com/apikeys), register an account if needed, and get your secret key. Replace `"your_stripe_secret_key"`.

### 2. Frontend (`environment.ts`)
Copy `Frontend/Client/src/environments/environment.example.ts` to `Frontend/Client/src/environments/environment.ts` and fill in:
- **recaptchaSiteKey**: From the [Google reCAPTCHA Admin Console](https://www.google.com/recaptcha/admin) (use the “site key”).
- **stripePublishableKey**: From your [Stripe Dashboard](https://dashboard.stripe.com/apikeys) (look for the “publishable key”).

> **Never commit your real secret keys to version control!**  
> Use the example files as templates and keep your real keys private.

---

## 🔐 Test Login Credentials
| Role     | Email        | Password |
|----------|-------------|----------|
| Driver   | 2@gmail.com | 123      |
| Manager  | 8@gmail.com | 123      |
| Passenger|14@gmail.com | 123      |

---

## Notes
- In Visual Studio, set the Backend project as the Startup Project.
- In VS Code, run the backend and frontend in two separate terminals.

---

## 📚 Project Background
This project was created as a required seminar assignment for the course Software Development I (Razvoj Softvera I), and it has been officially accepted.

---

## 📄 Authors
- Faris Begić
- Adi Gosto
- Nedim Jugo

3rd Year Students  
Faculty of Information Technologies  
"Džemal Bijedić" University of Mostar  
Course: Software Development I