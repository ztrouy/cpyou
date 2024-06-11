import { Box, Button, Chip, CircularProgress, Container, Paper, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { deleteBuild, getSingleBuild } from "../../managers/buildManager.js"
import DeleteModal from "../modals/DeleteModal.jsx"

export const BuildDetails = ({ loggedInUser }) => {
    const [build, setBuild] = useState(null)
    const [isModalOpen, setIsModalOpen] = useState(false)

    const { buildId } = useParams()

    const navigate = useNavigate()

    useEffect(() => {
        getSingleBuild(buildId).then(setBuild)
    }, [buildId])

    const toggleDeleteModal = () => {
        setIsModalOpen(!isModalOpen)
    }

    const handleDelete = () => {
        deleteBuild(buildId).then(() => {
            navigate("/builds")
        })
    }
    
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
            <DeleteModal 
                isOpen={isModalOpen}
                toggle={toggleDeleteModal}
                confirmDelete={handleDelete}
                typeName={"Build"}
            />
        </Container>
    )
}

export default BuildDetails