import {List, Pagination} from "@mui/material";
import React, {useState} from "react";
import {FlightItem} from "./FlightItem";


export const SearchResults = ({searchResults, passengerCount}) => {
    const [page, setPage] = useState(1);
    const [resultsPerPage, setResultsPerPage] = useState(10);



    return (
        <div>
            {
                searchResults.length > 0 ?

                    <div>
                        <List>
                            {searchResults
                                .slice((page - 1) * resultsPerPage, page * resultsPerPage)
                                .map((flight) => <FlightItem flight={flight} passengerCount={passengerCount} />)}
                        </List>

                        <Pagination
                            count={Math.ceil(searchResults.length / resultsPerPage)}
                            page={page}
                            onChange={(event, value) => setPage(value)}
                            sx={{ marginTop: 2, justifyContent: 'center', display: 'flex' }}
                        />
                    </div>

                    :

                    <div>
                        <span> Aucun resultat trouve ! </span>
                    </div>



            }
        </div>
    );
};