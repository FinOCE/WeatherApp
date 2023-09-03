import { API } from "../types/API"

type HourPreviewProps = {
  hourlyForecast: API.HourlyForecast[]
}

export default function HourPreview(props: HourPreviewProps) {
  return (
    <div>
      <p>24 hours of detailed data</p>
      <pre>
        <code>
          {JSON.stringify(props.hourlyForecast?.slice(0, 24), null, 2)}
        </code>
      </pre>
    </div>
  )
}

export function HourPreviewLoading() {
  return <div>Loading</div>
}

export function HourPreviewFallback() {
  return <div>Error occurred</div>
}
