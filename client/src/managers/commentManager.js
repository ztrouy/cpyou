const _apiUrl = "/api/comment"

export const createComment = (comment) => {
    const postOptions = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(comment)
    }

    return fetch(_apiUrl, postOptions)
}

export const editComment = (comment) => {
    const putOptions = {
        method: "PUT",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(comment)
    }

    return fetch(`${_apiUrl}/${comment.id}`, putOptions)
}

export const deleteComment = (commentId) => {
    const deleteOptions = {method: "DELETE"}

    return fetch(`${_apiUrl}/${commentId}`, deleteOptions)
}