const dockerUrl = 'http://localhost:80';
const devUrl = 'https://localhost:44316';

const usedUrl = dockerUrl;

export const buildGetFlightsUrl = (departureCity, arrivalCity, departureDate, returnDate, passengerCount) => {
    const queryParams = new URLSearchParams({
        departureCity,
        arrivalCity,
        departureDate,
        returnDate,
        passengerCount,
    });

    return `${usedUrl}/api/Flights?${queryParams.toString()}`;
};

export const buildPostBookingUrl = () => {
    return `${usedUrl}/api/Bookings`;
}











