# ServiceChannel Test

Design and create a RESTful Web API that provides access to COVID-19 data from the following
source:
https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_US.csv

Try to spend no more than 4 to 6 hours on this project, and complete what you can. We will go
over the project in a subsequent discussion.

## Project Requirements

 - Results from the API should be in JSON format.
 - The API should allow input of a location, either a county name or state name, and a date
range. If the date range is blank, all available data for the given location should be used.
If the location is blank or invalid, a friendly/informative error should be returned.
 - Source data is at the county level. If only a State is specified in the search, aggregated
results from all counties in the state should be returned.
 - The following information should be returned from the API as part of every result set
   - The name of the county where the given location is contained (provided in source
data), OR the state name if only a state is specified
   - Lat/long of the county (provided in source data), or null/blank if a state was
specified.
   - Average daily cases for the date range specified (rounded to the nearest tenth).
   - Minimum number of cases in the date range specified, and the date it occurred
on (use earliest date if multiple).
   - Maximum number of cases in the date range specified, and the date it occurred
on (use earliest date if multiple).
 - Create separate endpoints to return other relevant data:
   - Show a daily breakdown of the number of cases per day for the given location
and date range, both total cases AND new cases each day.
   - Show the rate of change (growth rate of new cases) for the given location and
date range.
 - Have a nice documentation page for your API to allow users to test the endpoints. Using
Swagger is recommended.
 - **_BONUS_**: Allow for more robust location input (city, state, zip, etc). Since data is at the
county level, you will need to find the correct county that contains the input location. For example, if the user enters “Pleasanton, CA” as the search location, results for Alameda
County should be returned.
   - You will need to use some kind of geocoding service in order to translate location
input. See https://developers.google.com/maps/documentation/geocoding/intro

## Technical Requirements
-  Written in C# (.Net Core 3.x preferred)
-  Must be able to run without modification on any machine without any source/config
editing.
-  Code should be created/organized for proper maintainability. Be sure to follow good
architectural practices and design, such as SOLID principles.
