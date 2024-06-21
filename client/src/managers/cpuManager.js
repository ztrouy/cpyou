const _apiUrl = "/api/cpu"

export const getCPUs = () => {
    return fetch(_apiUrl).then(res => res.json())
}