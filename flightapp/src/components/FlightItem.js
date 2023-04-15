import React, { useState } from "react";
import {
    ListItem,
    ListItemAvatar,
    Avatar,
    ListItemText,
    ListItemSecondaryAction,
    Typography,
    Box, Button,
} from "@mui/material";
import FlightIcon from "@mui/icons-material/Flight";
import BookingDialog from "./BookingDialog";
import SuccessSnackbar from "./SuccessSnackbar";

export const FlightItem = ({flight}, passengerCount) => {
    const [bookingDialogOpen, setBookingDialogOpen] = useState(false);
    const [snackbarOpen, setSnackbarOpen] = useState(false);

    const handleBookingDialogOpen = () => {
        setBookingDialogOpen(true);
    };

    const handleBookingDialogClose = () => {
        setBookingDialogOpen(false);
    };

    const handleSnackbarClose = (event, reason) => {
        if (reason === "clickaway") {
            return;
        }
        setSnackbarOpen(false);
    };

    return (
        <div>
            <ListItem
                key={flight.id}
                button
                sx={{
                    backgroundColor: "background.paper",
                    borderRadius: 1,
                    boxShadow: 1,
                    marginBottom: 1,
                    width: 750,
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
                <ListItemSecondaryAction>
                    <Typography variant="h6" color="primary">
                        {flight.price}€
                    </Typography>
                    <Button
                        variant="contained"
                        color="primary"
                        onClick={handleBookingDialogOpen}
                    >
                        Réserver
                    </Button>
                    <BookingDialog
                        flight={flight}
                        open={bookingDialogOpen}
                        handleClose={handleBookingDialogClose}
                        handleSnackbarOpen={() => setSnackbarOpen(true)}
                        passengerCount={passengerCount}
                    />
                </ListItemSecondaryAction>
            </ListItem>
            <SuccessSnackbar
                open={snackbarOpen}
                handleClose={handleSnackbarClose}
            />
        </div>
    );
};
