import { Avatar, Box, Button, Card, CardActions, CardContent, Collapse, Divider, IconButton, List, ListItem, ListItemAvatar, ListItemText, Typography } from "@mui/material"
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { useContext, useState } from "react"
import { AuthContext } from "../../App";
import AddReaction from "@mui/icons-material/AddReaction";
import CommentIcon from "@mui/icons-material/Comment";

export const Comment = ({ comment }) => {
    const [isExpanded, setIsExpanded] = useState(false)
    const [loggedInUser] = useContext(AuthContext)

    const toggleExpand = () => {
        setIsExpanded(!isExpanded)
    }

    return (
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
                            <IconButton sx={{p: 0}}>
                                <MoreVertIcon fontSize="small"/>
                            </IconButton>
                        </Box>
                    )}
                </Box>
                <CardActions sx={{pb: 0, mb: 0}}>
                    <IconButton><AddReaction fontSize="small"/></IconButton>
                    <IconButton><CommentIcon fontSize="small"/></IconButton>
                    {comment.userProfileId == loggedInUser.id && (
                        <Box sx={{display: {xs: "flex", sm: "none"}, flexGrow: 1, justifyContent: "end"}}>
                            <IconButton sx={{pr: 0}}>
                                <MoreVertIcon fontSize="small"/>
                            </IconButton>
                        </Box>
                    )}
                </CardActions>
                {comment.replies.length > 0 && (
                <>
                    <CardActions sx={{pb: 0, mb: 0}}>
                        <Button onClick={() => toggleExpand()}>
                            {isExpanded ? (
                                `Hide Replies`
                            ) : (
                                `Show ${comment.replies.length} ${comment.replies.length > 1 ? "Replies" : "Reply"}`
                            )}
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
                                                        <IconButton sx={{p: 0}}>
                                                            <MoreVertIcon fontSize="small"/>
                                                        </IconButton>
                                                    </Box>
                                                )}
                                            </Box>
                                            <Typography>{r.content}</Typography>
                                            <Box sx={{display: "flex", flexDirection: "row", flexGrow: 1, gap: 2}}>
                                                <IconButton><AddReaction fontSize="small"/></IconButton>
                                                <IconButton><CommentIcon fontSize="small"/></IconButton>
                                                {r.userProfileId == loggedInUser.id && (
                                                    <Box sx={{display: {xs: "flex", sm: "none"}, flexGrow: 1, justifyContent: "end"}}>
                                                        <IconButton sx={{pr: 0}}>
                                                            <MoreVertIcon fontSize="small"/>
                                                        </IconButton>
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
    )
}

export default Comment