import { Box, Button, Dialog, DialogActions, DialogTitle, TextField } from "@mui/material"
import { useState } from "react"

export const CommentModal = ({ isOpen, toggle, submit, typeName }) => {
    const [input, setInput] = useState("")
    const [failedSubmit, setFailedSubmit] = useState(false)
    
    const handleSubmit = () => {
        submit(input).then(success => {
            if (success) {
                setFailedSubmit(false)
                setTimeout(() => {
                    setInput("")
                }, 200);
            } else {
                setFailedSubmit(true)
            }
        })
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
                    fullWidth
                    value={input}
                    onChange={e => setInput(e.target.value)}
                    error={failedSubmit}
                    helperText={failedSubmit ? "Something went wrong, please try again" : ""}
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