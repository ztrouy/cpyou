import { Box, Button, Chip, CircularProgress, Container, Divider, List, ListItem, ListItemAvatar, ListItemText, Paper, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { deleteBuild, getSingleBuild } from "../../managers/buildManager.js"
import DeleteModal from "../modals/DeleteModal.jsx"
import Comment from "../comments/Comment.jsx"
import useAuthorizationProvider from "../../shared/hooks/authorization/useAuthorizationProvider.js"
import CommentModal from "../modals/CommentModal.jsx"
import { createComment } from "../../managers/commentManager.js"

export const BuildDetails = () => {
    const [build, setBuild] = useState(null)
    const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false)
    const [isCommentModalOpen, setIsCommentModalOpen] = useState(false)
    const { loggedInUser } = useAuthorizationProvider()

    const { buildId } = useParams()

    const navigate = useNavigate()

    useEffect(() => {
        getSingleBuild(buildId).then(setBuild)
    }, [buildId])

    const refreshPage = () => {
        getSingleBuild(buildId).then(setBuild)
    }
    
    const toggleDeleteModal = () => {
        setIsDeleteModalOpen(!isDeleteModalOpen)
    }

    const handleDelete = () => {
        deleteBuild(buildId).then(() => {
            navigate("/builds")
        })
    }

    const toggleCommentModal = () => {
        setIsCommentModalOpen(!isCommentModalOpen)
    }

    const handleSubmitComment = (content) => {
        const comment = {
            buildId: buildId,
            userProfileId: loggedInUser.id,
            content: content
        }

        createComment(comment).then(() => {
            toggleCommentModal()
            refreshPage()
        })
    }
    
    if (!build) {
        return <CircularProgress/>
    }

    return (
        <Container sx={{py: 5}}>
            <Paper elevation={2} sx={{p: 2}}>
                <Typography variant="h4">{build.name}</Typography>
                <Box sx={{my: 2}}>
                    <Typography>{build.userProfile.userName}</Typography>
                    <Typography>{build.formattedDateCreated}</Typography>
                </Box>
                <Typography>{build.content}</Typography>
                <Box sx={{mt: 2}}>
                    <Typography variant="h5">Parts</Typography>
                    {build.components.map(c => (
                        <Box sx={{display: "flex", flexDirection: "row"}} key={`c-${c.id}`}>
                            <Box sx={{display: "flex", flexDirection: "row", flexGrow: 1, alignItems: "center", gap: 1}}>
                                <Chip label={c.quantity}/>
                                <Typography>{c.name}</Typography>
                            </Box>
                            <Typography>{(c.price * c.quantity).toLocaleString("en-US", {style:"currency", currency:"USD"})}</Typography>
                        </Box>
                    ))}
                    <Typography textAlign={"end"} fontWeight={"bold"}>{build.totalPrice.toLocaleString("en-US", {style:"currency", currency:"USD"})}</Typography>
                </Box>
                {build.userProfileId == loggedInUser.id && (
                    <Box sx={{display: "flex", justifyContent: "end", mt: 1, gap: 1}}>
                        <Button variant="contained" onClick={() => navigate("edit")}>Edit</Button>
                        <Button variant="contained" onClick={() => toggleDeleteModal()}>Delete</Button>
                    </Box>
                )}
            </Paper>
            <Box sx={{display: "flex", flexDirection: "column", gap: 1}}>
                <Box sx={{display: "flex", flexDirection: "row", alignContent: "center", gap: 1}}>
                    <Typography variant="h5" marginY={1}>Comments</Typography>
                    <Box sx={{alignContent: "center", pt: 0.5}}>
                        <Button onClick={() => toggleCommentModal()}>Comment</Button>
                    </Box>
                </Box>
                {build.comments?.map(c => (
                    <Comment comment={c} refreshPage={refreshPage} key={`comment-${c.id}`}/>
                    ))}
                {build.comments?.length == 0 && (
                    <Typography textAlign={"center"}>There are no comments</Typography>
                )}
            </Box>
            <DeleteModal 
                isOpen={isDeleteModalOpen}
                toggle={toggleDeleteModal}
                confirmDelete={handleDelete}
                typeName={"Build"}
            />
            <CommentModal 
                isOpen={isCommentModalOpen}
                toggle={toggleCommentModal}
                submit={handleSubmitComment}
                typeName={"Comment"}
            />
        </Container>
    )
}

export default BuildDetails
