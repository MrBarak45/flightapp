import React, { useState } from 'react';
import { TextField, Button, MenuItem, Paper } from '@mui/material';
import {buildGetFlightsUrl} from "../helper/url";
import componentStyles from "./styles";
import {SearchResults} from "./SearchResults";
import {passengers, cities} from "../helper/constants";

export const SearchForm = () => {
    const [departureCity, setDepartureCity] = useState('');
    const [arrivalCity, setArrivalCity] = useState('');
    const [departureDate, setDepartureDate] = useState('');
    const [returnDate, setReturnDate] = useState('');
    const [passengerCount, setPassengerCount] = useState('');
    const [searchResults, setSearchResults] = useState([]);

    const [searchResultVisibility, setSearchResultVisibility] = useState(false);

    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            const searchUrl = buildGetFlightsUrl(departureCity, arrivalCity, departureDate, returnDate, passengerCount);

            const response = await fetch(searchUrl, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                },
            });

            const results = await response.json();

            setSearchResults(results);
            setSearchResultVisibility(true);
        } catch (error) {
            //snack bar with error later
            console.error(error);
        }
    };

    const classes = componentStyles();

    const year = new Date().getFullYear();
    const minDate = new Date(year, 3, 14).toISOString().split('T')[0];
    const maxDate = new Date(year, 3, 18).toISOString().split('T')[0];

    return (
        <div className={classes.container}>
            <Paper className={classes.formPaper}>
                <form className={classes.form} onSubmit={handleSubmit}>
                    <TextField
                        select
                        label="Ville de départ"
                        value={departureCity}
                        onChange={(event) => setDepartureCity(event.target.value)}
                        className={classes.formElement}
                    >
                        {cities.map((option) => (
                            <MenuItem key={option.value} value={option.value}>
                                {option.label}
                            </MenuItem>
                        ))}
                    </TextField>
                    <TextField
                        className={classes.formElement}
                        select
                        label="Ville d'arrivée"
                        value={arrivalCity}
                        onChange={(event) => setArrivalCity(event.target.value)}
                    >
                        {cities.map((option) => (
                            <MenuItem key={option.value} value={option.value}>
                                {option.label}
                            </MenuItem>
                        ))}
                    </TextField>
                    <TextField
                        className={classes.formElement}
                        label="Date de départ"
                        type="date"
                        InputLabelProps={{
                            shrink: true,
                        }}
                        inputProps={{min: minDate, max: maxDate }}
                        value={departureDate}
                        onChange={(event) => setDepartureDate(event.target.value)}
                    />
                    <TextField
                        className={classes.formElement}
                        label="Date de retour"
                        type="date"
                        InputLabelProps={{
                            shrink: true,
                        }}
                        value={returnDate}
                        inputProps={{min: departureDate, max: maxDate}}
                        onChange={(event) => setReturnDate(event.target.value)}
                    />
                    <TextField
                        className={classes.formElement}
                        select
                        label="Nombre de passagers"
                        value={passengerCount}
                        onChange={(event) => setPassengerCount(event.target.value)}
                    >
                        {passengers.map((option) => (
                            <MenuItem key={option.value} value={option.value}>
                                {option.label}
                            </MenuItem>
                        ))}
                    </TextField>
                    <Button type="submit" variant="contained" color="primary" className={classes.formElement}
                            disabled={!departureCity || !arrivalCity || !departureDate || !returnDate || !passengerCount}>
                        <strong>Rechercher</strong>
                    </Button>
                </form>
            </Paper>

            {
                searchResultVisibility &&
                    <SearchResults searchResults={searchResults} passengerCount={passengerCount}></SearchResults>
            }

        </div>
    );
};
