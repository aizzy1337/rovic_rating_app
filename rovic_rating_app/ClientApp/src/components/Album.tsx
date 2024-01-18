import { Typography, colors, Paper, Grid, Box, Button, MenuItem, Select } from "@mui/material";
import { useState, useEffect } from "react";

const userId = localStorage.getItem("id");
const token = localStorage.getItem("token");

export default function Album() {

    const [userAlbums, setUserAlbums] = useState([]);
    const [movieVisibility, setMovieVisibility] = useState(false);

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
            if(data.length !== 0) setMovieVisibility(true);
            console.log(data);
          })
          .catch((error) => console.log(error));
      }, []);

      const handleSubmit = (index: number) => async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);

        fetch("https://localhost:44426/api/album/", {
          method: "PUT",
          headers: {
            "Authorization": "Bearer " + token,
            "Content-Type": "application/json"
          },
          body: JSON.stringify({
            id: userAlbums[index].id,
            title: userAlbums[index].title,
            artist: userAlbums[index].artist,
            productionYear: userAlbums[index].productionYear,
            cover: userAlbums[index].cover,
            rate: data.get('rate'),
            userId: userId,
          })
        })
          .then((response) => response.json())
          .then((data) => {
            alert("Rate was changed");
            console.log(data);
          })
          .catch((error) => {
            alert(error.message);
            console.log(error);
            }
            );
    }

    const handleDelete = (index: number) => async () =>  {

        fetch("https://localhost:44426/api/album/?id=" + userAlbums[index].id, {
          method: "DELETE",
          headers: {
            "Authorization": "Bearer " + token,
            "Content-Type": "application/json"
          }
        })
          .then((response) => response.json())
          .then((data) => {
            alert("Album was deleted");
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
                visibility: movieVisibility ? "visible" : "hidden"
            }}>

                <Typography color={colors.grey[900]} fontSize={25} sx={{
                    mb: 2
                }}>
                    Your albums
                </Typography>

                <Grid container spacing={5}>

                    {userAlbums.reverse().map((album, index) => {
                        return (
                            <Grid key={index} item xs={6} justifyContent="center" alignItems="center">

                                <Paper
                                    sx={{
                                        padding: 2,
                                        height: 225,
                                        width: 500,
                                        backgroundColor: (theme) =>
                                        theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
                                        display: "flex",
                                        justifyContent: "normal",
                                        alignItems: "center",
                                        verticalAlign: "middle",
                                    }}
                                >
                                        <img src={album.cover} alt={album.title} height="150"/>
                                        <div style={{
                                            margin: 50
                                        }}>
                                        <Typography color={colors.grey[900]} fontSize={20}>
                                            {album.title}
                                        </Typography>
                                        <Typography color={colors.grey[900]} fontSize={10}>
                                            Artist: {album.artist}
                                        </Typography>
                                        <Typography color={colors.grey[900]}>
                                            Production Year: {album.productionYear}
                                        </Typography>
                                        <Typography color={colors.grey[900]}>
                                            Rate: {album.rate}
                                        </Typography>
                                        </div>
                                        <div>
                                        {album.tags.map((tag, index) => {
                                            return (
                                                <Paper
                                                    sx={{
                                                        padding: 0.5,
                                                        margin: 0.5,
                                                        backgroundColor: (theme) =>
                                                        theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
                                                        display: "flex",
                                                        justifyContent: "center",
                                                        alignItems: "center",
                                                        verticalAlign: "middle",
                                                    }}
                                                >
                                                <Typography color={colors.grey[900]} fontSize={10}>
                                                    {tag.name}
                                                </Typography>
                                                </Paper>
                                            )
                                        })}
                                        </div>

                                        <Box component="form" onSubmit={handleSubmit(index)} noValidate sx={{ mt: 1 }} flexDirection={"column"} justifyContent={"center"} justifyItems={"center"}>

                                        <Select
                                            labelId="rate"
                                            id="demo-simple-select"
                                            defaultValue={album.rate}
                                            label="Rate"
                                            size="small"
                                            name="rate"
                                            sx={{marginLeft: 1}}
                                        >
                                            <MenuItem value={1}>1</MenuItem>
                                            <MenuItem value={2}>2</MenuItem>
                                            <MenuItem value={3}>3</MenuItem>
                                            <MenuItem value={4}>4</MenuItem>
                                            <MenuItem value={5}>5</MenuItem>
                                            <MenuItem value={6}>6</MenuItem>
                                            <MenuItem value={7}>7</MenuItem>
                                            <MenuItem value={8}>8</MenuItem>
                                            <MenuItem value={9}>9</MenuItem>
                                            <MenuItem value={10}>10</MenuItem>
                                            
                                        </Select>

                                        <Button
                                        type="submit"
                                        variant="contained" sx={{
                                            margin: 1
                                        }}
                                        >
                                        V
                                        </Button>

                                        <Button onClick={handleDelete(index)}
                                        type="button"
                                        variant="contained" sx={{
                                            margin: 1
                                        }}
                                        >
                                        X
                                        </Button>

                                    </Box>

                                </Paper>

                            </Grid >
                        )
                    })}

                </Grid>

            </Paper>
        </>
    )

}