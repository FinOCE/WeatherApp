import { useEffect, useState } from "react"

export default function useWindowDimensions() {
  function getWindowDimensions() {
    const { innerWidth: width, innerHeight: height } = window
    return {
      width,
      height
    }
  }

  const [dimensions, setDimensions] = useState(getWindowDimensions())

  useEffect(() => {
    const handleResize = () => setDimensions(getWindowDimensions())
    window.addEventListener("resize", handleResize)
    return () => window.removeEventListener("resize", handleResize)
  }, [])

  return { dimensions }
}
