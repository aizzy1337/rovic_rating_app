import { Typography, colors, Paper, Grid, Box, Button, TextField } from "@mui/material";
import { useState, useEffect } from "react";

export default function Start() {

    const [movieTags, setMovieTags] = useState([]);
    const [movieTagsVisibility, setMovieTagsVisibility] = useState(false);

    const [albumTags, setAlbumTags] = useState([]);
    const [albumTagsVisibility, setAlbumTagsVisibility] = useState(false);

    const userId = localStorage.getItem("id");
    const token = localStorage.getItem("token");

    useEffect(() => {
        fetch("https://localhost:44426/api/tag/movie/user/" + userId, {
          method: "GET",
          headers: {
            "Authorization": "Bearer " + token,
          },
        })
          .then((response) => response.json())
          .then((data) => {
            setMovieTags(data);
            if(data.length !== 0) setMovieTagsVisibility(true);
            console.log(data);
          })
          .catch((error) => console.log(error));
      }, []);

      useEffect(() => {
        fetch("https://localhost:44426/api/tag/album/user/" + userId, {
          method: "GET",
          headers: {
            "Authorization": "Bearer " + token,
          },
        })
          .then((response) => response.json())
          .then((data) => {
            setAlbumTags(data);
            if(data.length !== 0) setAlbumTagsVisibility(true);
            console.log(data);
          })
          .catch((error) => console.log(error));
      }, []);

    const handleAddMovieTagSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);

        await fetch('https://localhost:44426/api/tag/', {
      method: 'POST',
      headers: {
        "Authorization": "Bearer " + token,
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        name: data.get('text'),
        IsMovieTag: true,
        userId: userId
      })
    })
    .then((response) => {
        return response.json()
    })
    .then((data) => {
       console.log(data);
       window.location.reload();
    })
    .catch((err) => {
       console.log(err.message);
    });
    };

    const handleAddAlbumTagSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);

        await fetch('https://localhost:44426/api/tag/', {
      method: 'POST',
      headers: {
        "Authorization": "Bearer " + token,
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        name: data.get('text'),
        IsMovieTag: false,
        userId: userId
      })
    })
    .then((response) => {
        return response.json()
    })
    .then((data) => {
       console.log(data);
       window.location.reload();
    })
    .catch((err) => {
       console.log(err.message);
    });
    };

    const handleDelete = (index: number, isMovie: boolean) => async () =>  {

        const id = (isMovie) ? movieTags[index].id : albumTags[index].id;

        fetch("https://localhost:44426/api/tag/?id=" + id, {
          method: "DELETE",
          headers: {
            "Authorization": "Bearer " + token,
            "Content-Type": "application/json"
          }
        })
          .then((response) => response.json())
          .then((data) => {
            alert("Tag was deleted");
            console.log(data);
          })
          .catch((error) => { 
            alert(error.message);
            console.log(error.message); });
    }

    return (
        <>
            <Paper elevation={3} sx={{
                p: 2,
                mt: 10,
            }}>

                <Typography color={colors.grey[900]} fontSize={25} sx={{
                    mb: 5
                }}>
                    Your movie tags
                </Typography>

                <Grid container spacing={5}>

                    {movieTags.map((tag, index) => {
                        return (
                            <Grid key={index} item xs={3} justifyContent="center" alignItems="center">

                                <Paper
                                    sx={{
                                        padding: 2,
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
                                        <Typography color={colors.grey[900]}>
                                            {tag.name}
                                        </Typography>
                                    </div>

                                    <Button onClick={handleDelete(index, true)}
                                        type="button"
                                        size="small"
                                        variant="outlined" sx={{
                                            margin: 1,
                                            marginLeft: 10
                                        }}
                                        >
                                        X
                                    </Button>

                                </Paper>

                            </Grid >
                        )
                    })}

                </Grid>

                <Box component="form" onSubmit={handleAddMovieTagSubmit} noValidate sx={{ mt: 1 }} flexDirection={"column"}>
                            <TextField className='TextField' size="small" sx={{
                                margin: 1
                            }}
                            id="text"
                            label="Type tag name"
                            name="text"
                            autoFocus
                            />
                            <Button
                            type="submit"
                            variant="contained" sx={{
                                margin: 1
                            }}
                            >
                            Add
                            </Button>
                        </Box>

            </Paper>

            <Paper elevation={3} sx={{
                p: 2,
                mt: 2,
            }}>

                <Typography color={colors.grey[900]} fontSize={25} sx={{
                    mb: 5
                }}>
                    Your album tags
                </Typography>

                <Grid container spacing={5}>

                    {albumTags.map((tag, index) => {
                        return (
                            <Grid key={index} item xs={3} justifyContent="center" alignItems="center">

                                <Paper
                                    sx={{
                                        padding: 2,
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
                                        <Typography color={colors.grey[900]}>
                                            {tag.name}
                                        </Typography>
                                    </div>

                                    <Button onClick={handleDelete(index, false)}
                                        type="button"
                                        size="small"
                                        variant="outlined" sx={{
                                            margin: 1,
                                            marginLeft: 10
                                        }}
                                        >
                                        X
                                    </Button>

                                </Paper>

                            </Grid >
                        )
                    })}

                </Grid>

                <Box component="form" onSubmit={handleAddAlbumTagSubmit} noValidate sx={{ mt: 1 }} flexDirection={"column"}>
                            <TextField className='TextField' size="small" sx={{
                                margin: 1
                            }}
                            id="text"
                            label="Type tag name"
                            name="text"
                            autoFocus
                            />
                            <Button
                            type="submit"
                            variant="contained" sx={{
                                margin: 1
                            }}
                            >
                            Add
                            </Button>
                        </Box>

            </Paper>
        </>
    )

}