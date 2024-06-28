import { createTheme } from "@mui/material"
import { indigo } from "@mui/material/colors"

export const baseTheme = createTheme({
    palette: {
        mode: "light",
        primary: indigo
    }
})

export default baseTheme