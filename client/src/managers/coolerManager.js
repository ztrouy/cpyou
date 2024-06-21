const _apiUrl = "/api/cooler"

export const getCoolers = () => {
    return fetch(_apiUrl).then(res => res.json())
}