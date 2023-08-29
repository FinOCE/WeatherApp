# Discussion

This file is updated as plans and decisions are made to demonstrate the thought process during developing this app. New headings are in order of development.

## Code and platform selection

An application like this could be made many different ways, but I've decided I will make it the same way I like to make other projects through client-side rendering. Using server-side rendering works perfectly fine too, however it results in slightly slower response times, since things like data fetching are done prior to anything reaching the client. Client-side rendering is also helpful for a weather app because without anything fancy, the server won't know where the client is geographically to provide helpful weather information on the initial load.

Typically I would build this front-end in something like Next.js, but in the spirit of the project I'm instead going to use Blazor since it uses C#. I've gone with Blazor WASM, meaning the C# is compiled into WebAssembly so it can be run locally unlike a Blazor server which uses SignalR to manage interactions on the page. I've gone with building a web UI instead of a desktop or mobile application since I'm more familiar with this environment. The Blazor app can also be configured as a PWA, meaning it could be installed onto a mobile application and offer a similar user experience to a normal mobile app, so I guess you could say it somewhat fulfills that use case as well.

While this is a simple app that doesn't have much going on in the backend, I've still setup an ASP.NET Core Web API to handle requesting weather data. I've done this to abstract away the exact implementation of the data fetching to separate responsibility away from the front-end app. While this could technically all be handled on the front-end, this wouldn't be so good long-term, as new requirements could make this hard to maintain. 

Funnily enough, the default Blazor app and API template are for displaying weather, but it just comes from a json file and displays is a boring table. Won't be like that for long!

## Creating the weather client

In order to get the weather data, there are many different sources of weather data available. For this implementation, I'm going with Open-Meteo since it is not restrictive with usage and seems to provide all the types of data needed for this application. Rather than requiring an API key with set request limits, it simply asks to not go overboard. If the data source required an API key, it'd be safe in the web API, but without any auth-related features means it could be overworked by someone who wishes to fetch that data themselves without their own key. Either way, the requests come from our API, but at least this case is not giving any extra convenience over the original API. Something like an anti-forgery token could be setup on the Blazor app to avoid these problems, but it isn't the focus of this project so isn't being implemented.

The weather client can be injected using DI to services through its interface, which means that the data source and process to make the requests could be changed entirely as long as it matches the currently defined interface. This interface has its own models, which the current Open-Meteo responses are converted into. The API is setup so that creating a new data source is trivial, just creating a new implementation of the interface within the same folder. An idea for a future change would be to create a client factory instead, which lets you choose which data source is used. Because only one data source is currently implemented, I haven't bothered to do so yet.
