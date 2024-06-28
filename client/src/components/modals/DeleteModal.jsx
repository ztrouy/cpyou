import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from "@mui/material"

export const DeleteModal = ({ isOpen, toggle, confirmDelete, typeName}) => {
    return (
        <Dialog open={isOpen} onClose={toggle}>
            <DialogTitle>Confirm Delete</DialogTitle>
            <DialogContent>
                <DialogContentText>
                    Are you sure you wish to delete this {typeName}
                </DialogContentText>
            </DialogContent>
            <DialogActions sx={{mr: 1, mb: 1, mt: -2}}>
                <Button onClick={() => toggle()}>Cancel</Button>
                <Button onClick={() => confirmDelete()}>Confirm</Button>
            </DialogActions>
        </Dialog>
    )
}

export default DeleteModal