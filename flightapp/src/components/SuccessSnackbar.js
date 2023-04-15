import React from "react";
import { Snackbar } from "@mui/material";
import Alert from "@mui/material/Alert";
import AlertTitle from "@mui/material/AlertTitle";

const SuccessSnackbar = ({ open, handleClose }) => {
  return (
    <Snackbar
      open={open}
      autoHideDuration={8000}
      onClose={handleClose}
      anchorOrigin={{ vertical: "top", horizontal: "right" }}
    >
      <Alert onClose={handleClose} severity="success" sx={{ width: "100%" }}>
        <AlertTitle>Réservation réussie!</AlertTitle>
        Vérifiez votre <strong>e-mail pour confirmation.</strong>
      </Alert>
    </Snackbar>
  );
};

export default SuccessSnackbar;
