import { Box, Button, Container, FormControl, InputLabel, MenuItem, Paper, Select, TextField, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import { getComponents } from "../../managers/componentManager.js"
import { useNavigate } from "react-router-dom"
import { createBuild } from "../../managers/buildManager.js"

export const BuildForm = ({ loggedInUser }) => {
    const [name, setName] = useState("")
    const [content, setContent] = useState("")
    const [components, setComponents] = useState([])
    const [chosenComponents, setChosenComponents] = useState([])

    const navigate = useNavigate()
    
    useEffect(() => {
        getComponents().then(setComponents)
    }, [])
    
    const handleSelection = (e) => {
        const copy = [...chosenComponents]
        
        const selection = {
            id: parseInt(e),
            name: components.find(c => c.id == e).name,
            price: components.find(c => c.id == e).price,
            quantity: 1
        }
        
        copy.push(selection)

        setChosenComponents(copy)
    }

    const handleChangeQuantity = (chosenComponent, quantity) => {
        const copy = [...chosenComponents]
        let componentCopy = copy.find(cc => cc == chosenComponent)
        
        if (quantity == 0) {
            const index = copy.indexOf(componentCopy)
            copy.splice(index, 1)
        } else {
            componentCopy.quantity = quantity
        }

        setChosenComponents(copy)
    }
    
    const handleSubmit = (e) => {
        e.preventDefault()
        
        const build = {
            userProfileId: loggedInUser.id,
            name: name,
            content: content,
            components: chosenComponents.map(cc => {
                return {
                    componentId: cc.id,
                    quantity: cc.quantity
                }
            })
        }

        createBuild(build).then(url => navigate(url))
    }

    return (
        <Container>
            <Paper elevation={5} sx={{p: 2, mt: 5}}>
                <Box
                    component={"form"} 
                    onSubmit={handleSubmit} 
                    sx={{display: "flex", flexDirection: "column", gap: 2}}
                    autoComplete="off"
                >
                    <Typography variant="h4">New Build</Typography>
                    <TextField 
                        label="Build Name"
                        type="text"
                        required
                        value={name}
                        onChange={e => setName(e.target.value)}
                    />
                    <TextField 
                        label="Description"
                        type="text"
                        required
                        value={content}
                        onChange={e => setContent(e.target.value)}
                    />
                    <FormControl>
                        <InputLabel>Component</InputLabel>
                        <Select
                            label="Component"
                            value={0}
                            onChange={e => handleSelection(e.target.value)}
                        >
                            <MenuItem value={0} key={"c-0"} disabled hidden>Choose a Component</MenuItem>
                            {chosenComponents.length == 0 ? (
                                components.map(c => <MenuItem value={c.id} key={`c-${c.id}`}>{c.name}</MenuItem>)
                            ) : (
                                components.filter(c => c.id != chosenComponents.find(cc => cc.id == c.id)?.id)
                                    .map(c => <MenuItem value={c.id} key={`c-${c.id}`}>{c.name}</MenuItem>))
                            }
                        </Select>
                    </FormControl>
                    {chosenComponents.map(cc => (
                        <Box sx={{display: "flex", flexDirection: "row"}} key={`cc-${cc.id}`}>
                            <Box sx={{display: "flex", flexDirection: "row", alignItems: "center", flexGrow: 1}}>
                                <Typography>{cc.name}</Typography>
                            </Box>
                            <Box sx={{display: "flex", alignItems: "center", gap: 2}}>
                                <Typography>{(cc.price * cc.quantity).toLocaleString("en-US", {style:"currency", currency:"USD"})}</Typography>
                                <TextField 
                                    type="number"
                                    size="small"
                                    sx={{width: "60px"}}
                                    value={cc.quantity}
                                    onChange={e => handleChangeQuantity(cc, parseInt(e.target.value))}
                                />
                            </Box>
                        </Box>
                    ))}
                    <Typography fontWeight={"bold"} alignSelf={"end"}>
                        {`Total: ${chosenComponents.reduce((n, {price, quantity}) => n + (price * quantity), 0).toLocaleString("en-US", {style:"currency", currency:"USD"})}`}
                    </Typography>
                    <Box sx={{display: "flex", justifyContent: "end"}}>
                        <Button variant="contained" type="submit">Submit</Button>
                    </Box>
                </Box>
            </Paper>
        </Container>
    )
}

export default BuildForm