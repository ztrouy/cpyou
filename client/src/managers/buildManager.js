const _apiUrl = "/api/build"

export const getBuilds = () => {
    return fetch(_apiUrl).then(res => res.json())
}