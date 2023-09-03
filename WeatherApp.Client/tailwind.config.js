const defaultTheme = require('tailwindcss/defaultTheme')

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {
      fontFamily: {
        serif: ["Libre Baskerville", ...defaultTheme.fontFamily.serif],
        sans: ["Libre Franklin", ...defaultTheme.fontFamily.sans]
      }
    }
  },
  plugins: [],
}
