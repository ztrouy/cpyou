import { Box, ButtonBase, Chip, CircularProgress, Container, Paper, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import { getBuilds } from "../../managers/buildManager.js"
import { useNavigate } from "react-router-dom"

export const BuildsList = () => {
    const [builds, setBuilds] = useState(null)
    
    useEffect(() => {
        getBuilds().then(setBuilds)
    }, [])

    const navigate = useNavigate()

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
                                <ButtonBase onClick={() => navigate(`${b.id}`)}>
                                    <Typography variant="h5">{b.name}</Typography>
                                </ButtonBase>
                                <Chip 
                                    label={b.totalPrice.toLocaleString("en-US", {style:"currency", currency:"USD"})}
                                />
                                <Box flexGrow={1}>
                                    <Typography textAlign={"end"}>{b.formattedDateCreated}</Typography>
                                </Box>
                            </Box>
                            <Typography>{b.userProfile.userName}</Typography>
                            <Typography sx={{fontStyle: "italic"}}>{`${b.wattage}W`}</Typography>
                            <Typography sx={{fontStyle: "italic"}}>{`${b.commentsQuantity} ${b.commentsQuantity == 1 ? "comment" : "comments"}`}</Typography>
                        </Paper>
                    ))}
                </Box>
            </Box>
        </Container>
    )
}

export default BuildsList