import React, { useState } from 'react';
import ListItemSecondaryAction from '@mui/material/ListItemSecondaryAction';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import {AlertTitle, Paper} from '@mui/material';
import { makeStyles } from '@mui/styles';
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';
import { List, ListItem, ListItemText, ListItemAvatar, Avatar, Typography, TextField, Pagination, Button, MenuItem, Box } from '@mui/material';
import FlightIcon from '@mui/icons-material/Flight';

const cities = [
    { value: 'ORY', label: 'Paris, France' },
    { value: 'FEZ', label: 'Fes, Maroc' },
    { value: 'LEJ', label: 'Leipzig, Allemagne' }
];

const passengers = [
    { value: 1, label: '1' },
    { value: 2, label: '2' },
    { value: 3, label: '3' },
    { value: 4, label: '4' },
];

const dockerUrl = 'http://localhost:80';
const devUrl = 'https://localhost:44316';

const useStyles = makeStyles((theme) => ({
    container: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        minHeight: '100vh',
        padding: theme.spacing(2),
    },
    formPaper: {
        padding: theme.spacing(2),
        marginBottom: theme.spacing(2),
    },
    bookingPaper: {
        padding: theme.spacing(2),
        marginTop: theme.spacing(2),
    },
    form: {
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center',

    },
    formElement: {
        margin: theme.spacing(1),
        width: '150px'
    },
}));

export const SearchForm = () => {
    const [departureCity, setDepartureCity] = useState('');
    const [arrivalCity, setArrivalCity] = useState('');
    const [departureDate, setDepartureDate] = useState('');
    const [returnDate, setReturnDate] = useState('');
    const [passengerCount, setPassengerCount] = useState('');
    const [searchResults, setSearchResults] = useState([]);

    const [selectedFlight, setSelectedFlight] = useState(null);
    const [page, setPage] = useState(1);
    const [resultsPerPage, setResultsPerPage] = useState(10);

    const [bookingDialogOpen, setBookingDialogOpen] = useState(false);
    const [emailError, setEmailError] = useState(false);
    const [phoneError, setPhoneError] = useState(false);
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [snackbarOpen, setSnackbarOpen] = useState(false);

    const classes = useStyles();

    const buildUrl = () => {
        const queryParams = new URLSearchParams({
            departureCity,
            arrivalCity,
            departureDate,
            returnDate,
            passengerCount,
        });

        return `${dockerUrl}/api/Flights?${queryParams.toString()}`;
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            const searchUrl = buildUrl();
            const response = await fetch(searchUrl, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                },
            });

            const results = await response.json();
            setSearchResults(results);
        } catch (error) {
            console.error(error);
        }
    };

    const handleBooking = async () => {
        const validateEmail = (email) => {
            const emailRegex = /^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/;
            return emailRegex.test(email);
        };
        const validatePhoneNumber = (phoneNumber) => {
            const phoneRegex = /^\d{10,15}$/;
            return phoneRegex.test(phoneNumber);
        };

        const isEmailValid = validateEmail(email);
        const isPhoneValid = validatePhoneNumber(phoneNumber);

        setEmailError(!isEmailValid);
        setPhoneError(!isPhoneValid);

        if (isEmailValid && isPhoneValid) {
            try {
                const bookingUrl = `${devUrl}/api/Bookings`;

                const bookingData = {
                    flightId: selectedFlight.id,
                    numberOfPassengers: passengerCount,
                    passengerName: name,
                    passengerEmail: email,
                    passengerPhone: phoneNumber,
                };
                // console.log(JSON.stringify(bookingData));
                const response = await fetch(bookingUrl, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(bookingData),
                });

                if (!response.ok) {
                    throw new Error(`HTTP error ${response.status}`);
                }

                const result = await response.json();
                // console.log("Booking created successfully:", result);
                console.log(selectedFlight)
            } catch (error) {
                console.error("Error creating booking:", error);
            }

            setBookingDialogOpen(false);
            setSnackbarOpen(true);

            setName('');
            setEmail('');
            setPhoneNumber('');
        }
    };

    const renderFlightItem = (flight, index) => {
        const handleBookingDialogOpen = (flight) => {
            setSelectedFlight(flight);
            setBookingDialogOpen(true);
        };

        const handleBookingDialogClose = () => {
            setBookingDialogOpen(false);
        };

        const handleSnackbarClose = (event, reason) => {
            if (reason === 'clickaway') {
                return;
            }
            setSnackbarOpen(false);
        };

        return (
            <div>
            <ListItem
                key={index}
                button
                onClick={() => setSelectedFlight(flight)}
                sx={{
                    backgroundColor: 'background.paper',
                    borderRadius: 1,
                    boxShadow: 1,
                    marginBottom: 1,
                    width: 750
                }}
            >
                <ListItemAvatar>
                    <Avatar>
                        <FlightIcon />
                    </Avatar>
                </ListItemAvatar>
                <ListItemText
                    primary={
                        <Typography variant="subtitle1">
                            {flight.departureCity} - {flight.arrivalCity}
                        </Typography>
                    }
                    secondary={
                        <Box>
                            <Typography variant="body2" color="text.secondary">
                                Depart: {flight.departureDate} / Retour: {flight.returnDate}
                            </Typography>
                            <Typography variant="body2" color="info.main">
                                Places: {flight.capacity}

                            </Typography>
                        </Box>
                    }
                />

                <Dialog open={bookingDialogOpen} onClose={handleBookingDialogClose}>
                    <DialogTitle>Réserver le vol</DialogTitle>
                    <DialogContent>
                        <TextField
                            autoFocus
                            margin="dense"
                            label="Nom et prénom"
                            type="text"
                            fullWidth
                            value={name}
                            onChange={(event) => setName(event.target.value)}
                        />
                        <TextField
                            error={emailError}
                            helperText={emailError ? 'Veuillez saisir une adresse email valide.' : ''}
                            margin="dense"
                            label="Email"
                            type="email"
                            fullWidth
                            value={email}
                            onChange={(event) => {
                                setEmail(event.target.value);
                                setEmailError(false);
                            }}
                        />
                        <TextField
                            error={phoneError}
                            helperText={phoneError ? 'Veuillez saisir un numéro de téléphone valide.' : ''}
                            margin="dense"
                            label="Téléphone"
                            type="tel"
                            fullWidth
                            value={phoneNumber}
                            onChange={(event) => {
                                setPhoneNumber(event.target.value);
                                setPhoneError(false);
                            }}
                        />
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleBookingDialogClose}>Annuler</Button>
                        <Button onClick={handleBooking}>Réserver</Button>
                    </DialogActions>
                </Dialog>

                <ListItemSecondaryAction>
                    <Typography variant="h6" color="primary">
                        {flight.price}€
                    </Typography>
                    <Button variant="contained" color="primary" onClick={() => handleBookingDialogOpen(flight)}>
                        Réserver
                    </Button>
                </ListItemSecondaryAction>
            </ListItem>
                <Snackbar
                    open={snackbarOpen}
                    autoHideDuration={8000}
                    onClose={handleSnackbarClose}
                    anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
                >
                    <Alert onClose={handleSnackbarClose} severity="success" sx={{ width: '100%' }}>
                        <AlertTitle>Réservation réussie!</AlertTitle>
                        Vérifiez votre <strong>e-mail pour confirmation.</strong>
                    </Alert>
                </Snackbar>

            </div>
        );
    };
    const renderSearchResults = () => {
        return (
            <div>
                <div>
                    <List>
                        {searchResults
                            .slice((page - 1) * resultsPerPage, page * resultsPerPage)
                            .map((flight, index) => renderFlightItem(flight, index))}
                    </List>

                    <Pagination
                        count={Math.ceil(searchResults.length / resultsPerPage)}
                        page={page}
                        onChange={(event, value) => setPage(value)}
                        sx={{ marginTop: 2, justifyContent: 'center', display: 'flex' }}
                    />
                </div>
            </div>
        );
    };

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
                    <Button type="submit" variant="contained" color="primary" className={classes.formElement} disabled={!departureCity || !arrivalCity || !departureDate || !returnDate || !passengerCount}>
                        Rechercher
                    </Button>
                </form>
            </Paper>

            {renderSearchResults()}
        </div>
    );
};
