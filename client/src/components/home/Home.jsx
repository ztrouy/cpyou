import { Box, CircularProgress, Container, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import CommentForHome from "../comments/CommentForHome"
import { getCommentsForHome } from "../../managers/commentManager"
import useAuthorizationProvider from "../../shared/hooks/authorization/useAuthorizationProvider"

export const Home = () => {
    const [comments, setComments] = useState(null)
    const { loggedInUser } = useAuthorizationProvider()
    
    useEffect(() => {
        refreshPage()
    }, [loggedInUser])

    const refreshPage = () => {
        getCommentsForHome(loggedInUser.id).then(setComments)
    }

    if (comments == null) {
        return <CircularProgress/>
    }

    return (
        <Container sx={{display: "flex", flexDirection: "column", justifyContent: "center", py: 3}}>
            <Box sx={{width: 1, maxWidth: "800px", alignSelf: "center"}}>
                <Typography variant="h3" marginBottom={2}>Notifications</Typography>
                <Box sx={{display: "flex", flexDirection: "column", gap: 2}}>
                    {comments.map(c => (
                        <CommentForHome comment={c} refreshPage={refreshPage} key={`comment-${c.id}`} />
                    ))}
                    {comments.length == 0 && (
                        <Typography fontStyle={"italic"} textAlign={"center"}>No Notifications</Typography>
                    )}
                </Box>
            </Box>
        </Container>
    )
}

export default Home