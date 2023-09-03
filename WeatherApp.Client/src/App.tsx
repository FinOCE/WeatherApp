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
import HourPreview, {
  HourPreviewFallback,
  HourPreviewLoading
} from "./components/HourPreview"
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
        const latitude = position.coords.latitude
        const longitude = position.coords.longitude
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
    <div>
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
        data={dailyForecast}
        error={<CurrentDisplayFallback />}
        loading={<CurrentDisplayLoading />}
      >
        <CurrentDisplay currentWeather={currentWeather.result!} />
      </Suspenseful>

      <Suspenseful
        data={dailyForecast}
        error={<WeekPreviewFallback />}
        loading={<WeekPreviewLoading />}
      >
        <WeekPreview dailyForecast={dailyForecast.result!} />
      </Suspenseful>

      <Suspenseful
        data={dailyForecast}
        error={<HourPreviewFallback />}
        loading={<HourPreviewLoading />}
      >
        <HourPreview hourlyForecast={hourlyForecast.result!} />
      </Suspenseful>
    </div>
  )
}
