const _apiUrl = "/api/comment"

export const createComment = (comment) => {
    const postOptions = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(comment)
    }

    return fetch(_apiUrl, postOptions)
}