# Quill
**Admin Access**: Zain Farhan

This Repo is owned and maintained by some CS (mostly LUMS) students for the purposes of using it for their Summer Project. This is a Word processor build from scratch using the Avalonia UI with the .NET framework.

## Table of Contends

1. [Introduction](#Introduction)
2. [Contributors](#Contributors)
3. [Timeline](#Timeline)
4. [Guidelines](#Guidelines)
5. [Naming Schemes](#Naming-Scheme)
6. [Resources](#Resources)

## Introduction

The aim of Quill is to be a lightweight and simple word processor so people like students, teacher, and office workers can use it to make simple documents. Below are the features that we have potentially planned for the project and those that are currently avalible.

### Objectives

- [ ] Text Engine
  - [ ] Basic Text
  - [ ] Rich Text
  - [ ] Alignment
  - [ ] Text Styles
- [ ] Export to PDF file format
- [ ] Export to DOCX file format

### Details 

Please make sure you have all the required tools installed and ready:
- Framework: .NET 10.0+ sdk
- Library: Avalonia UI 12.0+
- Operating System: Windows 10 or newer (Recommended but not required)
- Integrated Development Environment: JetBrains Rider (Recommended but not required)

### MVVM Paradigm

MVVM stands for Model–View–ViewModel. It’s a design pattern used to organize code, especially in apps with a user interface (like mobile or desktop apps). It helps separate the logic of your app (how it works) from the way it looks. For example, let's say we are building a weather app.

MVVM breaks this into 3 parts:

1. **Model:** 
This is your data layer. It includes the weather data and logic to fetch it from the internet or a database. No UI code here.

2. **View:**
This is the UI — what the user sees and interacts with. Buttons, labels, graphs, etc. It doesn’t know where the data comes from or how it’s processed. It just shows it. So in our case it would just be the weather information.

3. **View-Model:**
This is the middle layer between the View and the Model. It takes raw data from the Model and prepares it for the View. For example, it might convert "23.4°C" into "Warm day" for display. The View-Model also handles UI events, like when the user taps a button to refresh the weather.

## Contributors
- Zain Farhan: Project Lead

## Timeline
The Development Cycles have been divided into various Stages.

## Guidelines

Below are the guidelines for the code of conduct we recommend that you follow, these may be updated or changed throughout development. Though these will not be strictly enforced, we **highly suggest** following them:
- Contributors must make all of their changes on branches.
- We suggest you push your changes to the repository frequently.
- We suggest you only work on **one** feature per branch.
- We suggest informing a Head Developer (or sending a message in the Group Chat if applicable) before starting work on a new branch.
- Direct changes to the "main" branches are not permitted.
- Forceful changes to the "main" branch are **only** to be made the Admin.
- Forceful changes to the "main" branch are **only** to be made as a Drastic measure and a last resort.
- Any merge request must be approved by their designated code reviewer or the Admin.
- Any pull request must be approved by a code reviewer and one other contributor.
- All code reviewers must provide a Valid Description for any rejection.
- Avoid making self-explanatory comments:
```
  // Counter for Moves
  int moveCount;                                 X

  // Uses Matrix-Exponentiation to find largest
  // Fibonnaci Number
  long LargestFibNum(double time) {...}          O
```
- Avoid leaving large chunks of code commented out, if you wish to leave a backup of your previous code consider creating a separate file locally.
- Avoid ambiguous names (e.g. `Backup2.txt`).
- Unreasonable requests (e.g. a merge request consisting of more than a 1,000 lines) will not be humoured.

A failure to comply may result in your code be rejected or even being taken out of the development team based on how egregious the non-compliance is.

## Naming Scheme

Please use the following naming conventions when writing and submitting code, changes which do not follow be **may not be accepted**:
- Variable Name: *camelCase*
- Private Fields: *\_camelCase* with the prefix \_
- Methods/Properties: *PascalCase*
- Classes/Interfaces: *PascalCase*
- Files: *PascalCase*

Though these will not be strictly enforced, the naming convention used must be sensible and integrate well with the existing code base, or it may be rejected.

Additionally, following the official naming scheme would be appreciated but is not required. For additional info, [see here](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names).

## Resources

Official Documentation:
- [Microsoft's C#](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/)
- [Avalonia UI](https://docs.avaloniaui.net/docs/overview/what-is-avalonia)

Video Tutorials:
- [C# for Absolute Beginners](https://youtube.com/playlist?list=PLPV2KyIb3jR4CtEelGPsmPzlvP7ISPYzR&si=kc9i4wZJKezL6RRK)
- [C# Basics](https://youtu.be/GhQdlIFylQ8?si=kuTilL_nES_pctFF)
- [Avalonia UI](https://youtube.com/playlist?list=PLJYo8bcmfTDF6ROxC8QMVw9Zr_3Lx4Lgd&si=lsUY6YDOKVZBhFwv)
- [Avalonia UI Comprehensive](https://youtube.com/playlist?list=PLrW43fNmjaQWwIdZxjZrx5FSXcNzaucOO&si=f2XPAjseWJGIPJyP)