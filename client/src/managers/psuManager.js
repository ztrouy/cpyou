const _apiUrl = "/api/psu"

export const getPSUs = () => {
    return fetch(_apiUrl).then(res => res.json())
}