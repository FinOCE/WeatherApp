import useWindowDimensions from "../hooks/useWindowDimensions"
import { API } from "../types/API"

type HourGraphProps = {
  hourlyForecast: API.HourlyForecast[]
}

export default function HourGraph(props: HourGraphProps) {
  const { dimensions } = useWindowDimensions()

  const forecasts = props.hourlyForecast.slice(0, 24)

  const minimumTemperature = forecasts.reduce((pre, cur) =>
    cur.temperature < pre.temperature ? cur : pre
  ).temperature
  const maximumTemperature = forecasts.reduce((pre, cur) =>
    cur.temperature > pre.temperature ? cur : pre
  ).temperature
  const range = maximumTemperature - minimumTemperature

  function getHeightRatio(temperature: number) {
    return (temperature - minimumTemperature) / range
  }

  const useHalf = dimensions.width < 475

  return (
    <div className="flex flex-row gap-2 mx-2 mt-4 mb-6">
      {forecasts
        .filter((_, i) => (useHalf ? i % 2 === 0 : true))
        .map((forecast, i) => (
          <div
            className={`flex-1 h-[50px] flex flex-row items-end justify-center rounded-full relative ${
              i % 6 === 0 && "bg-[#282B3C]"
            }`}
            key={i}
          >
            <div
              style={{ height: getHeightRatio(forecast.temperature) * 42 + 8 }}
            >
              <div
                className="bg-[#8e44ad] rounded-full h-2 w-2"
                title={String(forecast.temperature) + "°C"}
              />
            </div>
            {i % 6 === 0 && (
              <div className="absolute -bottom-6 text-xs text-[#7f8c8d]">
                {["12am", "6am", "12pm", "6pm"][(i / 6) * (useHalf ? 2 : 1)]}
              </div>
            )}
          </div>
        ))}
    </div>
  )
}
