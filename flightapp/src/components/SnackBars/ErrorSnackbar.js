import React from "react";
import { Snackbar } from "@mui/material";
import Alert from "@mui/material/Alert";
import AlertTitle from "@mui/material/AlertTitle";

const ErrorSnackbar = ({ open, handleClose }) => {
  return (
    <Snackbar
      open={open}
      autoHideDuration={7000}
      onClose={handleClose}
      anchorOrigin={{ vertical: "top", horizontal: "right" }}
    >
      <Alert onClose={handleClose} severity="error" sx={{ width: "100%" }}>
        <AlertTitle><strong>Echec de réservation !</strong> </AlertTitle>
         Veuillez reéssayer après.
      </Alert>
    </Snackbar>
  );
};

export default ErrorSnackbar;
