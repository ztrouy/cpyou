const _apiUrl = "/api/build"

export const getBuilds = () => {
    return fetch(_apiUrl).then(res => res.json())
}

export const getSingleBuild = (buildId) => {
    return fetch(`${_apiUrl}/${buildId}`).then(res => res.json())
}