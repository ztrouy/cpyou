import { Alert, Box, Button, CircularProgress, Container, FormControl, FormHelperText, InputLabel, MenuItem, Paper, Select, Snackbar, Stack, TextField, Typography } from "@mui/material"
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

    const [cpuError, setCPUError] = useState("")
    const [gpuError, setGPUError] = useState("")
    const [psuError, setPSUError] = useState("")
    const [coolerError, setCoolerError] = useState("")
    const [memoryError, setMemoryError] = useState("")
    const [storageError, setStorageError] = useState("")
    const [motherboardError, setMotherboardError] = useState("")

    const [snackbarState, setSnackbarState] = useState({})
    
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

    const initialSnackbar = {
        open: false,
        vertical: "top",
        horizontal: "center",
        message: ""
    }
    
    useEffect(() => {
        getCPUs().then(setCPUs)
        getGPUs().then(setGPUs)
        getPSUs().then(setPSUs)
        getCoolers().then(setCoolers)
        getMemory().then(setMemory)
        getStorage().then(setStorage)
        getMotherboards().then(setMotherboards)

        setSnackbarState(initialSnackbar)
        
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
        setMemoryError("")
        const copy = [...build.buildMemories]
        
        const foundMemory = memory.find(m => m.id == e)

        const selection = {
            id: parseInt(e),
            name: foundMemory.name,
            interfaceId: foundMemory.interfaceId,
            price: foundMemory.price,
            sizeGB: foundMemory.sizeGB,
            fullName: foundMemory.fullName,
            quantity: 1
        }
        
        copy.push(selection)

        setBuild({...build, buildMemories: copy})
    }

    const handleStorageSelection = (e) => {
        setStorageError("")
        const copy = [...build.buildStorages]
        
        const foundStorage = storage.find(s => s.id == e)

        const selection = {
            id: parseInt(e),
            name: foundStorage.name,
            interfaceId: foundStorage.interfaceId,
            price: foundStorage.price,
            sizeGB: foundStorage.sizeGB,
            fullName: foundStorage.fullName,
            quantity: 1
        }
        
        copy.push(selection)

        setBuild({...build, buildStorages: copy})
    }

    const handleChangeMemoryQuantity = (chosenComponent, quantity) => {
        setMemoryError("")
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
        setStorageError("")
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

    const handleOpenSnackbar = (messageText) => {
        setSnackbarState({...snackbarState, open: true, message: messageText})
    }

    const handleCloseSnackbar = () => {
        setSnackbarState({...snackbarState, open: false})
    }

    const handleErrors = (res) => {
        if (!res.errors) {
            handleOpenSnackbar(res)
            return
        }

        setCPUError(res.errors.cpu.join(", "))
        setGPUError(res.errors.gpu.join(", "))
        setPSUError(res.errors.psu.join(", "))
        setMotherboardError(res.errors.motherboard.join(", "))
        setCoolerError(res.errors.cooler.join(", "))
        setMemoryError(res.errors.memory.join(", "))
        setStorageError(res.errors.storage.join(", "))
        handleOpenSnackbar("Issues found with build")
    }
    
    const handleSubmit = (e) => {
        e.preventDefault()

        setCPUError("")
        setGPUError("")
        setPSUError("")
        setCoolerError("")
        setMemoryError("")
        setStorageError("")
        setMotherboardError("")

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
            createBuild(copy).then(res => {
                if (res.status === 201) {
                    const url = res.headers.get("Location")
                    navigate(url)
                } else {
                    res.json().then(errors => handleErrors(errors))
                }
            })
        } else {
            updateBuild(copy).then(res => {
                if (res.status === 204) {
                    navigate(`/builds/${buildId}`)
                } else {
                    res.json().then(errors => handleErrors(errors))
                }
            })
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
                    <FormControl required error={Boolean(cpuError)}>
                        <InputLabel>Processor</InputLabel>
                        <Select
                            label="Processor"
                            value={build.cpuId}
                            onChange={e => {
                                setBuild({...build, cpuId: e.target.value})
                                setCPUError("")
                            }}
                        >
                            <MenuItem value={0} key={"cpu-0"} disabled hidden>Choose a CPU</MenuItem>
                            {cpus.map(c => (
                                <MenuItem value={c.id} key={`cpu-${c.id}`}>{c.name}</MenuItem>
                            ))}
                        </Select>
                        {cpuError && <FormHelperText>{cpuError}</FormHelperText>}
                    </FormControl>
                    <FormControl required error={Boolean(gpuError)}>
                        <InputLabel>Graphics Card</InputLabel>
                        <Select
                            label="Graphics Card"
                            value={build.gpuId}
                            onChange={e => {
                                setBuild({...build, gpuId: e.target.value})
                                setGPUError("")
                            }}
                        >
                            <MenuItem value={0} key={"gpu-0"} disabled hidden>Choose a GPU</MenuItem>
                            {gpus.map(g => (
                                <MenuItem value={g.id} key={`gpu-${g.id}`}>{g.name}</MenuItem>
                            ))}
                        </Select>
                        {gpuError && <FormHelperText>{gpuError}</FormHelperText>}
                    </FormControl>
                    <FormControl required error={Boolean(motherboardError)}>
                        <InputLabel>Motherboard</InputLabel>
                        <Select
                            label="Motherboard"
                            value={build.motherboardId}
                            onChange={e => {
                                setBuild({...build, motherboardId: e.target.value})
                                setMotherboardError("")
                            }}
                        >
                            <MenuItem value={0} key={"motherboard-0"} disabled hidden>Choose a Motherboard</MenuItem>
                            {motherboards.map(m => (
                                <MenuItem value={m.id} key={`motherboard-${m.id}`}>{m.name}</MenuItem>
                            ))}
                        </Select>
                        {motherboardError && <FormHelperText>{motherboardError}</FormHelperText>}
                    </FormControl>
                    <FormControl required error={Boolean(psuError)}>
                        <InputLabel>Power Supply</InputLabel>
                        <Select
                            label="Power Supply"
                            value={build.psuId}
                            onChange={e => {
                                setBuild({...build, psuId: e.target.value})
                                setPSUError("")
                            }}
                        >
                            <MenuItem value={0} key={"psu-0"} disabled hidden>Choose a PSU</MenuItem>
                            {psus.map(p => (
                                <MenuItem value={p.id} key={`psu-${p.id}`}>{p.name}</MenuItem>
                            ))}
                        </Select>
                        {psuError && <FormHelperText>{psuError}</FormHelperText>}
                    </FormControl>
                    <FormControl required error={Boolean(coolerError)}>
                        <InputLabel>CPU Cooler</InputLabel>
                        <Select
                            label="CPU Cooler"
                            value={build.coolerId}
                            onChange={e => {
                                setBuild({...build, coolerId: e.target.value})
                                setCoolerError("")
                            }}
                        >
                            <MenuItem value={0} key={"cooler-0"} disabled hidden>Choose a CPU Cooler</MenuItem>
                            {coolers.map(c => (
                                <MenuItem value={c.id} key={`cooler-${c.id}`}>{c.name}</MenuItem>
                            ))}
                        </Select>
                        {coolerError && <FormHelperText>{coolerError}</FormHelperText>}
                    </FormControl>
                    <Stack direction={"row"} spacing={1}>
                        <FormControl sx={{width: 1}}  required error={Boolean(memoryError)}>
                            <InputLabel>Memory</InputLabel>
                            <Select label="Memory" value={0} onChange={e => handleMemorySelection(e.target.value)}>
                                <MenuItem value={0} key={"memory-0"} disabled hidden>Choose a Memory Module</MenuItem>
                                {build.buildMemories.length == 0 ? (
                                    memory.map(m => <MenuItem value={m.id} key={`memory-${m.id}`}>{m.fullName}</MenuItem>)
                                ) : (
                                    memory.filter(m => m.id != build.buildMemories.find(bm => bm.id == m.id)?.id).map(m => (
                                        <MenuItem value={m.id} key={`memory-${m.id}`}>{m.fullName}</MenuItem>
                                    ))
                                )}
                            </Select>
                            {memoryError && <FormHelperText>{memoryError}</FormHelperText>}
                        </FormControl>
                        <FormControl sx={{width: 1}} required error={Boolean(storageError)}>
                            <InputLabel>Storage</InputLabel>
                            <Select label="Storage" value={0} onChange={e => handleStorageSelection(e.target.value)}>
                                <MenuItem value={0} key={"storage-0"} disabled hidden>Choose a Storage Device</MenuItem>
                                {build.buildMemories.length == 0 ? (
                                    storage.map(s => <MenuItem value={s.id} key={`storage-${s.id}`}>{s.fullName}</MenuItem>)
                                ) : (
                                    storage.filter(s => s.id != build.buildStorages.find(bs => bs.id == s.id)?.id).map(s => (
                                        <MenuItem value={s.id} key={`storage-${s.id}`}>{s.fullName}</MenuItem>
                                    ))
                                )}
                            </Select>
                            {storageError && <FormHelperText>{storageError}</FormHelperText>}
                        </FormControl>
                    </Stack>
                    {build.buildMemories.map(bm => (
                        <Stack direction={"row"} alignItems={"center"} spacing={1} key={`buildMemory-${bm.id}`}>
                            <Box display={"flex"} alignItems={"center"} flexGrow={1}>
                                <Typography>{bm.fullName}</Typography>
                            </Box>
                                <TextField 
                                    type="number"
                                    size="small"
                                    value={bm.quantity}
                                    sx={{width: "60px", minWidth: "60px"}}
                                    onChange={e => handleChangeMemoryQuantity(bm, e.target.value)}
                                />
                                <Box width={"80px"} minWidth={"80px"}>
                                    <Typography textAlign={"end"}>{(bm.price * bm.quantity).toLocaleString("en-US", {style:"currency", currency:"USD"})}</Typography>
                                </Box>
                        </Stack>
                    ))}
                    {build.buildStorages.map(bs => (
                        <Stack direction={"row"} alignItems={"center"} spacing={1} key={`buildStorage-${bs.id}`}>
                            <Box display={"flex"} alignItems={"center"} flexGrow={1}>
                                <Typography>{bs.fullName}</Typography>
                            </Box>
                                <TextField 
                                    type="number"
                                    size="small"
                                    value={bs.quantity}
                                    sx={{width: "60px", minWidth: "60px"}}
                                    onChange={e => handleChangeStorageQuantity(bs, e.target.value)}
                                />
                                <Box width={"80px"} minWidth={"80px"}>
                                    <Typography textAlign={"end"} width={"80px"}>{(bs.price * bs.quantity).toLocaleString("en-US", {style:"currency", currency:"USD"})}</Typography>
                                </Box>
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
            <Snackbar open={snackbarState.open} autoHideDuration={4000} onClose={handleCloseSnackbar}>
                <Alert
                    severity="error"
                    variant="filled"
                    sx={{width: 1}}
                >
                    {snackbarState.message}
                </Alert>
            </Snackbar>
        </Container>
    )
}

export default BuildForm