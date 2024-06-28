import { useEffect } from "react";
import { tryGetLoggedInUser } from "./managers/authManager";
import NavBar from "./components/NavBar";
import ApplicationViews from "./components/ApplicationViews";
import { CircularProgress, CssBaseline, ThemeProvider } from "@mui/material";
import useAuthorizationProvider from "./shared/hooks/authorization/useAuthorizationProvider.js";
import baseTheme from "./shared/hooks/theme/baseTheme.jsx";

const App = () => {
    const { loggedInUser, setLoggedInUser } = useAuthorizationProvider()

    useEffect(() => {
        // user will be null if not authenticated
        tryGetLoggedInUser().then((user) => {
            setLoggedInUser(user);
        });
    }, []);

    // wait to get a definite logged-in state before rendering
    if (loggedInUser === undefined) {
        return <CircularProgress />;
    }

    return (
        <ThemeProvider theme={baseTheme}>
            <CssBaseline>
                <NavBar/>
                <ApplicationViews/>
            </CssBaseline>
        </ThemeProvider>
    );
}

export default App;
