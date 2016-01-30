# Duty Hours App Overview

This is an application to track duty hours for Residents at medical institutions to 
ensure compliance with ACGME rules.

### Building Process
I've begun the process of building this application. The steps I'm breaking it down
into are going to be as follows:

#### Initial Steps
1. Scaffolding the app's solution. This includes:
    - Setup of projects I know will be needed
    - Pulling in Nuget packages that will be used
    - Adding client libraries
    - Stubbing out the setup of Autofac for IoC
2. Designing the backend data model to support the application
3. Define and build the data layer code
4. Define and stub the API interactions
5. From this point the application should be setup nicely so that remaining modules
    of work should largely be capable of being worked independently and in parallel. 
    Those other pieces/modules are: 
    - Building rules engine to process during analysis of Resident duty hours to 
        ensure compliance.
    - Building UI component for Resident calendar
    - Building UI component for Resident time/shift entry
    - Building UI component for admin users to be able to run analysis for 1 -> many
        Residents

#### Upon completion of the first set of modules:     
1. Build UI component to output analysis details to admins and Residents
2. Integration of the resident calendar module into the analysis page to 
        show details on broken compliance rules

### Next Steps
1. Refactor away from built-in ASP.NET bundling and mification to gulp
2. Add authentication and authorization rules to application to ensure:
    - Resident's who aren't also Admins, cannot run analysis on any other Residents
    - Admins's who aren't residents, cannot enter time/shifts
    - Web Apis needing authorization or decorated with `[Authorize]` attribute