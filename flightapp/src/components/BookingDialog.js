import React, { useState } from "react";
import { Dialog, DialogTitle, DialogContent, DialogActions, TextField, Button} from "@mui/material";
import {validateEmail, validatePhoneNumber} from "../helper/validators";
import {buildPostBookingUrl} from "../helper/url";

const BookingDialog = ({ flight, open, handleClose, handleSuccessSnackbarOpen, handleErrorSnackbarOpen, passengerCount }) => {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [emailError, setEmailError] = useState(false);
    const [phoneNumber, setPhoneNumber] = useState("");
    const [phoneError, setPhoneError] = useState(false);

    const handleBooking = async () => {
        const isEmailValid = validateEmail(email);
        const isPhoneValid = validatePhoneNumber(phoneNumber);
        setEmailError(!isEmailValid);
        setPhoneError(!isPhoneValid);


        if (isEmailValid && isPhoneValid) {
            try {
                const bookingUrl = buildPostBookingUrl();
                const bookingData = {
                    flightId: flight.id,
                    numberOfPassengers: passengerCount,
                    passengerName: name,
                    passengerEmail: email,
                    passengerPhone: phoneNumber,
                };
                // console.log("booking data")
                // console.log(bookingData)

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

                // console.log("result")
                // console.log(result)
                if(result){
                    handleClose();
                    handleSuccessSnackbarOpen();
                } else {
                    handleClose();
                    handleErrorSnackbarOpen();
                }
                // console.log("Booking created successfully:", result);
                // console.log(selectedFlight)
            } catch (error) {
                console.error("Error creating booking:", error);
            }

            setName('');
            setEmail('');
            setPhoneNumber('');
        }
    };

    return (
        <Dialog open={open} onClose={handleClose}>
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
                    helperText={
                        emailError ? "Veuillez saisir une adresse email valide." : ""
                    }
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
                    helperText={
                        phoneError ? "Veuillez saisir un numéro de téléphone valide." : ""
                    }
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
                <Button onClick={handleClose}>Annuler</Button>
                <Button onClick={handleBooking}>Réserver</Button>
            </DialogActions>
        </Dialog>
    );
};

export default BookingDialog;