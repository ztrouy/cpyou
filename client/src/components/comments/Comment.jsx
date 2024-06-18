import { Avatar, Box, Button, Card, CardActions, CardContent, Collapse, Divider, IconButton, List, ListItem, ListItemAvatar, ListItemText, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import AddReaction from "@mui/icons-material/AddReaction";
import CommentIcon from "@mui/icons-material/Comment";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import EditRemoveMenu from "../menus/EditRemoveMenu.jsx";
import useAuthorizationProvider from "../../shared/hooks/authorization/useAuthorizationProvider.js";
import CommentModal from "../modals/CommentModal.jsx";
import { createReply, deleteReply } from "../../managers/replyManager.js";
import DeleteModal from "../modals/DeleteModal.jsx";
import { deleteComment } from "../../managers/commentManager.js";

export const Comment = ({ comment, refreshPage }) => {
    const [isExpanded, setIsExpanded] = useState(false)
    const [isCommentModalOpen, setIsCommentModalOpen] = useState(false)
    const [objectToEdit, setObjectToEdit] = useState(null)
    const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false)
    const [objectToDelete, setObjectToDelete] = useState(null)
    const [modalTypeName, setModalTypeName] = useState("")
    const { loggedInUser } = useAuthorizationProvider()
    
    const toggleExpand = () => {
        setIsExpanded(!isExpanded)
    }

    const toggleCommentModal = () => {
        setIsCommentModalOpen(!isCommentModalOpen)
    }

    const toggleDeleteModal = () => {
        setIsDeleteModalOpen(!isDeleteModalOpen)
    }

    const handleSubmitReply = (input) => {
        
        const reply = {
            commentId: comment.id,
            userProfileId: loggedInUser.id,
            content: input
            }
            
        return createReply(reply).then(res => {
            if (res.ok) {
                refreshPage()
                toggleCommentModal()
                setIsExpanded(true)
                return true
            } else {
                return false
            }
        })
    }

    const handleCommentButton = () => {
        setObjectToEdit(null)
        setModalTypeName("Reply")
        toggleCommentModal()
    }
    
    const handleEdit = (input) => {
        setObjectToEdit(input)
        
        if (input.hasOwnProperty("buildId")) {
            setModalTypeName("Comment")
        } else {
            setModalTypeName("Reply")
        }
        
        toggleCommentModal()
    }

    const handleDeleteModal = (object) => {
        if (object.hasOwnProperty("buildId")) {
            setModalTypeName("Comment")
        } else if (object.hasOwnProperty("commentId")) {
            setModalTypeName("Reply")
        }
        
        setObjectToDelete(object)
        toggleDeleteModal()
    }

    const handleDelete = () => {
        let deleteFunction
        
        if (objectToDelete.hasOwnProperty("buildId")) {
            deleteFunction = deleteComment
        } else if (objectToDelete.hasOwnProperty("commentId")) {
            deleteFunction = deleteReply
        }

        deleteFunction(objectToDelete.id).then(() => {
            toggleDeleteModal()
            refreshPage()
        })
    }

    return (
        <>
            <Card sx={{pt: 1}}>
                <CardContent>
                    <Box sx={{display: "flex", flexDirection: "row", gap: 1}}>
                        <Avatar 
                            src={comment.userProfile.imageLocation}
                        />
                        <Box sx={{flexGrow: 1, pr: 1}}>
                            <Box sx={{display: "flex", flexDirection: "row", gap: 1, flexGrow: 1}}>
                                <Typography fontWeight={"bold"}>{comment.userProfile.userName}</Typography>
                                <Typography sx={{fontStyle: "italic", flexGrow: 1, textAlign: {xs: "end", sm: "start"}}}>{comment.formattedDateCreated}</Typography>
                            </Box>
                            <Typography>{comment.content}</Typography>
                        </Box>
                        {comment.userProfileId == loggedInUser.id && (
                            <Box sx={{display: {xs: "none", sm: "flex"}, flexGrow: 1, justifyContent: "end", alignItems: "start"}}>
                                <EditRemoveMenu editHandler={() => handleEdit(comment)} deleteHandler={() => handleDeleteModal(comment)} />
                            </Box>
                        )}
                    </Box>
                    <CardActions sx={{pb: 0, mb: 0}}>
                        <IconButton><AddReaction fontSize="small"/></IconButton>
                        <IconButton onClick={() => handleCommentButton()}><CommentIcon fontSize="small"/></IconButton>
                        {comment.userProfileId == loggedInUser.id && (
                            <Box sx={{display: {xs: "flex", sm: "none"}, flexGrow: 1, justifyContent: "end"}}>
                                <EditRemoveMenu editHandler={() => handleEdit(comment)} deleteHandler={() => handleDeleteModal(comment)} />
                            </Box>
                        )}
                    </CardActions>
                    {comment.replies.length > 0 && (
                    <>
                        <CardActions sx={{pb: 0, mb: 0}}>
                            <Button onClick={() => toggleExpand()}
                                startIcon={isExpanded ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                            >
                                {`${comment.replies.length} ${comment.replies.length > 1 ? "Replies" : "Reply"}`}
                            </Button>
                        </CardActions>
                        <Collapse in={isExpanded} timeout={"auto"}>
                            <List sx={{width: 1, pb: 0, px: 0}}>
                                {comment.replies.map(r => (
                                    <Box key={`reply-${r.id}`}>
                                        <Divider variant="inset" sx={{pt: 1}}/>
                                        <ListItem alignItems="flex-start" sx={{pb: 0, pr: 0, pt: 2}}>
                                            <ListItemAvatar>
                                                <Avatar src={r.userProfile.imageLocation}/>
                                            </ListItemAvatar>
                                            <ListItemText>
                                                <Box sx={{display: "flex", flexDirection: "row", gap: 1, flexGrow: 1, pr: 1}}>
                                                    <Typography sx={{fontWeight: "bold"}} noWrap>{r.userProfile.userName}</Typography>
                                                    <Typography sx={{fontStyle: "italic", flexGrow: 1, textAlign: {xs: "end", sm: "start"}}} noWrap>{r.formattedDateCreated}</Typography>
                                                    {r.userProfileId == loggedInUser.id && (
                                                        <Box display={{xs: "none", sm: "inline-block"}}>
                                                            <EditRemoveMenu editHandler={() => handleEdit(r)} deleteHandler={() => handleDeleteModal(r)} />
                                                        </Box>
                                                    )}
                                                </Box>
                                                <Typography>{r.content}</Typography>
                                                <Box sx={{display: "flex", flexDirection: "row", flexGrow: 1, gap: 2}}>
                                                    <IconButton><AddReaction fontSize="small"/></IconButton>
                                                    <IconButton onClick={() => handleCommentButton()}><CommentIcon fontSize="small"/></IconButton>
                                                    {r.userProfileId == loggedInUser.id && (
                                                        <Box sx={{display: {xs: "flex", sm: "none"}, flexGrow: 1, justifyContent: "end"}}>
                                                            <EditRemoveMenu editHandler={() => handleEdit(r)} deleteHandler={() => handleDeleteModal(r)} />
                                                        </Box>
                                                    )}
                                                </Box>
                                            </ListItemText>
                                        </ListItem>
                                    </Box>
                                ))}
                            </List>
                        </Collapse>
                    </>
                    )}
                    
                </CardContent>
            </Card>
            <CommentModal isOpen={isCommentModalOpen} toggle={toggleCommentModal} submit={handleSubmitReply} typeName={modalTypeName} importForEdit={objectToEdit} />
            <DeleteModal isOpen={isDeleteModalOpen} toggle={toggleDeleteModal} confirmDelete={handleDelete} typeName={modalTypeName} />
        </>
    )
}

export default Comment