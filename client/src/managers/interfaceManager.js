const _apiUrl = "/api/interface"

export const getInterfaces = () => {
    return fetch(_apiUrl).then(res => res.json())
}