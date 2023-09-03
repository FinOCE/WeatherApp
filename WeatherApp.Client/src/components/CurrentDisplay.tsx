import { API } from "../types/API"

type CurrentDisplayProps = {
  currentWeather: API.CurrentWeather
}

export default function CurrentDisplay(props: CurrentDisplayProps) {
  return (
    <div>
      <p>Current weather info</p>
      <pre>
        <code>{JSON.stringify(props.currentWeather, null, 2)}</code>
      </pre>
    </div>
  )
}

export function CurrentDisplayLoading() {
  return <div>Loading</div>
}

export function CurrentDisplayFallback() {
  return <div>Error occurred</div>
}
