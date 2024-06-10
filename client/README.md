# CP&You
## Problem Solved
Choosing the parts for a new computer can be a stressful experience. You might be worried that your part selection doesn’t make sense, or that some parts might even be incompatible with each other.

This website aims to solve these issues with a dual-pronged approach:
- Built-in compatibility checking
- A community that can suggest helpful changes

## Technologies Used
- ReactJS
- MUI v5
- EF Core 8.0
- PostgreSQL 16
- JavaScript
- HTML5
- CSS3
- Emotion
- Vite

## Installation and Setup Instructions
Clone this repository, and ensure you have the following installed on your machine:
- [node](https://github.com/nodejs/node)
- [npm](https://github.com/npm/cli)
- [.Net 8.0.101 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL 16](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)

Then run the following command
```
dotnet tool install --global dotnet-ef --framework net8.0
```
#### Installation
Navigate to the cloned directory and run the following
```
dotnet user-secrets init
```
```
dotnet user-secrets set 'HouseRulesDbConnectionString' 'Host=localhost;Port=5432;Username=postgres;Password=<your_postgresql_password>;Database=HouseRules'
```
```
dotnet user-secrets set AdminPassword password
```
```
dotnet restore
```
```
dotnet ef migrations add InitialCreate
```
```
dotnet ef database update
```
Then navigate to the client directory and run the following
```
npm install
```
#### Run Database
Run the following command in the project root directory
```
dotnet watch run --launch-profile https
```
#### Run Website
Navigate to the client directory and run the following
```
npm run dev
```