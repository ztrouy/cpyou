const _apiUrl = "/api/reply"

export const createReply = (reply) => {
    const postOptions = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(reply)
    }

    return fetch(_apiUrl, postOptions)
}

export const editReply = (reply) => {
    const putOptions = {
        method: "PUT",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(reply)
    }

    return fetch(`${_apiUrl}/${reply.id}`, putOptions)
}

export const deleteReply = (replyId) => {
    const deleteOptions = {method: "DELETE"}

    return fetch(`${_apiUrl}/${replyId}`, deleteOptions)
}