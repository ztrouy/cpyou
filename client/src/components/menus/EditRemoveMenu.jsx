import MoreVert from "@mui/icons-material/MoreVert"
import { IconButton, Menu, MenuItem } from "@mui/material"
import { useState } from "react"

export const EditRemoveMenu = () => {
    const [anchorEl, setAnchorEl] = useState(null)
    const open = Boolean(anchorEl)

    const handleClick = (event) => {
        setAnchorEl(event.currentTarget)
        console.log("Clicked!")
    }

    const handleClose = () => {
        setAnchorEl(null)
    }

    const handleEdit = () => {
        console.log("Edit!")
        handleClose()
    }
    
    const handleDelete = () => {
        console.log("Delete!")
        handleClose()
    }
    
    return (
        <>
            <IconButton 
                onClick={handleClick} 
            >
                <MoreVert fontSize="small"/>
            </IconButton>
            <Menu
                anchorEl={anchorEl}
                anchorOrigin={{
                    vertical: "bottom",
                    horizontal: "right"
                }}
                transformOrigin={{
                    vertical: "top",
                    horizontal: "right"
                }}
                open={open}
                onClose={handleClose}
            >
                <MenuItem onClick={handleEdit}>Edit</MenuItem>
                <MenuItem onClick={handleDelete}>Delete</MenuItem>
            </Menu>
        </>
    )
}

export default EditRemoveMenu