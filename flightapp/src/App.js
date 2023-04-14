import './App.css';
import {SearchForm} from "./components/SearchForm";
import { ThemeProvider, createTheme } from '@mui/material/styles';

const theme = createTheme();
function App() {
  return (
    <ThemeProvider theme={theme}>
      <SearchForm/>
    </ThemeProvider>
  );
}

export default App;
