import { Box, Button, Chip, CircularProgress, Container, Divider, List, ListItem, ListItemAvatar, ListItemText, Paper, Stack, Typography } from "@mui/material"
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
            
            return createComment(comment).then(res => {
                if (res.ok) {
                    refreshPage()
                    toggleCommentModal()
                    return true
                } else {
                    return false
                }
        })
    }

    const standardComponent = (component) => {
        return (
            <Box sx={{display: "flex", flexDirection: "row"}}>
                <Box sx={{display: "flex", flexGrow: 1, alignItems: "center"}}>
                    <Typography>{component.name}</Typography>
                </Box>
                <Typography>{component.price.toLocaleString("en-US", {style:"currency", currency:"USD"})}</Typography>
            </Box>
        )
    }

    const multiComponent = (component, keyName) => {
        return (
            <Box sx={{display: "flex", flexDirection: "row"}} key={`${keyName}-${component.id}`}>
                <Box sx={{display: "flex", flexDirection: "row", flexGrow: 1, alignItems: "center", gap: 1}}>
                    <Chip label={component.quantity} size="small"/>
                    <Typography>{component.name} {component.sizeGB}GB</Typography>
                </Box>
                <Typography>{(component.price * component.quantity).toLocaleString("en-US", {style:"currency", currency:"USD"})}</Typography>
            </Box>
        )
    }
    
    if (!build) {
        return <CircularProgress/>
    }

    return (
        <Container sx={{py: 5}}>
            <Stack direction={"column"} alignItems={"center"}>
                <Paper elevation={2} sx={{p: 3, width: 1, maxWidth: "800px"}}>
                    <Typography variant="h4">{build.name}</Typography>
                    <Box sx={{my: 2}}>
                        <Typography>{build.userProfile.userName}</Typography>
                        <Typography>{build.formattedDateCreated}</Typography>
                    </Box>
                    <Typography>{build.content}</Typography>
                    <Typography variant="h5" marginTop={2}>Parts</Typography>
                    <Stack direction={"column"} spacing={1} divider={<Divider orientation="horizontal" flexItem />}>
                        <Box>
                            <Typography variant="h6">CPU</Typography>
                            {standardComponent(build.cpu)}
                        </Box>
                        <Box>
                            <Typography variant="h6">GPU</Typography>
                            {standardComponent(build.gpu)}
                        </Box>
                        <Box>
                            <Typography variant="h6">PSU</Typography>
                            {standardComponent(build.psu)}
                        </Box>
                        <Box>
                            <Typography variant="h6">Motherboard</Typography>
                            {standardComponent(build.motherboard)}
                        </Box>
                        <Box>
                            <Typography variant="h6">Cooler</Typography>
                            {standardComponent(build.cooler)}
                        </Box>
                        <Box paddingBottom={0.5}>
                            <Typography variant="h6">Memory</Typography>
                            <Stack spacing={1}>
                                {build.memory.map(m => multiComponent(m, "memory"))}
                            </Stack>
                        </Box>
                        <Box paddingBottom={0.5}>
                            <Typography variant="h6">Storage</Typography>
                            <Stack spacing={1}>
                                {build.storage.map(s => multiComponent(s, "storage"))}
                            </Stack>
                        </Box>
                        <Typography textAlign={"end"} fontWeight={"bold"} paddingTop={1}>{build.totalPrice.toLocaleString("en-US", {style:"currency", currency:"USD"})}</Typography>
                    </Stack>
                    {build.userProfileId == loggedInUser.id && (
                        <Stack direction={"row"} justifyContent={"end"} spacing={1} marginTop={2}>
                            <Button variant="contained" onClick={() => navigate("edit")}>Edit</Button>
                            <Button variant="contained" onClick={() => toggleDeleteModal()}>Delete</Button>
                        </Stack>
                    )}
                </Paper>
                <Box sx={{display: "flex", flexDirection: "column", gap: 1, width: 1, maxWidth: "800px"}}>
                    <Box sx={{display: "flex", flexDirection: "row", alignContent: "center", mt: 1, gap: 1}}>
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
            </Stack>
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
