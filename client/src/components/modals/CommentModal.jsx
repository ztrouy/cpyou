import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from "@mui/material"
import { useEffect, useState } from "react"
import { editComment } from "../../managers/commentManager"
import { editReply } from "../../managers/replyManager"

export const CommentModal = ({ isOpen, toggle, submit, typeName, importForEdit }) => {
    const [input, setInput] = useState("")
    const [failedSubmit, setFailedSubmit] = useState(false)
    const [importedComment, setImportedComment] = useState(null)
    const [importedReply, setImportedReply] = useState(null)

    useEffect(() => {
        if (!importForEdit) {
            setImportedComment(null)
            setImportedReply(null)
            setInput("")
        } else {
            if (importForEdit.hasOwnProperty("buildId")) {
                setImportedComment(importForEdit)
                setInput(importForEdit.content)
            } else if (importForEdit.hasOwnProperty("commentId")) {
                setImportedReply(importForEdit)
                setInput(importForEdit.content)
            }
        }
    }, [importForEdit])

    const createNew = () => {
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

    const editCurrentComment = () => {
        const commentToEdit = {
            id: importedComment.id,
            content: input
        }
        
        editComment(commentToEdit).then(success => {
            if (success) {
                setFailedSubmit(false)
                importedComment.content = input
                toggle()
                setImportedComment(null)
                setTimeout(() => {
                    setInput("")
                }, 200);
            } else {
                setFailedSubmit(true)
            }
        })
    }

    const editCurrentReply = () => {
        const replyToEdit = {
            id: importedReply.id,
            content: input
        }

        editReply(replyToEdit).then(success => {
            if (success) {
                setFailedSubmit(false)
                importedReply.content = input
                toggle()
                setImportedReply(null)
                setTimeout(() => {
                    setInput("")
                }, 200);
            } else {
                setFailedSubmit(true)
            }
        })
    }
    
    const handleSubmit = () => {
        if (importedComment) {
            editCurrentComment()
        } else if (importedReply) {
            editCurrentReply()
        } else {
            createNew()
        }
    }
        
    return (
        <Dialog open={isOpen} onClose={toggle} fullWidth>
            <DialogTitle>{importForEdit ? "Edit" : "Create"} {typeName}</DialogTitle>
            <DialogContent sx={{px: 2}}>
                <TextField 
                    type="text"
                    placeholder={`Write ${typeName.toLowerCase()} here...`}
                    autoFocus
                    multiline
                    fullWidth
                    value={input}
                    onChange={e => setInput(e.target.value)}
                    error={failedSubmit}
                    helperText={failedSubmit ? "Something went wrong, please try again" : ""}
                />
            </DialogContent>
            <DialogActions sx={{mr: 1, mt: -2}}>
                <Button onClick={toggle}>Cancel</Button>
                <Button onClick={handleSubmit}>Submit</Button>
            </DialogActions>
        </Dialog>
    )
}

export default CommentModal