import { API } from "../types/API"

type LocationEntryProps = {
  location: Record<keyof API.Location, string>
  realCoords: { latitude: number; longitude: number } | null
  setLocation: (location: Record<keyof API.Location, string>) => void
  publish: () => void
}

export default function LocationEntry(props: LocationEntryProps) {
  return (
    <form
      onSubmit={e => {
        e.preventDefault()
        props.publish()
      }}
    >
      <input
        type="text"
        value={props.location.latitude}
        onChange={e =>
          props.setLocation({
            ...props.location,
            latitude: e.currentTarget.value.replaceAll(/[^\d.]*/g, "")
          })
        }
        placeholder="Enter latitude here"
      />
      <br />
      <input
        type="text"
        value={props.location.longitude}
        onChange={e =>
          props.setLocation({
            ...props.location,
            longitude: e.currentTarget.value.replaceAll(/[^\d.]*/g, "")
          })
        }
        placeholder="Enter longitude here"
      />
      <br />
      <input
        type="button"
        value="Use current location"
        onClick={() =>
          props.realCoords &&
          props.setLocation({
            ...props.location,
            latitude: props.realCoords.latitude.toString(),
            longitude: props.realCoords.longitude.toString()
          })
        }
        disabled={
          !!props.realCoords &&
          props.location.latitude === props.realCoords.latitude.toString() &&
          props.location.longitude === props.realCoords.longitude.toString()
        }
      />
      <br />
      <input type="submit" value="Submit" />
      <br />
      <br />
    </form>
  )
}
