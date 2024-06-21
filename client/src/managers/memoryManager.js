const _apiUrl = "/api/memory"

export const getMemory = () => {
    return fetch(_apiUrl).then(res => res.json())
}