const _apiUrl = "/api/component"

export const getComponents = () => {
    return fetch(_apiUrl).then(res => res.json())
}