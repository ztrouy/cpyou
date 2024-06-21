const _apiUrl = "/api/gpu"

export const getGPUs = () => {
    return fetch(_apiUrl).then(res => res.json())
}