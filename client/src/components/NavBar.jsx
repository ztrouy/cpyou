import { AppBar, Box, Button, ButtonBase, Toolbar } from "@mui/material"
import { logout } from "../managers/authManager.js"
import useAuthorizationProvider from "../shared/hooks/authorization/useAuthorizationProvider.js"
import LogoWhite from "../assets/LogoWhite.svg"

export const NavBar = () => {
    const { loggedInUser, setLoggedInUser } = useAuthorizationProvider()
    
    const handleLogout = (e) => {
        e.preventDefault()
        logout().then(() => setLoggedInUser(null))
    }
    
    return (
        <AppBar position="sticky">
            <Toolbar disableGutters sx={{justifyContent: "flex-end", pr: 1}}>
                <Box flexGrow={1}>
                        <ButtonBase href="/">
                            <Box sx={{ml: 2, mt: 0.5}}>
                                <img src={LogoWhite} color="white" height={"30px"}/>
                            </Box>
                        </ButtonBase>
                </Box>
                {loggedInUser ? (
                    <>
                        <Button variant="text" href="/builds" sx={{color: "white"}}>
                            Builds
                        </Button>
                        <Button variant="text" href="/builds/my" sx={{color: "white"}}>
                            My Builds
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
    )
}

export default NavBar