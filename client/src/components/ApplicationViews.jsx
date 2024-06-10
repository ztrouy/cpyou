import { Route, Routes } from "react-router-dom";
import { AuthorizedRoute } from "./auth/AuthorizedRoute";
import Login from "./auth/Login";
import Register from "./auth/Register";
import BuildsList from "./builds/BuildsList.jsx";
import BuildDetails from "./builds/BuildDetails.jsx";

export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <>Home Page</>
            </AuthorizedRoute>
          }
        />
        <Route path="builds">
          <Route 
            index
            element= {
              <AuthorizedRoute loggedInUser={loggedInUser}>
                <BuildsList loggedInUser={loggedInUser}/>
              </AuthorizedRoute>
            }
          />
          <Route path=":buildId">
            <Route 
              index
              element={
                <AuthorizedRoute loggedInUser={loggedInUser}>
                  <BuildDetails loggedInUser={loggedInUser}/>
                </AuthorizedRoute>
              }
            />
          </Route>
        </Route>
        <Route
          path="login"
          element={<Login setLoggedInUser={setLoggedInUser} />}
        />
        <Route
          path="register"
          element={<Register setLoggedInUser={setLoggedInUser} />}
        />
      </Route>
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}
