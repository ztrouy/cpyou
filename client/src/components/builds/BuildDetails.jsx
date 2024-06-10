import { Box, Button, Chip, CircularProgress, Container, Paper, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import { useParams } from "react-router-dom"
import { getSingleBuild } from "../../managers/buildManager.js"

export const BuildDetails = ({ loggedInUser }) => {
    const [build, setBuild] = useState(null)

    const { buildId } = useParams()

    useEffect(() => {
        getSingleBuild(buildId).then(setBuild)
    }, [buildId])
    
    if (!build) {
        return <CircularProgress/>
    }

    return (
        <Container>
            <Paper sx={{p: 2, mt: 5}}>
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
                            <Typography>{`$${c.price * c.quantity}`}</Typography>
                        </Box>
                    ))}
                    <Typography textAlign={"end"} fontWeight={"bold"}>{`$${build.totalPrice}`}</Typography>
                </Box>
                <Box sx={{display: "flex", justifyContent: "end", mt: 1, gap: 1}}>
                    <Button variant="contained">Edit</Button>
                    <Button variant="contained">Delete</Button>
                </Box>
            </Paper>
        </Container>
    )
}

export default BuildDetails