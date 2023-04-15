import { createTheme } from "@mui/material/styles";

const theme = createTheme({})
export const componentStyles = {
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
};

export default componentStyles;
