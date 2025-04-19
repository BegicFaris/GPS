# Gradski Prevozni Sistem (GPS)

Ovo je web aplikacija razvijena pomoću **.NET Core Web API** za backend i **Angular** za frontend.  
Aplikacija služi za upravljanje gradskim bus sistemom, omogućavajući korisnicima pregled linija, stanica, vožnji i kupovinu karata.

## ⚙️ Tehnologije

- .NET 8+ Web API
- Angular 15+
- SQL Server (localdb)
- Entity Framework Core

## 📦 Preduvjeti

Prije pokretanja aplikacije potrebno je instalirati:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- SQL Server (lokalni ili mrežni)

## Promjena imena klonranog foldera

1. Potrebno je promijeniti ime kloniranog foldera u npr. GPS kako bi radilo (specijalni karakteri koje dodaje Azure mogu praviti problem pa da se ništa ne prikazuje na backendu)

## 🔧 Backend – Pokretanje API servera

1. Instalacija paketa
```bash
cd Backend
cd API
dotnet restore
```

2. Postavljanje baze podataka
```bash
dotnet ef database update --context ApplicationDbContext
```

3. Seeder podaci
Prilikom postavljanja baze podataka automatski se generišu unutar OnModelCreating() metode u ApplicationDbContextu. 

4. Pokretanje backend API-ja  
Pokrenite projekat pritiskom dugmeta u navigacijskom meniju 

## 💻 Frontend – Pokretanje Angular klijenta
Napomena:
Frontend predstavlja glavni projekat (primarna Angular aplikacija).

Frontend2 je testna instanca koja se koristi za testiranje multitenancy funkcionalnosti.
Frontend2 nije neophodan za rad same aplikacije.  

1. Instalacija zavisnosti
```bash
cd Frontend
cd Client
npm install
```

2. Pokretanje Angular aplikacije
```bash
npm start
```

3. Instalacija drugog frontenda
```bash
cd Frontend2
cd Client
npm install
```

## 🔐 Test login podaci

| Uloga | Email | Lozinka |
|-------|-------|---------|
| Vozač | 2@gmail.com | 123 |
| Menadžer |8@gmail.com  | 123 |
| Putnik | 14@gmail.com | 123 |

## 📎 Napomene

- Ako koristite Visual Studio, postavite Backend projekt kao Startup Project
- Ako koristite VS Code, pokrenite API i Angular zasebno iz terminala
- Verzije koje se koriste:
  - .NET SDK: 8.0
  - Node.js: 18.x
  - Angular CLI: 15.x

## 📄 Autori
Nedim Jugo, Faris Begić, Adi Gosto  
Godina studija: 3  
Predmet: Razvoj softvera 1