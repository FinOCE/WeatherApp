import { useEffect, useState } from "react"
import { API } from "./types/API"
import WeekPreview, {
  WeekPreviewFallback,
  WeekPreviewLoading
} from "./components/WeekPreview"
import Suspenseful from "./components/Suspenseful"
import CurrentDisplay, {
  CurrentDisplayFallback,
  CurrentDisplayLoading
} from "./components/CurrentDisplay"
import LocationEntry from "./components/LocationEntry"
import { Draft } from "./types/Draft"
import useWeatherData from "./hooks/useWeatherData"
import { Coordinate } from "./types/Coordinate"

export default function App() {
  const [location, setLocation] = useState<Draft<API.Location>>({
    published: {
      latitude: 0,
      longitude: 0,
      name: "UNKNOWN",
      timezone: "GMT"
    },
    draft: {
      latitude: "0",
      longitude: "0",
      name: "UNKNOWN",
      timezone: "GMT"
    }
  })

  const { currentWeather, dailyForecast, hourlyForecast, reload } =
    useWeatherData(location.published)

  const [realCoords, setRealCoords] = useState<Coordinate | null>(null)

  useEffect(() => {
    if (navigator.geolocation)
      navigator.geolocation.getCurrentPosition(position => {
        const latitude = Number(position.coords.latitude.toFixed(3))
        const longitude = Number(position.coords.longitude.toFixed(3))
        const name = "UNKNOWN"
        const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone

        setLocation({
          published: { latitude, longitude, name, timezone },
          draft: {
            latitude: latitude.toString(),
            longitude: longitude.toString(),
            name,
            timezone
          }
        })

        setRealCoords({ latitude, longitude })
      })
  }, [])

  return (
    <div className="bg-[#080B1C] text-[#fff] min-w-[100vw] min-h-[100vh]">
      <div className="flex justify-center min-w-[355px]">
        <div className="w-[90vw] max-w-[934px] overflow-hidden">
          <div className="flex flex-col gap-4 my-4">
            <LocationEntry
              location={location.draft}
              realCoords={realCoords}
              setLocation={(location: Record<keyof API.Location, string>) =>
                setLocation(prev => ({ ...prev, draft: location }))
              }
              publish={() => {
                setLocation(prev => ({
                  ...prev,
                  published: {
                    ...prev.draft,
                    latitude: Number(prev.draft.latitude),
                    longitude: Number(prev.draft.longitude)
                  }
                }))
                reload()
              }}
            />

            <Suspenseful
              data={[currentWeather, hourlyForecast]}
              error={<CurrentDisplayFallback />}
              loading={<CurrentDisplayLoading />}
            >
              <CurrentDisplay
                currentWeather={currentWeather.result!}
                currentHourIndex={
                  hourlyForecast.result?.findIndex(
                    forecast =>
                      new Date(forecast.time).getUTCHours() ===
                      new Date().getUTCHours()
                  )!
                }
                hourlyForecast={hourlyForecast.result!}
              />
            </Suspenseful>

            <Suspenseful
              data={dailyForecast}
              error={<WeekPreviewFallback />}
              loading={<WeekPreviewLoading />}
            >
              <WeekPreview dailyForecast={dailyForecast.result!} />
            </Suspenseful>
          </div>
        </div>
      </div>
    </div>
  )
}
