import { useEffect, useState } from "react"
import { Suspended } from "../components/Suspenseful"
import { API } from "../types/API"

const SERVER_URL = "https://localhost:7252"

export default function useWeatherData(location: API.Location) {
  const [reloadCounter, setReloadCounter] = useState(0)

  const [currentWeather, setCurrentWeather] = useState<
    Suspended<API.CurrentWeather>
  >({})
  const [dailyForecast, setDailyForecast] = useState<
    Suspended<API.DailyForecast[]>
  >({})
  const [hourlyForecast, setHourlyForecast] = useState<
    Suspended<API.HourlyForecast[]>
  >({})

  function reload() {
    setCurrentWeather({})
    setDailyForecast({})
    setHourlyForecast({})
    setReloadCounter(prev => prev + 1)
  }

  useEffect(() => {
    if (
      reloadCounter > 0 ||
      location.latitude !== 0 ||
      location.longitude !== 0
    ) {
      const queryParams = `?name=${location.name}&lat=${location.latitude}&long=${location.longitude}&tz=${location.timezone}`

      fetch(`${SERVER_URL}/forecast/current${queryParams}`)
        .then(res => {
          if (res.status !== 200) throw new Error()
          return res
        })
        .then(res => res.json())
        .then((result: API.CurrentWeather) => setCurrentWeather({ result }))
        .catch(error => setCurrentWeather({ error }))

      fetch(`${SERVER_URL}/forecast${queryParams}&type=daily`)
        .then(res => {
          if (res.status !== 200) throw new Error()
          return res
        })
        .then(res => res.json())
        .then((result: API.DailyForecast[]) => setDailyForecast({ result }))
        .catch(error => setDailyForecast({ error }))

      fetch(`${SERVER_URL}/forecast${queryParams}&type=hourly`)
        .then(res => {
          if (res.status !== 200) throw new Error()
          return res
        })
        .then(res => res.json())
        .then((result: API.HourlyForecast[]) => setHourlyForecast({ result }))
        .catch(error => setHourlyForecast({ error }))
    }
  }, [location, reloadCounter])

  return { currentWeather, dailyForecast, hourlyForecast, reload }
}
