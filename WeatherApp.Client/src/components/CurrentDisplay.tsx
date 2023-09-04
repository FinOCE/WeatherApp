import { useEffect, useState } from "react"
import moment from "moment"
import { API } from "../types/API"
import HourGraph from "./HourGraph"
import Spinner from "./Spinner"

type CurrentDisplayProps = {
  currentWeather: API.CurrentWeather
  currentHourIndex: number
  hourlyForecast: API.HourlyForecast[]
}

export default function CurrentDisplay(props: CurrentDisplayProps) {
  const [date, setDate] = useState(new Date())

  useEffect(() => {
    const interval = setInterval(() => setDate(new Date()), 1000)
    return () => clearInterval(interval)
  }, [])

  return (
    <div className="flex flex-col gap-2 border-4 border-[#8e44ad] bg-[#181B2C] rounded-lg p-4">
      <div className="flex flex-col mb-2">
        <span className="text-[#7f8c8d] text-sm">
          {moment(date).format("Do MMMM YYYY")}
        </span>
        <span className="text-lg">{date.toLocaleTimeString()}</span>
      </div>
      <div className="flex flex-col sm:flex-row justify-between gap-y-2">
        <div className="flex flex-col gap-2">
          <div className="flex items-center gap-5">
            <span className="text-4xl font-bold">
              {props.currentWeather.temperature}&deg;C
            </span>
            <div className="flex flex-col -mt-1">
              <span className="text-[#7f8c8d] text-sm">Feels like</span>
              <span className="text-sm">
                {
                  props.hourlyForecast[props.currentHourIndex]
                    .apparentTemperature
                }
                &deg;C
              </span>
            </div>
          </div>
          <span>
            {props.hourlyForecast[props.currentHourIndex].weather.description}
          </span>
        </div>
        <div className="flex flex-col gap-1 text-sm text-[#7f8c8d]">
          <span>
            Chance of rain:{" "}
            {Math.round(
              props.hourlyForecast[props.currentHourIndex]
                .precipitationProbability * 100
            )}
            %
          </span>
          <span>
            UV Index: {props.hourlyForecast[props.currentHourIndex].uvIndex}
          </span>
          <span>
            {Math.round(props.currentWeather.windSpeed)}kph{" "}
            {props.currentWeather.windDirection.direction} Winds
          </span>
        </div>
      </div>
      <div>
        <HourGraph hourlyForecast={props.hourlyForecast} />
      </div>
    </div>
  )
}

export function CurrentDisplayLoading() {
  return (
    <div className="flex items-center justify-center border-[1px] border-[#282B3C] bg-[#181B2C] rounded-lg p-4 h-[278px]">
      <Spinner />
    </div>
  )
}

export function CurrentDisplayFallback() {
  return (
    <div className="flex items-center justify-center border-[1px] border-[#282B3C] bg-[#181B2C] rounded-lg p-4 h-[278px]">
      <span>Invalid location</span>
    </div>
  )
}
