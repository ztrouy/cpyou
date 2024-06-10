import { AppBar, Box, Button, Toolbar, Typography } from "@mui/material"
import { logout } from "../managers/authManager.js"

export const NavBar = ({ loggedInUser, setLoggedInUser}) => {
    const handleLogout = (e) => {
        e.preventDefault()
        logout().then(() => setLoggedInUser(null))
    }
    
    return (
        <Box sx={{flexGrow: 1}}>
            <AppBar position="static">
                <Toolbar disableGutters sx={{justifyContent: "flex-end", pr: 1}}>
                    <Box flexGrow={1}>
                        <Button sx={{color: "white", ml: 1}}>CP&You</Button>
                    </Box>
                    <Button variant="text" href="/" sx={{color: "white"}}>
                        Builds
                    </Button>
                    <Button variant="text" href="/" sx={{color: "white"}}>
                        New Build
                    </Button>
                    <Box sx={{display: "flex", justifyContent: "flex-end"}}>
                        <Button onClick={e => handleLogout(e)} variant="text" href="/login" sx={{color: "white"}}>
                            Logout
                        </Button>
                    </Box>
                </Toolbar>
            </AppBar>
        </Box>
    )
}

export default NavBar