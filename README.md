# Project Soccer Table

## Introduction

This repository contains the code for the Soccer Table application, a **.NET Core-based** project.

## Installation

To get started with the Soccer Table application, follow these steps:

### 1. Clone the Repository

Open your terminal and run the following commands:

```bash
git clone https://github.com/agilemongoaps/Project-Soccer-Table-APP.git
cd Project-Soccer-Table-APP
```

### 2. Install Dependencies

Ensure you have .NET Core installed on your machine. Once confirmed, run the following  
command in the root directory of the project (where the **.csproj** file is located):

```bash
dotnet restore
```

## Running the Application

To run the Soccer Table application, navigate to the root directory of the project (where the **.csproj** file is located)
and execute the following command:

```bash
dotnet run
```

## Manage Leagues

Within the project, there is a src folder where you can add your league folders. This league folder is
used by the application to display the league related data.

The directory structure should look like this:

```bash
Project-Soccer-Table/
│
├── src/
│   ├── your-league-folder/
│   │   └── (your league files here)
│
├── Project-Soccer-Table.csproj
└── ...
```

## Running Tests

To run the tests for the Soccer Table application, navigate to the root directory of the project
(where the **.csproj** file is located) and use the following command:

This command will run all unit tests in the solution and output the test results to the console.

```bash
dotnet test
```
