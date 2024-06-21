import { Box, Button, CircularProgress, Container, FormControl, InputLabel, MenuItem, Paper, Select, Stack, TextField, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { createBuild, getSingleBuildForEdit, updateBuild } from "../../managers/buildManager.js"
import useAuthorizationProvider from "../../shared/hooks/authorization/useAuthorizationProvider.js"
import { getCPUs } from "../../managers/cpuManager.js"
import { getGPUs } from "../../managers/gpuManager.js"
import { getPSUs } from "../../managers/psuManager.js"
import { getCoolers } from "../../managers/coolerManager.js"
import { getMemory } from "../../managers/memoryManager.js"
import { getStorage } from "../../managers/storageManager.js"
import { getMotherboards } from "../../managers/motherboardManager.js"

export const BuildForm = () => {
    const [cpus, setCPUs] = useState([])
    const [gpus, setGPUs] = useState([])
    const [psus, setPSUs] = useState([])
    const [coolers, setCoolers] = useState([])
    const [memory, setMemory] = useState([])
    const [storage, setStorage] = useState([])
    const [motherboards, setMotherboards] = useState([])
    const [build, setBuild] = useState(null)
    
    const { loggedInUser } = useAuthorizationProvider()
    const { buildId } = useParams()
    const navigate = useNavigate()

    const initialBuild = {
        userProfileId: loggedInUser.id,
        name: "",
        content: "",
        cpuId: 0,
        gpuId: 0,
        psuId: 0,
        coolerId: 0,
        motherboardId: 0,
        buildMemories: [],
        buildStorages: []
    }
    
    useEffect(() => {
        getCPUs().then(setCPUs)
        getGPUs().then(setGPUs)
        getPSUs().then(setPSUs)
        getCoolers().then(setCoolers)
        getMemory().then(setMemory)
        getStorage().then(setStorage)
        getMotherboards().then(setMotherboards)
        
        if (buildId) {
            getSingleBuildForEdit(buildId).then(buildObj => {
                if (buildObj.userProfileId != loggedInUser.id) {
                    navigate(`/builds/${buildId}`)
                    return
                }

                setBuild(buildObj)
            })
        } else {
            setBuild(initialBuild)
        }
    }, [buildId])
    
    const handleMemorySelection = (e) => {
        const copy = [...build.buildMemories]
        
        const foundMemory = memory.find(m => m.id == e)

        const selection = {
            id: parseInt(e),
            name: foundMemory.name,
            interfaceId: foundMemory.interfaceId,
            price: foundMemory.price,
            sizeGB: foundMemory.sizeGB,
            quantity: 1
        }
        
        copy.push(selection)

        setBuild({...build, buildMemories: copy})
    }

    const handleStorageSelection = (e) => {
        const copy = [...build.buildStorages]
        
        const foundStorage = storage.find(s => s.id == e)

        const selection = {
            id: parseInt(e),
            name: foundStorage.name,
            interfaceId: foundStorage.interfaceId,
            price: foundStorage.price,
            sizeGB: foundStorage.sizeGB,
            quantity: 1
        }
        
        copy.push(selection)

        setBuild({...build, buildStorages: copy})
    }

    const handleChangeMemoryQuantity = (chosenComponent, quantity) => {
        const copy = [...build.buildMemories]
        let memoryCopy = copy.find(mc => mc == chosenComponent)
        
        if (quantity <= 0) {
            const index = copy.indexOf(memoryCopy)
            copy.splice(index, 1)
        } else {
            memoryCopy.quantity = quantity
        }

        setBuild({...build, buildMemories: copy})
    }

    const handleChangeStorageQuantity = (chosenComponent, quantity) => {
        const copy = [...build.buildStorages]
        let storageCopy = copy.find(sc => sc == chosenComponent)
        
        if (quantity <= 0) {
            const index = copy.indexOf(storageCopy)
            copy.splice(index, 1)
        } else {
            storageCopy.quantity = quantity
        }

        setBuild({...build, buildStorages: copy})
    }
    
    const handleSubmit = (e) => {
        e.preventDefault()

        const copy = {...build}
        copy.buildMemories = copy.buildMemories.map(bm => ({
            memoryId: bm.id,
            quantity: bm.quantity
        }))
        copy.buildStorages = copy.buildStorages.map(bs => ({
            storageId: bs.id,
            quantity: bs.quantity
        }))

        if (!buildId) {
            createBuild(copy).then(url => navigate(url))
        } else {
            updateBuild(copy).then(() => navigate(`/builds/${buildId}`))
        }
    }

    const calculateTotal = () => {
        let cpuCost = build.cpuId != 0 ? cpus.find(c => c.id == build.cpuId)?.price : 0
        let gpuCost = build.gpuId != 0 ? gpus.find(c => c.id == build.gpuId)?.price : 0
        let psuCost = build.psuId != 0 ? psus.find(c => c.id == build.psuId)?.price : 0
        let coolerCost = build.coolerId != 0 ? coolers.find(c => c.id == build.coolerId)?.price : 0
        let motherboardCost = build.motherboardId != 0 ? motherboards.find(c => c.id == build.motherboardId)?.price : 0
        let memoryCost = build.buildMemories.reduce((n, {price, quantity}) => n + (price * quantity), 0)
        let storageCost = build.buildStorages.reduce((n, {price, quantity}) => n + (price * quantity), 0)

        let totalCost = (cpuCost + gpuCost + psuCost + coolerCost + motherboardCost + memoryCost + storageCost)

        return totalCost
    }

    const calculateWattage = () => {
        let cpuWattage = build.cpuId != 0 ? cpus.find(c => c.id == build.cpuId)?.tdp : 0
        let gpuWattage = build.gpuId != 0 ? gpus.find(g => g.id == build.gpuId)?.tdp : 0

        let totalWattage = Math.ceil((cpuWattage + gpuWattage) * 1.2)

        return totalWattage
    }

    if (!build || cpus.length == 0 || gpus.length == 0 || psus.length == 0 || 
        coolers.length == 0 || motherboards.length == 0 || memory.length == 0 || 
        storage.length == 0) {
        return <CircularProgress/>
    }

    return (
        <Container>
            <Paper elevation={5} sx={{p: 3, my: 5, width: 1, maxWidth: "800px", mx: "auto"}}>
                <Box
                    component={"form"} 
                    onSubmit={handleSubmit} 
                    sx={{display: "flex", flexDirection: "column", gap: 2}}
                    autoComplete="off"
                >
                    {!buildId ? (
                        <Typography variant="h4">New Build</Typography>
                    ) : (
                        <Typography variant="h4">Edit Build</Typography>
                    )}
                    <Typography variant="h5">Build Details</Typography>
                    <TextField 
                        label="Build Name"
                        type="text"
                        required
                        autoFocus
                        value={build.name}
                        onChange={e => setBuild({...build, name: e.target.value})}
                    />
                    <TextField 
                        label="Description"
                        type="text"
                        multiline
                        required
                        value={build.content}
                        onChange={e => setBuild({...build, content: e.target.value})}
                    />
                    <Typography variant="h5">Components</Typography>
                    <FormControl>
                        <InputLabel>Processor</InputLabel>
                        <Select
                            label="Processor"
                            value={build.cpuId}
                            onChange={e => setBuild({...build, cpuId: e.target.value})}
                        >
                            <MenuItem value={0} key={"cpu-0"} disabled hidden>Choose a CPU</MenuItem>
                            {cpus.map(c => (
                                <MenuItem value={c.id} key={`cpu-${c.id}`}>{c.name}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <FormControl>
                        <InputLabel>Graphics Card</InputLabel>
                        <Select
                            label="Graphics Card"
                            value={build.gpuId}
                            onChange={e => setBuild({...build, gpuId: e.target.value})}
                        >
                            <MenuItem value={0} key={"gpu-0"} disabled hidden>Choose a GPU</MenuItem>
                            {gpus.map(g => (
                                <MenuItem value={g.id} key={`gpu-${g.id}`}>{g.name}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <FormControl>
                        <InputLabel>Motherboard</InputLabel>
                        <Select
                            label="Motherboard"
                            value={build.motherboardId}
                            onChange={e => setBuild({...build, motherboardId: e.target.value})}
                        >
                            <MenuItem value={0} key={"motherboard-0"} disabled hidden>Choose a Motherboard</MenuItem>
                            {motherboards.map(m => (
                                <MenuItem value={m.id} key={`motherboard-${m.id}`}>{m.name}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <FormControl>
                        <InputLabel>Power Supply</InputLabel>
                        <Select
                            label="Power Supply"
                            value={build.psuId}
                            onChange={e => setBuild({...build, psuId: e.target.value})}
                        >
                            <MenuItem value={0} key={"psu-0"} disabled hidden>Choose a PSU</MenuItem>
                            {psus.map(p => (
                                <MenuItem value={p.id} key={`psu-${p.id}`}>{p.name}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <FormControl>
                        <InputLabel>CPU Cooler</InputLabel>
                        <Select
                            label="CPU Cooler"
                            value={build.coolerId}
                            onChange={e => setBuild({...build, coolerId: e.target.value})}
                        >
                            <MenuItem value={0} key={"cooler-0"} disabled hidden>Choose a CPU Cooler</MenuItem>
                            {coolers.map(c => (
                                <MenuItem value={c.id} key={`cooler-${c.id}`}>{c.name}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <Stack direction={"row"} spacing={1}>
                        <FormControl sx={{width: 1}}>
                            <InputLabel>Memory</InputLabel>
                            <Select label="Memory" value={0} onChange={e => handleMemorySelection(e.target.value)}>
                                <MenuItem value={0} key={"memory-0"} disabled hidden>Choose a Memory Module</MenuItem>
                                {build.buildMemories.length == 0 ? (
                                    memory.map(m => <MenuItem value={m.id} key={`memory-${m.id}`}>{m.name} {m.sizeGB}GB</MenuItem>)
                                ) : (
                                    memory.filter(m => m.id != build.buildMemories.find(bm => bm.id == m.id)?.id).map(m => (
                                        <MenuItem value={m.id} key={`memory-${m.id}`}>{m.name} {m.sizeGB}GB</MenuItem>
                                    ))
                                )}
                            </Select>
                        </FormControl>
                        <FormControl sx={{width: 1}}>
                            <InputLabel>Storage</InputLabel>
                            <Select label="Storage" value={0} onChange={e => handleStorageSelection(e.target.value)}>
                                <MenuItem value={0} key={"storage-0"} disabled hidden>Choose a Storage Device</MenuItem>
                                {build.buildMemories.length == 0 ? (
                                    storage.map(s => <MenuItem value={s.id} key={`storage-${s.id}`}>{s.name} {s.sizeGB}GB</MenuItem>)
                                ) : (
                                    storage.filter(s => s.id != build.buildStorages.find(bs => bs.id == s.id)?.id).map(s => (
                                        <MenuItem value={s.id} key={`storage-${s.id}`}>{s.name} {s.sizeGB}GB</MenuItem>
                                    ))
                                )}
                            </Select>
                        </FormControl>
                    </Stack>
                    {build.buildMemories.map(bm => (
                        <Stack direction={"row"} key={`buildMemory-${bm.id}`}>
                            <Box display={"flex"} alignItems={"center"} flexGrow={1}>
                                <Typography>{bm.name} {bm.sizeGB}GB</Typography>
                            </Box>
                            <Stack direction={"row"} alignItems={"center"} spacing={2}>
                                <TextField 
                                    type="number"
                                    size="small"
                                    value={bm.quantity}
                                    sx={{width: "60px"}}
                                    onChange={e => handleChangeMemoryQuantity(bm, e.target.value)}
                                />
                                <Typography>{(bm.price * bm.quantity).toLocaleString("en-US", {style:"currency", currency:"USD"})}</Typography>
                            </Stack>
                        </Stack>
                    ))}
                    {build.buildStorages.map(bs => (
                        <Stack direction={"row"} key={`buildStorage-${bs.id}`}>
                            <Box display={"flex"} alignItems={"center"} flexGrow={1}>
                                <Typography>{bs.name} {bs.sizeGB}GB</Typography>
                            </Box>
                            <Stack direction={"row"} alignItems={"center"} spacing={2}>
                                <TextField 
                                    type="number"
                                    size="small"
                                    value={bs.quantity}
                                    sx={{width: "60px"}}
                                    onChange={e => handleChangeStorageQuantity(bs, e.target.value)}
                                />
                                <Typography>{(bs.price * bs.quantity).toLocaleString("en-US", {style:"currency", currency:"USD"})}</Typography>
                            </Stack>
                        </Stack>
                    ))}
                    <Stack spacing={4} marginTop={1}>
                        {build.cpuId != 0 && (
                            <Stack direction={"row"}>
                                <Stack flexGrow={1}>
                                    <Typography>{cpus.find(c => c.id == build.cpuId)?.name}</Typography>
                                </Stack>
                                <Typography>${cpus.find(c => c.id == build.cpuId)?.price}</Typography>
                            </Stack>
                        )}
                        {build.gpuId != 0 && (
                            <Stack direction={"row"}>
                                <Stack flexGrow={1}>
                                    <Typography>{gpus.find(c => c.id == build.gpuId)?.name}</Typography>
                                </Stack>
                                <Typography>${gpus.find(c => c.id == build.gpuId)?.price}</Typography>
                            </Stack>
                        )}
                        {build.psuId != 0 && (
                            <Stack direction={"row"}>
                                <Stack flexGrow={1}>
                                    <Typography>{psus.find(c => c.id == build.psuId)?.name}</Typography>
                                </Stack>
                                <Typography>${psus.find(c => c.id == build.psuId)?.price}</Typography>
                            </Stack>
                        )}
                        {build.coolerId != 0 && (
                            <Stack direction={"row"}>
                                <Stack flexGrow={1}>
                                    <Typography>{coolers.find(c => c.id == build.coolerId)?.name}</Typography>
                                </Stack>
                                <Typography>${coolers.find(c => c.id == build.coolerId)?.price}</Typography>
                            </Stack>
                        )}
                        {build.motherboardId != 0 && (
                            <Stack direction={"row"}>
                                <Stack flexGrow={1}>
                                    <Typography>{motherboards.find(c => c.id == build.motherboardId)?.name}</Typography>
                                </Stack>
                                <Typography>${motherboards.find(c => c.id == build.motherboardId)?.price}</Typography>
                            </Stack>
                        )}
                    </Stack>
                    <Stack direction={"row"} spacing={2} justifyContent={"end"}>
                        <Typography fontWeight={"bold"}>{`${calculateWattage()}W`}</Typography>
                        <Typography fontWeight={"bold"}>
                            {`Total: ${calculateTotal().toLocaleString("en-US", {style:"currency", currency:"USD"})}`}
                        </Typography>
                    </Stack>
                    <Box sx={{display: "flex", justifyContent: "end", gap: 1}}>
                        {buildId && <Button variant="contained" onClick={() => navigate(`/builds/${buildId}`)}>Cancel</Button>}
                        <Button variant="contained" type="submit">Submit</Button>
                    </Box>
                </Box>
            </Paper>
        </Container>
    )
}

export default BuildForm