# EveEntrepreneur - Common
## Introduction
EveEntrepreneur is a larger project of mine, a third party software suite for the Massively Multiplayer Online game EVE Online. The software suite is meant as a tool to help me, and in future others as well, track various character data provided by the EVE's ESI API. The suite consists of various components, each revolving around different functionality.

EveEntrepreneur Common is a shared library containing code common to all the modules. Main elements of the Common library include:
- **Esi Auth Client** - module responsible for handling OAuth authorization tokens.
- **Esi Rest Client** - module responsible for interacting with various ESI API endpoints and returning obtained data. While only a small subset of endpoints is supported with proper data objects, lower-level methods allow calling the API directly through RestSharp library.
- **EntityFramework 6** - a module utilising EF6 to interact with database, where data is persistently stored. This is the main reason behind the whole package, to capture volatile data and store it in persistent manner (as some data from API can only reach back up to three months time).
- **Data Models** - library contains data transfer objects for data returned from the ESI API.
- **Other** - some other pieces of code, such as extension methods or helper classes that assist with writing business logic.
## Status of the project
As I am not actively playing EVE Online for over a year now, the project's development is pretty much halted, with occasional exception of improving the persistence functionality, as I personally loathe losing data.

I may pick up the project in due time, as I have put many hours into it already and have learned a lot from doing so. However, I am unable to provide any information as to when that would happen.