# MoneySmart

[![Build Status][build-status-badge]][build-status]
[![Quality Gate Status][quality-gate-status-badge]][quality-gate-status]
[![Coverage Status][coverage-status-badge]][coverage-status]

ASP.NET Core Razor Pages Web Application with EF Core and ASP.NET Core Identity.

Get ready to manage your income and expenses the smart way!

## Overview

A very simple Web Application to capture everyday income and expenses.

### Features (WIP)

1. [x] Capture your daily income and expenses.
2. [x] Manage multiple accounts to separate your transactions (e. g. checking or savings).
3. [ ] Classify your transactions in categories.
4. [ ] More to come...

## Prerequisites

- Visual Studio 2019
- ASP.NET Core 3.1

## Getting started

1. Clone the project.
2. Open the solution file `MoneySmart.sln` on Visual Studio.
3. In the Solution Explorer right-click the `MoneySmart.Database` project and click "Publish..."
4. In the Publish dialog click "Load Profile..." and select the `MoneySmart.Database.publish.xml` file.
5. Confirm the loaded connection string matches your target instance ((localdb)\MSSQLLocalDB by default).
6. Press F5 to start the build and run the application.
7. Wait for your default browser to be automatically launched into <https://localhost:44360>

## License

    MIT License

    Copyright (c) 2019 Felipe Romero

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.

[build-status-badge]: https://dev.azure.com/feliperomeromx/Projects/_apis/build/status/feliperomero3.MoneySmart?branchName=master
[build-status]: https://dev.azure.com/feliperomeromx/Projects/_build/latest?definitionId=9&branchName=master
[quality-gate-status-badge]: https://sonarcloud.io/api/project_badges/measure?project=feliperomero3_MoneySmart&metric=alert_status
[quality-gate-status]: https://sonarcloud.io/dashboard?id=feliperomero3_MoneySmart
[coverage-status-badge]: https://sonarcloud.io/api/project_badges/measure?project=feliperomero3_MoneySmart&metric=coverage
[coverage-status]: https://sonarcloud.io/dashboard?id=feliperomero3_MoneySmart
