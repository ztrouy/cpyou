import { Box, Button, Dialog, DialogActions, DialogTitle, TextField } from "@mui/material"
import { useState } from "react"

export const CommentModal = ({ isOpen, toggle, submit, typeName}) => {
    const [input, setInput] = useState("")
    
    const handleSubmit = () => {
        submit(input)
        setInput("")
    }

    return (
        <Dialog open={isOpen} onClose={toggle} fullWidth>
            <DialogTitle>Create {typeName}</DialogTitle>
            <Box sx={{px: 2}}>
                <TextField 
                    type="text"
                    placeholder="Write comment here..."
                    autoFocus
                    multiline
                    value={input}
                    onChange={e => setInput(e.target.value)}
                    fullWidth
                />

            </Box>
            <DialogActions>
                <Button onClick={toggle}>Cancel</Button>
                <Button onClick={handleSubmit}>Submit</Button>
            </DialogActions>
        </Dialog>
    )
}

export default CommentModal