import { Button, Dialog, DialogActions, DialogContent, DialogTitle } from "@mui/material"

export const DeleteModal = ({ isOpen, toggle, confirmDelete, typeName}) => {
    return (
        <Dialog open={isOpen} onClose={toggle}>
            <DialogTitle>Confirm Delete</DialogTitle>
            <DialogContent>Are you sure you wish to delete this {typeName}</DialogContent>
            <DialogActions>
                <Button onClick={() => toggle()}>Cancel</Button>
                <Button onClick={() => confirmDelete()}>Confirm</Button>
            </DialogActions>
        </Dialog>
    )
}

export default DeleteModal