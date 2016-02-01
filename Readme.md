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
3. Build out the ability for the Resident to save shift details

#### Upon completion of the first set of modules:     
1. Get the compliance analysis working for a single patient
    - use a generic rules engine to process the 28 days of shifts
    - rules should implement a common interface and be very easy to add
2. Get the admin analysis built where the user will be capable of running
    analysis for more than 1 resident
3. Build the schedule directive to show resident shifts on a calendar. In 
    looking I found 2 options that might be worthwhile: 
    - [Ui Calendar](http://angular-ui.github.io/ui-calendar/)
    - [Angular-Bootstrap-Calendar](https://github.com/mattlewis92/angular-bootstrap-calendar)
4. Build the analysis results view showing details on compliance rules broken
    - details on the shifts causing break
    - ability to click shift and see it in the resident's calendar
5. Add authentication and authorization rules to application to ensure:
    - Resident's who aren't also Admins, cannot run analysis on any other Residents
    - Admins's who aren't residents, cannot enter time/shifts
    - Web Apis needing authorization or decorated with `[Authorize]` attribute

#### Long-Term Steps
1. Refactor away from built-in ASP.NET bundling and mification to gulp
2. Caching impelementation
3. More robust logging
4. Split up Mapper class (or preferably implement AutoMapper)

#### What's I'm not Happy With
1. UI Design and ease of use
2. Lack of tests
3. I encountered an EF problem I haven't squashed yet. When saving a time entry for 
    a resident, it's adding/attaching the detached entity model, getting through save 
    changes without error, and the entity returned has the auto-identity from the 
    table, but the record isn't persisted. From reading about it, it may have 
    something to do with it being a local mdf file, but I haven't figured it out
    quite yet.
4. I was hoping to get as far as building out the rules engine and at least writing
    some tests against it.