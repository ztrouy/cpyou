const _apiUrl = "/api/motherboard"

export const getMotherboards = () => {
    return fetch(_apiUrl).then(res => res.json())
}