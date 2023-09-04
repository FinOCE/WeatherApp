import { API } from "../types/API"
import { Coordinate } from "../types/Coordinate"

type LocationEntryProps = {
  location: Record<keyof API.Location, string>
  realCoords: Coordinate | null
  setLocation: (location: Record<keyof API.Location, string>) => void
  publish: () => void
}

export default function LocationEntry(props: LocationEntryProps) {
  return (
    <div className="border-[1px] border-[#282B3C] bg-[#181B2C] rounded-lg p-4 flex flex-col gap-4">
      <h1 className="text-2xl">WeatherApp</h1>
      <form
        className="flex flex-row gap-4 flex-wrap"
        onSubmit={e => {
          e.preventDefault()
          props.publish()
        }}
      >
        <div className="flex flex-row gap-4">
          <div>
            <label htmlFor="latitude" className="text-[#7f8c8d] text-sm">
              Latitude
            </label>
            <br />
            <input
              id="latitude"
              className="border-[1px] rounded-lg p-4 bg-[#282B3C30] border-[#282B3C] hover:bg-[#282B3C90] w-[136px]"
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
          </div>
          <div>
            <label htmlFor="longitude" className="text-[#7f8c8d] text-sm">
              Longitude
            </label>
            <br />
            <input
              id="longitude"
              className="border-[1px] rounded-lg p-4 bg-[#282B3C30] border-[#282B3C] hover:bg-[#282B3C90] w-[136px]"
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
          </div>
        </div>
        <div className="flex flex-row gap-4">
          <div className="flex items-end">
            <input
              className="border-[1px] rounded-lg p-4 bg-[#282B3C] hover:bg-[#282B3C90] disabled:bg-[#181B2C] border-[#383B4C] cursor-pointer disabled:cursor-not-allowed disabled:text-[#7f8c8d]"
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
                props.location.latitude ===
                  props.realCoords.latitude.toString() &&
                props.location.longitude ===
                  props.realCoords.longitude.toString()
              }
            />
          </div>
          <div className="flex items-end">
            <input
              className="border-[1px] rounded-lg p-4 bg-[#282B3C] border-[#383B4C] cursor-pointer hover:bg-[#282B3C90]"
              type="submit"
              value="Update"
            />
          </div>
        </div>
      </form>
    </div>
  )
}
