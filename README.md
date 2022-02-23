# Chinook_Assignment

## Installation
SQL Server Express <br>
SQL Server Management Studio- SSMS

## Collaborator
Gabriel Andersson <br>
Kitt Cheung

## Usage
In order to start the full application, a database has to be created and connected.
In this case, for this application, a Chinook SQL script is provided with the setup for the database and SSMS is the program used to run this script. 

1. Start up SSMS and run the Chinook.sql script. 
2. Set up the database by clicking on "Execute".
3. Check in Object Explorer that the database called Chinook exists with it's tables and it's contents.

With this the database is set up.
Next step is to connect the application with the database.
1. Start up the application in Visual Studio.
2. At the Solution Explorer locate the file called ChinookContext.cs
3. Inside the method called "OnConfiguring", change the string inside "optionsbuilder.UseSqlServer" to your computername. <br> From "Data Source=LAPTOP-AWZUM" into for example "DESKTOP-F6VPM3B".
4. Next is to locate the "Program.cs" file and follow the short description on how to run the methods created for the assignment.

## Description
This application follows the assignment requirements provided by Noroff. <br>
To find the exact requirements please look at the Assignment-pdf at Appendix B: 2) Customer Requirements.

## Project Status
This application is the final result for the assignment. Due to some trouble with the resharper or cachefiles on Visual Studio the original git-history is located at a different repository. 
