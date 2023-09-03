export namespace API {
  export type Location = {
    name: string
    latitude: number
    longitude: number
    timezone: string
  }

  export type Weather = {
    code: number
  }

  export type Cardinal = {
    angle: number
  }

  export type CurrentWeather = {
    location: Location
    weather: Weather
    temperature: number
    windSpeed: number
    windDirection: Cardinal
    isDay: boolean
    time: string
  }

  export type DailyForecast = {
    location: Location
    weather: Weather
    maximumTemperature: number
    minimumTemperature: number
    maximumApparentTemperature: number
    minimumApparentTemperature: number
    sunrise: string
    sunset: string
    uvIndex: number
    precipitationProbability: number
    maximumWindSpeed: number
    windDirection: Cardinal
  }

  export type HourlyForecast = {
    location: Location
    weather: Weather
    temperature: number
    apparentTemperature: number
    precipitationProbability: number
    visibility: number
    maximumWindSpeed: number
    windDirection: Cardinal
    uvIndex: number
    isDay: boolean
  }
}
