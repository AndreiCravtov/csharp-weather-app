
# C# weather app

## Learning summary

While learning the C# programming language, I have produced a simple web scraper, that scours Wikipedia web pages, as one of my projects. This project scrapes the links from each page so it can continue crawling, and the content of each page. The content is used as material to determine the letter prevalence of the letters of the English alphabet. These prevalence statistics are then displayed as a bar chart.

* C# programming language: I have continued to practice C# syntax as well as expanding my knowledge about Visual Studio and the .NET standard library. I have also learned to write extension methods for base types like `string` to expand the functionality of the language without recompiling it.

* Object-oriented programming: while working on thus project, I have utilised the rich OOP infrastructure of the C# language. For instance, to recreate the Java-style `Enum`, I used a `private` constructor, `static` instances (which are the `Enum` values themselves) and `public` properties of those instances. Furthermore, I used object-oriented patterns in this project, like the `KeyCapture` singleton.

* Multithreading: this app's UI is based on keyboard input. Waiting for user input is an execution blocking functionality. As a result, the part of the program which waits for user input utilises threading functionality to run in the background and not interrupt the main thread.

* API: the data for this app comes from a well known weather API. In this project, I learned how to fetch data from a HTPP API and interpret it within the application, before displaying it.

* Postman: as this project is deeply dependant on APIs, I found it to be the perfect opportunity to learn the baisics of how to use Postman for API testing. 

* Problem solving: in this project, I learned how to use the `$""` string syntax in order to create interactive and animated text in the console. Designing such a UI involved solving problems of UI navigation, how to animate certain features, and how to show users feedback to their inputs.

## How to operate this project

### How to run the project

1. Download the `.zip` file from [here](https://github.com/AndreiCravtov/csharp-weather-app/releases/tag/Windows).

2. Extract the `.zip` file, and run `WeatherApp.exe`.

### Application use

The application will run and display the UI in the console . Be sure to size the console window appropriately as to not cause any jitter. Follow on-screen navigation instructions to operate the application.

## Viewing and  modifying  the project

This repository is a Visual Studio solution. It can be opened and edited by cloning this repo and opening the appropriate `.sln` file in Visual Studio, like any other project. From there, the source code can be viewed and modified.
