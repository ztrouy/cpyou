const _apiUrl = "/api/build"

export const getBuilds = () => {
    return fetch(_apiUrl).then(res => res.json())
}

export const getSingleBuild = (buildId) => {
    return fetch(`${_apiUrl}/${buildId}`).then(res => res.json())
}

export const getSingleBuildForEdit = (buildId) => {
    return fetch(`${_apiUrl}/${buildId}/edit`).then(res => res.json())
}

export const createBuild = (build) => {
    const postOptions = {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(build)
    }

    return fetch(_apiUrl, postOptions).then(res => res.headers.get("Location"))
}

export const updateBuild = (buildId, build) => {
    const putOptions = {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(build)
    }

    return fetch(`${_apiUrl}/${buildId}`, putOptions)
}