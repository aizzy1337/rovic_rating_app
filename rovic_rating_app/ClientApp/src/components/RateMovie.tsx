import { Typography, colors, Paper, Grid, Button, TextField, Box, Link, Select, InputLabel, MenuItem, SelectChangeEvent } from "@mui/material";
import React from "react";
import { useState, useEffect } from "react";

const userId = localStorage.getItem("id");
const token = localStorage.getItem("token");

export default function RateMovie() {

    const [movies, setMovies] = useState([]);
    const [movieVisibility, setMovieVisibility] = useState(false);

    const [movieTags, setMovieTags] = useState([]);
    const [movieTagsVisibility, setMovieTagsVisibility] = useState(false);

    const [tagList, setTagList] = React.useState<string[]>([]);

    const handleChange = (event: SelectChangeEvent<typeof tagList>) => {
        const {
        target: { value },
        } = event;
        setTagList(
        typeof value === 'string' ? value.split(',') : value,
        );
        console.log(tagList);
    };

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

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);

        fetch("https://localhost:44426/api/movie/search/" + data.get('text'), {
          method: "GET",
          headers: {
            "Authorization": "Bearer " + token,
          },
        })
          .then((response) => response.json())
          .then((data) => {
            setMovies(data);
            if(data.length !== 0) setMovieVisibility(true);
            console.log(data);
          })
          .catch((error) => console.log(error));
    }

    const handleAddSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);

        await fetch('https://localhost:44426/api/movie/', {
      method: 'POST',
      headers: {
        "Authorization": "Bearer " + token,
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        title: movies[0].title,
        description: movies[0].description,
        productionYear: movies[0].productionYear,
        poster: movies[0].poster,
        rate: data.get('rate'),
        userId: userId
      })
    })
    .then((response) => {
        return response.json()
    })
    .then((data) => {
        alert("Movie was added");
       console.log(data);
       tagList.forEach(async tag => {
        await fetch('https://localhost:44426/api/movie/' + data.id + '/tag/' + tag, {
            method: 'POST',
            headers: {
              "Authorization": "Bearer " + token,
              "Content-Type": "application/json"
            }
          })
          .then((response) => {
              return response.json()
          })
          .then((data) => {
             console.log(data);
          })
          .catch((err) => {
             console.log(err.message);
          });
       });
    })
    .catch((err) => {
       console.log(err.message);
    });

    }

    return (
        <>
            <Paper elevation={3} sx={{
                p: 2,
                mt: 10,
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                textAlign: "center",
                verticalAlign: "middle",
            }}>

                <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }} flexDirection={"column"}>
                            <TextField className='TextField' size="small" sx={{
                                margin: 1
                            }}
                            id="text"
                            label="Type movie name"
                            name="text"
                            autoFocus
                            />
                            <Button
                            type="submit"
                            variant="contained" sx={{
                                margin: 1
                            }}
                            >
                            Search
                            </Button>
                        </Box>

            </Paper>

            <Paper elevation={3} sx={{
                p: 2,
                mt: 1,
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                textAlign: "center",
                verticalAlign: "middle",
                visibility: movieVisibility ? "visible" : "hidden"
            }}>

                <Grid container spacing={5}>

                {movies.slice(0,1).map((movie, index) => {
                    return (
                        <Grid key={index} item xs={12} justifyContent="center" alignItems="center">

                            <Paper
                                sx={{
                                    padding: 2,
                                    backgroundColor: (theme) =>
                                    theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
                                    display: "flex",
                                    justifyContent: "normal",
                                    alignItems: "center",
                                    verticalAlign: "middle",
                                }}
                            >
                                    <img src={movie.poster} alt={movie.title} height="400"/>
                                    <div style={{
                                        margin: 50
                                    }}>
                                    <Typography color={colors.grey[900]} fontSize={20} sx={{padding: 2}}>
                                        {movie.title}
                                    </Typography>
                                    <Typography color={colors.grey[900]} fontSize={10} overflow={"hidden"} sx={{padding: 2}}>
                                        Description: {movie.description}
                                    </Typography>
                                    <Typography color={colors.grey[900]} sx={{padding: 2}}>
                                        Production Year: {movie.productionYear}
                                    </Typography>


                                    <Box component="form" onSubmit={handleAddSubmit} noValidate sx={{ mt: 1 }} flexDirection={"column"}>
                                        <Select
                                            labelId="tag"
                                            id="demo-simple-select"
                                            value={tagList}
                                            placeholder="Tags"
                                            onChange={handleChange}
                                            label="Tags"
                                            size="small"
                                            multiple
                                        >
                                            {movieTags.slice(0,4).map((tag, index) => {
                                                return (
                                                    <MenuItem value={tag.id}>{tag.name}</MenuItem>
                                                )}
                                            )}
                                        </Select>

                                        <Select
                                            labelId="rate"
                                            id="demo-simple-select"
                                            defaultValue={1}
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
                                        Add
                                        </Button>

                                    </Box>

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