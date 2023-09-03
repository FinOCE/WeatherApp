import { useEffect, useState } from "react"
import { API } from "./types/API"
import WeekPreview, {
  WeekPreviewFallback,
  WeekPreviewLoading
} from "./components/WeekPreview"
import Suspenseful, { Suspended } from "./components/Suspenseful"
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

const SERVER_URL = "https://localhost:7252"

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

  const [realCoords, setRealCoords] = useState<{
    latitude: number
    longitude: number
  } | null>(null)

  const [currentWeather, setCurrentWeather] = useState<
    Suspended<API.CurrentWeather>
  >({})
  const [dailyForecast, setDailyForecast] = useState<
    Suspended<API.DailyForecast[]>
  >({})
  const [hourlyForecast, setHourlyForecast] = useState<
    Suspended<API.HourlyForecast[]>
  >({})

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

  useEffect(() => {
    const queryParams = `?name=${location.published.name}&lat=${location.published.latitude}&long=${location.published.longitude}&tz=${location.published.timezone}`

    fetch(`${SERVER_URL}/forecast/current${queryParams}`)
      .then(res => res.json())
      .then((result: API.CurrentWeather) => setCurrentWeather({ result }))
      .catch(error => setCurrentWeather({ error }))

    fetch(`${SERVER_URL}/forecast${queryParams}&type=daily`)
      .then(res => res.json())
      .then((result: API.DailyForecast[]) => setDailyForecast({ result }))
      .catch(error => setDailyForecast({ error }))

    fetch(`${SERVER_URL}/forecast${queryParams}&type=hourly`)
      .then(res => res.json())
      .then((result: API.HourlyForecast[]) => setHourlyForecast({ result }))
      .catch(error => setHourlyForecast({ error }))
  }, [location.published])

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
          setCurrentWeather({})
          setDailyForecast({})
          setHourlyForecast({})
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
