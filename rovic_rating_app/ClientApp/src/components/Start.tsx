import { Grid, ListItemButton, ListItemIcon, ListItemText, Paper, Typography, colors } from "@mui/material";
import React, { useEffect, useState } from "react";
import DashboardIcon from '@mui/icons-material/Dashboard';

export default function Start() {

    const [userMovies, setUserMovies] = useState([]);
    const [movieVisibility, setMovieVisibility] = useState(false);

    const [userAlbums, setUserAlbums] = useState([]);
    const [albumVisibility, setAlbumVisibility] = useState(false);

    const userId = localStorage.getItem("id");
    const token = localStorage.getItem("token");

    useEffect(() => {
        fetch("https://localhost:44426/api/movie/user/" + userId, {
          method: "GET",
          headers: {
            "Authorization": "Bearer " + token,
          },
        })
          .then((response) => response.json())
          .then((data) => {
            setUserMovies(data);
            if(data.length != 0) setMovieVisibility(true);
            console.log(data);
          })
          .catch((error) => console.log(error));
      }, []);

      useEffect(() => {
        fetch("https://localhost:44426/api/album/user/" + userId, {
          method: "GET",
          headers: {
            "Authorization": "Bearer " + token,
          },
        })
          .then((response) => response.json())
          .then((data) => {
            setUserAlbums(data);
            if(data.length != 0) setAlbumVisibility(true);
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
                visibility: movieVisibility ? "visible" : "hidden"
            }}>

                <Typography color={colors.grey[900]} fontSize={25} sx={{
                    mb: 2
                }}>
                    Your recently rated movies
                </Typography>

                <Grid container spacing={5}>

                    {userMovies.reverse().slice(0,4).map((movie, index) => {
                        return (
                            <Grid key={index} item xs={3} justifyContent="center" alignItems="center">

                                <Paper
                                    sx={{
                                        height: 225,
                                        width: 200,
                                        backgroundColor: (theme) =>
                                        theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
                                        overflow: "hidden",
                                        display: "flex",
                                        justifyContent: "center",
                                        alignItems: "center",
                                        textAlign: "center",
                                        verticalAlign: "middle",
                                    }}
                                >
                                    <div>
                                        <img src={movie.poster} alt={movie.title} height="150" />
                                        <Typography color={colors.grey[900]}>
                                            {movie.title}
                                        </Typography>
                                        <Typography color={colors.grey[900]}>
                                            Rate: {movie.rate}
                                        </Typography>
                                    </div>

                                </Paper>

                            </Grid >
                        )
                    })}

                </Grid>

            </Paper>

            <Paper elevation={3} sx={{
                p: 2,
                mt: 2,
                visibility: albumVisibility ? "visible" : "hidden"
            }}>

                <Typography color={colors.grey[900]} fontSize={25} sx={{
                    mb: 2
                }}>
                    Your recently rated albums
                </Typography>

                <Grid container spacing={5}>

                    {userAlbums.reverse().slice(0,4).map((album, index) => {
                        return (
                            <Grid key={index} item xs={3} justifyContent="center" alignItems="center">

                                <Paper
                                    sx={{
                                        height: 225,
                                        width: 200,
                                        backgroundColor: (theme) =>
                                        theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
                                        overflow: "hidden",
                                        display: "flex",
                                        justifyContent: "center",
                                        alignItems: "center",
                                        textAlign: "center",
                                        verticalAlign: "middle",
                                    }}
                                >
                                    <div>
                                        <img src={album.cover} alt={album.title} height="150" />
                                        <Typography color={colors.grey[900]}>
                                            {album.title}
                                        </Typography>
                                        <Typography color={colors.grey[900]}>
                                            Rate: {album.rate}
                                        </Typography>
                                    </div>

                                </Paper>

                            </Grid >
                        )
                    })}

                </Grid>

            </Paper>
        </>
    )

}