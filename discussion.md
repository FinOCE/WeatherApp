# Discussion

This file is updated as plans and decisions are made to demonstrate the thought process during developing this app. New headings are in order of development.

## Code and platform selection

An application like this could be made many different ways, but I've decided I will make it the same way I like to make other projects through client-side rendering. Using server-side rendering works perfectly fine too, however it results in slightly slower response times, since things like data fetching are done prior to anything reaching the client. Client-side rendering is also helpful for a weather app because without anything fancy, the server won't know where the client is geographically to provide helpful weather information on the initial load.

For the front-end, I'll be sticking to what I'm comfortable with and building it with React for a web UI. Initially I wanted to use Blazor, but after a bit of tinkering around with it there were quite a few quirks I wasn't used to, so for the sake of not spending even more time on this project than necessary, I'll just do what I'm used to. It communicates with the back-end over HTTP, so it isn't like using a different language matters. The task description doesn't explicitly say the front-end needs to be written in C#, so I don't think this is breaking any rules.

While this is a simple app that doesn't have much going on in the backend, I've still setup an ASP.NET Core Web API to handle requesting weather data. I've done this to abstract away the exact implementation of the data fetching to separate responsibility away from the front-end app. While this could technically all be handled on the front-end, this wouldn't be so good long-term, as new requirements could make this hard to maintain. 

Funnily enough, the default API template are for displaying weather, but it just comes from a json file and displays is a boring table. Won't be like that for long!

## Creating the weather client

In order to get the weather data, there are many different sources of weather data available. For this implementation, I'm going with Open-Meteo since it is not restrictive with usage and seems to provide all the types of data needed for this application. Rather than requiring an API key with set request limits, it simply asks to not go overboard. If the data source required an API key, it'd be safe in the web API, but without any auth-related features means it could be overworked by someone who wishes to fetch that data themselves without their own key. Either way, the requests come from our API, but at least this case is not giving any extra convenience over the original API. Something like an anti-forgery token could be setup on the app to avoid these problems, but it isn't the focus of this project so isn't being implemented.

The weather client can be injected using DI to services through its interface, which means that the data source and process to make the requests could be changed entirely as long as it matches the currently defined interface. This interface has its own models, which the current Open-Meteo responses are converted into. The API is setup so that creating a new data source is trivial, just creating a new implementation of the interface within the same folder. An idea for a future change would be to create a client factory instead, which lets you choose which data source is used. Because only one data source is currently implemented, I haven't bothered to do so yet.

The weather service class is essentially a wrapper for the current weather client. It does nothing other than call the identical name on the client. The reason it exists rather than injecting the client into the controller is so that future iterations of the app would be able to easily add business logic to the service, rather than refactoring to introduce it.

## Testing the server

In order to test the server, I've created projects for unit and integration tests on it. These are used to test essentially all of the code in the actual server project. Both these test projects are using MSTest and I'm using NSubstitute for mocking.

## Creating the API controller

Rather than making separate endpoints for the different types of forecasts, I thought it'd make sense to just keep them all under the same, using a query parameter to decide which to fetch. Both this and the current weather endpoint also ask for location information as part of the query parameters, since they are GET requests and the server needs to know where to be fetching data for. Currently, it requires the client send the name of the location as well, even though it just gets passed through to the response, not changing the operation. I've done this because if I wanted to iterate on this I would change it so it fetches from a different third-party API to get the location name from the coordinates or vice-versa, meaning it'd be EITHER the latitude/longitude, OR the location name. For now, it requires both so they can both be included in the response.

As a result of this, the two exposed endpoints are:

- `/weather`
- `/weather/current`

With the required query parameters, they are:

- `/weather?name=PlaceName&lat=0&long=0&type=daily` (type can be `daily`, `hourly`, `current-daily`, or `current-hourly`)
- `/weather/current?name=PlaceName&lat=0&long=0`

With this done, the server is complete, and I can move onto the front-end.
