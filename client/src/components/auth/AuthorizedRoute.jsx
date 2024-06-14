import { Navigate, Outlet } from "react-router-dom";
import useAuthorizationProvider from "../../shared/hooks/authorization/useAuthorizationProvider.js";

// This component returns a Route that either display the prop element
// or navigates to the login. If roles are provided, the route will require
// all of the roles when all is true, or any of the roles when all is false
export const AuthorizedRoute = ({ roles, all }) => {
    const { loggedInUser } = useAuthorizationProvider()
    
    let authed = false;
    if (loggedInUser) {
        if (roles && roles.length) {
            authed = all
                ? roles.every((r) => loggedInUser.roles.includes(r))
                : roles.some((r) => loggedInUser.roles.includes(r));
        } else {
            authed = true;
        }
    }

    return authed ? <Outlet /> : <Navigate to="/login" />;
};
