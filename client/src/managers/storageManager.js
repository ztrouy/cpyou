const _apiUrl = "/api/storage"

export const getStorage = () => {
    return fetch(_apiUrl).then(res => res.json())
}