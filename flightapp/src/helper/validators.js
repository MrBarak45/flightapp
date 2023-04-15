export const validateEmail = (email) => {
    const emailRegex = /^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/;
    return emailRegex.test(email);
};

export const validatePhoneNumber = (phoneNumber) => {
    const phoneRegex = /^\d{10,15}$/;
    return phoneRegex.test(phoneNumber);
};