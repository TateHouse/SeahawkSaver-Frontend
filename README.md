<div align="center">
<img src="https://cdn.freebiesupply.com/logos/thumbs/2x/uncw-logo.png" width="300" height="300">
</div>

# Seahawk Saver - Frontend

## Overview

The frontend for my CSC-450: Software Engineering project called `Seahawk Saver`, an application targeted towards
students to efficiently and easily manage their finances.

This frontend is built using `Blazor WASM` with a clean architecture inspired approach mixed with feature folders.
A strong emphasis is placed on SOLID principles and design patterns.

## Future Plans

Even though I have finished the course this project was created for, there are a few things I would still like to do.
Some include adding additional information to each financial model, adding some new functionality to the frontend for
admins and users, and some general cleaning up and refactoring around the codebase. The most important thing to refactor
would be extracting out the hardcoded URIs within the projects for the endpoints. These should be moved into a
configuration file.

I would also like to experiment with automated testing the frontend at various levels.

## Demos

* [User Demo](https://youtu.be/9imzY8K3QiY?si=4UsUuUQkJpYpEMHm)
* [Admin Demo](https://youtu.be/9hXEDy0F0ao?si=huqD1nMd4j8wtYCk)

## Dependencies

Dependencies are managed through `NuGet`. Some of the important packages in the solution include:

* MudBlazor
* Heron.MudCalendar