import { API } from "../types/API"

type DayPreviewProps = {
  dailyForecast: API.DailyForecast[]
}

export default function WeekPreview(props: DayPreviewProps) {
  function getDayName(index: number) {
    const day = (new Date().getDay() - 1 + index) % 7

    switch (day) {
      case 0:
        return "Mon"
      case 1:
        return "Tue"
      case 2:
        return "Wed"
      case 3:
        return "Thu"
      case 4:
        return "Fri"
      case 5:
        return "Sat"
      case 6:
        return "Sun"
    }
  }

  return (
    <div>
      <div className="flex flex-row overflow-auto gap-4 border-[1px] border-[#282B3C] bg-[#181B2C] rounded-lg p-4">
        {props.dailyForecast?.slice(0).map((forecast, i) => (
          <div
            className={`flex flex-col gap-2 rounded-lg p-4 ${
              i === 0
                ? "bg-[#282B3C] border-[#8e44ad] border-4"
                : "bg-[#282B3C30] border-[#282B3C] border-[1px]"
            }`}
            key={i}
          >
            <div className="w-20" />
            <div className="text-center font-bold text-sm">
              {getDayName(i)}
              <span className="text-[#7f8c8d] inline-block mx-2">&middot;</span>
              {new Date().getDate() + i}
            </div>

            <div className="flex flex-row gap-2 justify-center ml-1 text-xs">
              <span>{forecast.maximumTemperature.toFixed(0)}&deg;</span>
              <span className="text-[#7f8c8d]">
                {forecast.minimumTemperature.toFixed(0)}&deg;
              </span>
            </div>
            <div className="text-center text-sm mt-2">
              {forecast.weather.description}
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}

export function WeekPreviewLoading() {
  return (
    <div>
      <div className="flex flex-row relative overflow-auto gap-4 border-[1px] border-[#282B3C] bg-[#181B2C] rounded-lg p-4">
        {Array.from({ length: 7 }).map((_, i) => (
          <div
            className="flex flex-col gap-2 border-[1px] rounded-lg p-4 bg-[#282B3C30] border-[#282B3C] h-[148px] w-[114px]"
            key={i}
          />
        ))}
      </div>
    </div>
  )
}

export function WeekPreviewFallback() {
  return (
    <div>
      <div className="flex flex-row relative overflow-auto gap-4 border-[1px] border-[#282B3C] bg-[#181B2C] rounded-lg p-4">
        {Array.from({ length: 7 }).map((_, i) => (
          <div
            className="flex flex-col gap-2 border-[1px] rounded-lg p-4 bg-[#282B3C30] border-[#282B3C] h-[148px] w-[114px]"
            key={i}
          />
        ))}
      </div>
    </div>
  )
}
