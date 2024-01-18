import { Grid, ListItemButton, ListItemIcon, ListItemText, Paper, Typography, colors } from "@mui/material";
import React, { useEffect, useState } from "react";
import DashboardIcon from '@mui/icons-material/Dashboard';

export default function Panel() {

    const [users, setUsers] = useState([]);

    const [roles, setRoles] = useState([]);

    const userId = localStorage.getItem("id");
    const token = localStorage.getItem("token");

    useEffect(() => {
        fetch("https://localhost:44426/api/setup/getallusers/", {
          method: "GET",
          headers: {
            "Authorization": "Bearer " + token,
          },
        })
          .then((response) => response.json())
          .then((data) => {
            setUsers(data);
            console.log(data);
          })
          .catch((error) => console.log(error));
      }, []);

      useEffect(() => {
        fetch("https://localhost:44426/api/setup/", {
          method: "GET",
          headers: {
            "Authorization": "Bearer " + token,
          },
        })
          .then((response) => response.json())
          .then((data) => {
            setRoles(data);
            console.log(data);
          })
          .catch((error) => console.log(error));
      }, []);

    return (
        <>
            <Typography color={colors.grey[900]} mt={10} fontSize={50}>
                Hello {localStorage.getItem("name")}
            </Typography>

            <Paper elevation={3} sx={{
                p: 2,
                mt: 2,
            }}>

                <Typography color={colors.grey[900]} fontSize={25} sx={{
                    mb: 5
                }}>
                    All users
                </Typography>

                <Grid container spacing={1}>

                    {users.reverse().map((user, index) => {
                        return (
                            <Grid key={index} item xs={12} justifyContent="center" alignItems="center">

                                <Paper
                                    sx={{
                                        backgroundColor: (theme) =>
                                        theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
                                        overflow: "hidden",
                                        display: "flex",
                                        justifyContent: "left",
                                        alignItems: "left",
                                        textAlign: "left",
                                        verticalAlign: "middle",
                                    }}
                                >
                                        <Typography color={colors.grey[900]} sx={{
                                            padding: 2
                                        }}>
                                            Id: {user.id}
                                        </Typography>
                                        <Typography color={colors.grey[900]} sx={{
                                            padding: 2
                                        }}>
                                            Username: {user.userName}
                                        </Typography>
                                        <Typography color={colors.grey[900]} sx={{
                                            padding: 2
                                        }}>
                                            Email: {user.email}
                                        </Typography>

                                </Paper>

                            </Grid >
                        )
                    })}

                </Grid>

            </Paper>

            <Paper elevation={3} sx={{
                p: 2,
                mt: 2,
            }}>

                <Typography color={colors.grey[900]} fontSize={25} sx={{
                    mb: 5
                }}>
                    All roles
                </Typography>

                <Grid container spacing={1}>

                    {roles.map((role, index) => {
                        return (
                            <Grid key={index} item xs={12} justifyContent="center" alignItems="center">

                                <Paper
                                    sx={{
                                        backgroundColor: (theme) =>
                                        theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
                                        overflow: "hidden",
                                        display: "flex",
                                        justifyContent: "left",
                                        alignItems: "center",
                                        textAlign: "center",
                                        verticalAlign: "middle",
                                    }}
                                >
                                        <Typography color={colors.grey[900]} sx={{
                                            padding: 2
                                        }}>
                                            Id: {role.id}
                                        </Typography>
                                        <Typography color={colors.grey[900]} sx={{
                                            padding: 2
                                        }}>
                                            Role: {role.name}
                                        </Typography>

                                </Paper>

                            </Grid >
                        )
                    })}

                </Grid>

            </Paper>
        </>
    )

}