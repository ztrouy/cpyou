import { Badge, Box, Chip, CircularProgress, Container, Paper, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import { getBuilds } from "../../managers/buildManager.js"

export const BuildsList = ({ loggedInUser }) => {
    const [builds, setBuilds] = useState(null)
    
    useEffect(() => {
        getBuilds().then(setBuilds)
    }, [])

    if (!builds) {
        return <CircularProgress/>
    }

    return (
        <Container sx={{display: "flex", justifyContent: "center", mb: 5}}>
            <Box sx={{width: 1, maxWidth: "800px"}}>
                <Typography variant="h3" sx={{my: 2}}>Builds</Typography>
                <Box sx={{display: "flex", flexDirection: "column", gap: 1}}>
                    {builds.map(b => (
                        <Paper sx={{p: 2}} elevation={3} key={`b-${b.id}`}>
                            <Box sx={{display: "flex", gap: 1}}>
                                <Typography variant="h5">{b.name}</Typography>
                                <Chip 
                                    label={`$${b.totalPrice}`}
                                />
                            </Box>
                            <Typography>{b.userProfile.userName}</Typography>
                            <Typography sx={{fontStyle: "italic"}}>{`${b.componentsQuantity} components`}</Typography>
                        </Paper>
                    ))}
                </Box>
            </Box>
        </Container>
    )
}

export default BuildsList