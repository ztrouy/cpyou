import { AppBar, Box, Button, Toolbar } from "@mui/material"
import { logout } from "../managers/authManager.js"
import useAuthorizationProvider from "../shared/hooks/authorization/useAuthorizationProvider.js"

export const NavBar = () => {
    const { loggedInUser, setLoggedInUser } = useAuthorizationProvider()
    
    const handleLogout = (e) => {
        e.preventDefault()
        logout().then(() => setLoggedInUser(null))
    }
    
    return (
        <Box sx={{flexGrow: 1}}>
            <AppBar position="static">
                <Toolbar disableGutters sx={{justifyContent: "flex-end", pr: 1}}>
                    <Box flexGrow={1}>
                        <Button sx={{color: "white", ml: 1}} href="/">CP&You</Button>
                    </Box>
                    {loggedInUser ? (
                        <>
                            <Button variant="text" href="/builds" sx={{color: "white"}}>
                                Builds
                            </Button>
                            <Button variant="text" href="/builds/new" sx={{color: "white"}}>
                                New Build
                            </Button>
                            <Button onClick={e => handleLogout(e)} variant="text" href="/login" sx={{color: "white"}}>
                                Logout
                            </Button>
                        </>
                    ) : (
                        <Button variant="text" href="/login" sx={{color: "white"}}>
                            Login
                        </Button>
                    )}
                </Toolbar>
            </AppBar>
        </Box>
    )
}

export default NavBar