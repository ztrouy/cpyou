import { Route, Routes } from "react-router-dom";
import { AuthorizedRoute } from "./auth/AuthorizedRoute";
import Login from "./auth/Login";
import Register from "./auth/Register";
import BuildsList from "./builds/BuildsList.jsx";
import BuildDetails from "./builds/BuildDetails.jsx";
import BuildForm from "./builds/BuildForm.jsx";
import Home from "./home/Home.jsx";

export const ApplicationViews = () => {
    return (
        <Routes>
            <Route path="/" element={<AuthorizedRoute />}>
                <Route index element={<Home/>}/>
                <Route path="builds">
                    <Route index element={<BuildsList/>}/>
                    <Route path=":buildId">
                        <Route index element={<BuildDetails/>}/>
                        <Route path="edit" element={<BuildForm/>}/>
                    </Route>
                    <Route path="new">
                        <Route index element={<BuildForm/>}/>
                    </Route>
                </Route>
            </Route>
            <Route path="login" element={<Login/>}/>
            <Route path="register" element={<Register/>}/>
            <Route path="*" element={<p>Whoops, nothing here...</p>}/>
        </Routes>
    );
}

export default ApplicationViews
