import { API } from "../types/API"

type DayPreviewProps = {
  dailyForecast: API.DailyForecast[]
}

export default function WeekPreview(props: DayPreviewProps) {
  return (
    <div>
      <p>The upcoming week</p>
      <pre>
        <code>{JSON.stringify(props.dailyForecast?.slice(0, 7), null, 2)}</code>
      </pre>
    </div>
  )
}

export function WeekPreviewLoading() {
  return <div>Loading</div>
}

export function WeekPreviewFallback() {
  return <div>Error occurred</div>
}
