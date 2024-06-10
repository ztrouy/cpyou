const _apiUrl = "/api/auth";

export const login = (email, password) => {
    const postOptions = {
        method: "POST",
        credentials: "same-origin",
        headers: {
            Authorization: `Basic ${btoa(`${email}:${password}`)}`,
        }
    }
    
    return fetch(_apiUrl + "/login", postOptions).then((res) => {
        if (res.status !== 200) {
            return Promise.resolve(null);
        } else {
            return tryGetLoggedInUser();
        }
    });
};

export const logout = () => {
    return fetch(_apiUrl + "/logout");
};

export const tryGetLoggedInUser = () => {
    return fetch(_apiUrl + "/me").then((res) => {
        return res.status === 401 ? Promise.resolve(null) : res.json();
    });
};

export const register = (userProfile) => {
    userProfile.password = btoa(userProfile.password);
    
    const postOptions = {
        credentials: "same-origin",
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(userProfile)
    }
    
    return fetch(_apiUrl + "/register", postOptions).then((res) => {
        if (res.ok) {
            return fetch(_apiUrl + "/me").then((res) => res.json());
        } else if (res.status === 400) {
            return res.json();
        } else {
            return Promise.resolve({ errors: ["Unknown registration error"] });
        }
    });
};
