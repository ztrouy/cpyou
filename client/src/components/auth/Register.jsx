import { useState } from "react";
import { register } from "../../managers/authManager";
import { Link, useNavigate } from "react-router-dom";
import { Box, Button, Container, Paper, TextField, Typography } from "@mui/material";
import useAuthorizationProvider from "../../shared/hooks/authorization/useAuthorizationProvider.js";

export default function Register() {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [userName, setUserName] = useState("");
    const [email, setEmail] = useState("");
    const [imageLocation, setImageLocation] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [errors, setErrors] = useState([]);

    const [passwordMismatch, setPasswordMismatch] = useState();

    const {setLoggedInUser} = useAuthorizationProvider()

    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();

        if (password !== confirmPassword) {
            setPasswordMismatch(true);
        } else {
            const newUser = {
                firstName,
                lastName,
                userName,
                email,
                password,
                imageLocation: imageLocation || null,
            };

            register(newUser).then((user) => {
                if (user.errors) {
                    setErrors(user.errors);
                } else {
                    setLoggedInUser(user);
                    navigate("/");
                }
            });
        }
    };

    return (
        <Container sx={{display: "flex", justifyContent: "center"}}>
            <Paper elevation={5} sx={{mt: 5, p: 3, width: 1, maxWidth: "600px"}}>
                <Box 
                    component={"form"} 
                    onSubmit={handleSubmit} 
                    sx={{display: "flex", flexDirection: "column", gap: 2}}
                    autoComplete="off"
                >
                    <Typography variant="h3">Sign Up</Typography>
                    <TextField
                        type="text"
                        value={firstName}
                        label="First Name"
                        onChange={e => {
                            setFirstName(e.target.value)
                        }}
                    />
                    <TextField
                        type="text"
                        value={lastName}
                        label="Last Name"
                        onChange={e => {
                            setLastName(e.target.value)
                        }}
                    />
                    <TextField
                        type="text"
                        value={email}
                        label="Email"
                        onChange={e => {
                            setEmail(e.target.value)
                        }}
                    />
                    <TextField
                        type="text"
                        value={userName}
                        label="Username"
                        onChange={e => {
                            setUserName(e.target.value)
                        }}
                    />
                    <TextField
                        type="text"
                        value={imageLocation}
                        label="Image URL"
                        onChange={e => {
                            setImageLocation(e.target.value)
                        }}
                    />
                    <TextField
                        type="password"
                        value={password}
                        label="Password"
                        error={passwordMismatch}
                        onChange={e => {
                            setPasswordMismatch(false)
                            setPassword(e.target.value)
                        }}
                    />
                    <TextField
                        type="password"
                        value={confirmPassword}
                        label="Confirm Password"
                        error={passwordMismatch}
                        helperText={passwordMismatch ? "Passwords do not match!" : ""}
                        onChange={e => {
                            setPasswordMismatch(false)
                            setConfirmPassword(e.target.value)
                        }}
                    />
                    <Box sx={{display: "flex", justifyContent: "flex-end"}}>
                        <Button disabled={passwordMismatch} variant="contained" type="submit">
                            Register
                        </Button>
                    </Box>
                    {errors.map((e, i) => (
                        <Typography key={i} color={"red"}>
                            {e}
                        </Typography>
                    ))}
                    <Typography textAlign={"center"}>
                        Already signed up? Log in <Link to="/login">here</Link>
                    </Typography>
                </Box>
            </Paper>
        </Container>
    );
}
