import { createContext, useEffect, useState } from "react";
import "./App.css";
// import "bootstrap/dist/css/bootstrap.min.css";
import { tryGetLoggedInUser } from "./managers/authManager";
import NavBar from "./components/NavBar";
import ApplicationViews from "./components/ApplicationViews";
import { CircularProgress, CssBaseline, ThemeProvider } from "@mui/material";

export const AuthContext = createContext()

function App() {
  const [loggedInUser, setLoggedInUser] = useState();

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
    <>
    {/* <ThemeProvider> */}
      <CssBaseline>
        <AuthContext.Provider value={[loggedInUser, setLoggedInUser]}>
          <NavBar loggedInUser={loggedInUser} setLoggedInUser={setLoggedInUser} />
          <ApplicationViews
            loggedInUser={loggedInUser}
            setLoggedInUser={setLoggedInUser}
          />
        </AuthContext.Provider>
      </CssBaseline>
    {/* </ThemeProvider> */}
    </>
  );
}

export default App;
