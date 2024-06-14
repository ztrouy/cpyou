import { useEffect } from "react";
import { tryGetLoggedInUser } from "./managers/authManager";
import NavBar from "./components/NavBar";
import ApplicationViews from "./components/ApplicationViews";
import { CircularProgress, CssBaseline } from "@mui/material";
import useAuthorizationProvider from "./shared/hooks/authorization/useAuthorizationProvider.js";

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
        <CssBaseline>
            <NavBar/>
            <ApplicationViews/>
        </CssBaseline>
    );
}

export default App;
